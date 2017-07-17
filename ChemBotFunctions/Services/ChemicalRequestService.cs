// Dot net Namespaces
using System;
using System.Net;

// ChemBot Namespaces
using ChemBotFunctions.Services.PubChemService;
using ChemBotFunctions.Data;
using ChemBotFunctions.Utils;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services
{
    /// <summary>
    /// Manages the requests to the PubChem Service and the responses and errors.
    /// </summary>
    public class ChemicalRequestService : IDisposable
    {
        #region Variables
        /// <summary>
        /// Instance of the PubChem Response formatter object.
        /// </summary>
        private IPubChemFormatter formatter;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChemicalRequestService()
        {
            formatter = new PubChemFormatter();
        }
        #endregion

        #region Services
        /// <summary>
        /// Get Chemical request response.
        /// </summary>
        /// <param name="request">Instance of a ChemicalCompoundRequest made by the user in Slack.</param>
        /// <returns>Instance of the response.</returns>
        public ChemicalResponseData GetChemicalRequest(ChemicalCompoundRequest request)
        {
            ChemicalResponseData result = new ChemicalResponseData(request);

            try
            {
                result = GetRequestResult(request, result);
                result.State = ResponseState.SUCESSFULL;
            }
            catch(ChemBotWebException webException)
            {
                ProcessWebException(webException, result);
            }
            catch(Exception excep)
            {
                ProcessException(excep, result);
            }

            return result;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Process response data and gets an user friendly representation of the data.
        /// </summary>
        /// <param name="request">Chemical Request.</param>
        /// <param name="result">Chemical response.</param>
        /// <returns></returns>
        private ChemicalResponseData GetRequestResult(ChemicalCompoundRequest request, ChemicalResponseData result)
        {
            using (IPubChemRequest serviceRequest = new PubChemRequestService())
            {
                JObject response = null;

                var propertyToRetrieve = request.PubChemChemicalPropertyName;
                bool doFlashCardAttachments = false;

                if (string.Equals(propertyToRetrieve, "synonyms", StringComparison.Ordinal) ||
                    string.Equals(propertyToRetrieve, "synonym", StringComparison.Ordinal))
                {
                    response = serviceRequest.GetSynonymsFromId(request.SearchCriteria, request.IdentifierType);
                }
                else
                {
                    if (propertyToRetrieve == ChemBotText.FLASH_CARD_PROPERTY)
                    {
                        doFlashCardAttachments = true;
                        propertyToRetrieve = ChemBotText.FLASH_CARD_URL_TABLE_PROPERTIES;
                    }

                    response = serviceRequest.GetChemicalPropertiesFromId(request.SearchCriteria,
                        request.IdentifierType, propertyToRetrieve);

                }

                var formatResult = formatter.ProcessMessage(response);
                result.ResultText = formatResult.TextResult;
                result.CID = formatResult.CID;

                if(doFlashCardAttachments)
                {
                    AddAttachmentUrl(request, result, ChemBotText.ATTACHMENT_STRUCTURE);
                    AddAttachmentUrl(request, result, ChemBotText.ATTACHMENT_SDF);
                }
                else
                {
                    AddAttachmentUrl(request, result, request.Attachment);
                }

                // Always add a reference to the PubChem Record
                AddAttachmentUrl(request, result, ChemBotText.ATTACHMENT_PUBCHEM_LINK);
            }

            return result;
        }

        /// <summary>
        /// Process a web exception and returns a friendly user message. The full exception data will be stored in the Lambda logs.
        /// </summary>
        /// <param name="webException">Instance of a web exception.</param>
        /// <param name="result">Instance of the chimical response.</param>
        /// <returns>Chemical response with error messsages.</returns>
        private ChemicalResponseData ProcessWebException(ChemBotWebException webException, ChemicalResponseData result)
        {
            result.ExceptionStackTrace = webException.StackTrace;
            result.ResultText.AppendLine(webException.Message);
            result.ExceptionMessage = webException.Message;
            result.State = ResponseState.ERROR;
            return result;
        }

        /// <summary>
        /// Process a generic .NET exception into a friendly error message. It will record the full exception in the Lambda logs.
        /// </summary>
        /// <param name="excep">Instance of the .NET exception.</param>
        /// <param name="result">Instance of the chemical response with friendly user message.</param>
        /// <returns>Instance of the chemical response with error information.</returns>
        private ChemicalResponseData ProcessException(Exception excep, ChemicalResponseData result)
        {
            result.ResultText.AppendLine(ChemBotText.MESSAGE_GENERIC_REQUEST_ERROR);
            result.ExceptionStackTrace = excep.StackTrace;
            result.ExceptionMessage = excep.Message;
            return result;
        }

        /// <summary>
        /// Add attachment URL to the Chemical Response.
        /// </summary>
        /// <param name="request">Chemical Request.</param>
        /// <param name="result">Chemical response.</param>
        /// <param name="attachmentTypeRequested">Type of attachment request. It was separated to make easier the generation of the Flash Cards.</param>
        private void AddAttachmentUrl(ChemicalCompoundRequest request, ChemicalResponseData result, string attachmentTypeRequested)
        {
            if(!string.IsNullOrEmpty(attachmentTypeRequested))
            {
                var baseUrl = $"{ChemBotText.PUBCHEM_SERVER}compound/cid/{result.CID}/";
                if(attachmentTypeRequested == ChemBotText.ATTACHMENT_SDF)
                {
                    result.Attachments.Add(new ChemicalResponseAttachment {
                        AttachmentURL = $"{baseUrl}SDF",
                        Title = ChemBotText.ATTACHMENT_SDF_TITLE
                    });
                }
                else if(attachmentTypeRequested == ChemBotText.ATTACHMENT_STRUCTURE)
                {
                    result.Attachments.Add(new ChemicalResponseAttachment {
                        ImageURL = $"{baseUrl}PNG",
                        Title = ChemBotText.ATTACHMENT_STRUCTURE_TITLE
                    });
                }
                else if(attachmentTypeRequested == ChemBotText.ATTACHMENT_PUBCHEM_LINK)
                {
                    result.Attachments.Add(new ChemicalResponseAttachment
                    {
                        AttachmentURL = $"{ChemBotText.PUBCHEM_DOMAIN}compound/{result.CID}",
                        Title = ChemBotText.ATTACHMENT_PUBCHEM_LINK_TITLE
                    });
                }
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    formatter.Dispose();
                    formatter = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ChemicalRequestService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
