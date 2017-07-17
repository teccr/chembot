// Dot Net Namespaces
using System;
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;
using ChemBotFunctions.Services.ChemBotHelpService.HelpComposers;

// AWS Namespaces
using Amazon.Lambda.LexEvents;

namespace ChemBotFunctions.Services.ChemBotHelpService
{
    /// <summary>
    /// Resolves help requests
    /// </summary>
    internal class HelpRequestService : IHelpRequestService
    {
        #region Variables
        /// <summary>
        /// Reference to the chain of composers to process help requests.
        /// </summary>
        private HelpComposer composers;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public HelpRequestService()
        {
            InitComposers();
        }

        #endregion

        #region IHelpRequestService Implementation

        /// <summary>
        /// Returns the correct Lex response accordingly to the criteria requested by the user.
        /// </summary>
        /// <param name="helpRequest">Help Request sent by the user.</param>
        /// <returns>Lex Response for the help request.</returns>
        HelpResponse IHelpRequestService.ResolveHelpRequest(string helpRequest)
        {
            HelpResponse response = null;

            if(string.IsNullOrEmpty(helpRequest))
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine($"{ChemBotText.TEXT_REQUEST_NOT_PROCESSED}.");

                response = new HelpResponse
                {
                    Successful = false,
                    Result = message
                };
            }
            else
            {
                response = composers.ComposeHelpMessage(helpRequest);
            }

            return response;
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Initializes composers
        /// </summary>
        private void InitComposers()
        {
            composers = new HelpChemicalProperties();
            var idTypes = new HelpIDTypes();
            var searchCriteria = new HelpSearchCriteria();
            var flashCard = new HelpFlashCard();
            composers.SetNextComposer(idTypes);
            idTypes.SetNextComposer(searchCriteria);
            searchCriteria.SetNextComposer(flashCard);
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
                    ((IDisposable)composers).Dispose();
                    composers = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~HelpRequestService() {
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
