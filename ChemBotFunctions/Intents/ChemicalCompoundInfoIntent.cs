// Dot Net Namespaces
using System.Collections.Generic;

// AWS Namespaces
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

// ChemBot Namespaces
using ChemBotFunctions.Data;
using ChemBotFunctions.Validation;
using ChemBotFunctions.Utils;
using ChemBotFunctions.Services;

namespace ChemBotFunctions.Intents
{
    /// <summary>
    /// This class will process an intent to get information about a chemical compound.
    /// </summary>
    public class ChemicalCompoundInfoIntent : BaseIntent
    {
        #region Intent Slots
        /// <summary>
        /// Criteria identifying the compound. Example: Glucose, C1CCCC1C, etc.
        /// </summary>
        private const string SLOT_COMPOUND_TO_SEARCH = "CompoundToSearch";

        /// <summary>
        /// Compound property to search. Example: Synonyms, Molecular Formula, Exact Mass, etc.
        /// </summary>
        private const string SLOT_PROPERTY_TO_SEARCH = "PropertyToSearch";

        /// <summary>
        /// Type of compound identifier used in the search. Example: name, sid, etc.
        /// </summary>
        private const string SLOT_ID_TYPE = "IdType";

        /// <summary>
        /// Attachments for the search.
        /// </summary>
        private const string SLOT_ATTACHMENT = "Attachments";

        /// <summary>
        /// Serialized version of the current request.
        /// </summary>
        internal const string CURRENT_REQUEST_SESSION_ATTRIBUTE = "currentRequest";

        /// <summary>
        /// Serialized version of the last successful request.
        /// </summary>
        internal const string LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE = "lastConfirmedRequest";
        #endregion

        #region BaseIntent Overrides
        /// <summary>
        /// Main method for processing the Lex event for the intent.
        /// </summary>
        /// <param name="lexEvent">Instance of the Lex Event.</param>
        /// <param name="context">Context of the Lambda Execution.</param>
        /// <returns>Lex response for the bot.</returns>
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            // Get data from the Lex intent
            var slots = lexEvent.CurrentIntent.Slots;
            var sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();

            // Initialize data proxy
            var request = PopulateRequestProxy(slots);

            // Evaluate previous requests
            string confirmationStatus = lexEvent.CurrentIntent.ConfirmationStatus;
            ChemicalCompoundRequest lastRequest = GetLastRequest(sessionAttributes);
            if(lastRequest != null && lastRequest.FailedInPreviousExecution)
            {
                if(confirmationStatus == ChemBotText.INTENT_FULLFILMENT_CONFIRMED)
                {
                    request = lastRequest;
                }
                else
                {
                    return Close(sessionAttributes, ChemBotText.INTENT_FULLFILMENT_FAILED, new LexResponse.LexMessage
                    {
                        ContentType = ChemBotText.MESSAGE_CONTENT_TYPE,
                        Content = ChemBotText.ERROR_TALK_TO_YOU
                    });
                }
            }

            var validationResult = Validate(request);
            context.Logger.LogLine( $"Has required fields: {request.HasRequiredFields}, Has valid values: {validationResult.IsValid}" );
            if(!(request.HasRequiredFields && validationResult.IsValid))
            {
                context.Logger.LogLine($"Slot {validationResult.ViolationSlot} is invalid: {validationResult.Message?.Content}");
                return ElicitSlot(sessionAttributes, ChemBotText.INTENT_COMPOUND_INFO, slots, validationResult.ViolationSlot,
                    validationResult.Message);
            }

            // Execute Search
            ChemicalRequestService pubChemService = new ChemicalRequestService();
            var response = pubChemService.GetChemicalRequest(request);
            LogResponseInformation(response, lexEvent, context);

            // Return chemical query response
            return ConvertChemicalResponseToLexResponse(response, lexEvent, sessionAttributes);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Verifies that any values for slots in the intent are valid.
        /// </summary>
        /// <param name="request">Instance of the Chemical compound request.</param>
        /// <returns>Result of the validation.</returns>
        private ValidationResult Validate(ChemicalCompoundRequest request)
        {
            if(string.IsNullOrEmpty(request.IdentifierType) || 
                !TypeValidators.IsValidSearchCriteria(request.IdentifierType))
            {
                return new ValidationResult(false, SLOT_ID_TYPE, 
                    string.Format(ChemBotText.ERROR_ID_NOT_SUPPORTED, request.IdentifierType));
            }

            if (string.IsNullOrEmpty(request.PropertyToRetrieve) || 
                string.IsNullOrEmpty(request.PubChemChemicalPropertyName))
            {
                return new ValidationResult(false, SLOT_PROPERTY_TO_SEARCH,
                    string.Format(ChemBotText.ERROR_PROPERTY_NOT_SUPPORTED, request.PropertyToRetrieve));
            }

            if (string.IsNullOrEmpty(request.SearchCriteria))
            {
                return new ValidationResult(false, SLOT_COMPOUND_TO_SEARCH,
                    ChemBotText.ERROR_SEARCH_CRITERIA_EMPTY);
            }

            return ValidationResult.VALID_RESULT;
        }

        /// <summary>
        /// Creates a new instance of a ChemicalCompoundRequest and fills it out with the information from slots.
        /// </summary>
        /// <param name="slots">Slots of the current intent.</param>
        /// <returns>New ChemicalCompoundRequest instance.</returns>
        private ChemicalCompoundRequest PopulateRequestProxy(IDictionary<string, string> slots)
        {
            ChemicalCompoundRequest request = new ChemicalCompoundRequest
            {
                IdentifierType = slots.ContainsKey(SLOT_ID_TYPE) ? slots[SLOT_ID_TYPE] : null,
                PropertyToRetrieve = slots.ContainsKey(SLOT_PROPERTY_TO_SEARCH) ? slots[SLOT_PROPERTY_TO_SEARCH] : null,
                SearchCriteria = slots.ContainsKey(SLOT_COMPOUND_TO_SEARCH) ? slots[SLOT_COMPOUND_TO_SEARCH] : null,
                Attachment = slots.ContainsKey(SLOT_ATTACHMENT) ? slots[SLOT_ATTACHMENT].ToLower() : null
            };

            return request;
        }

        /// <summary>
        /// Verifies if there is a previous request serialized in the session attributes, if there any the function will deserialize it.
        /// </summary>
        /// <param name="sessionAttributes">Session attributes for the current.</param>
        /// <returns>Deserialized instance of the ChemicalCompoundRequest.</returns>
        private ChemicalCompoundRequest GetLastRequest(IDictionary<string, string> sessionAttributes)
        {
            ChemicalCompoundRequest lastRequest = null;

            if (sessionAttributes.ContainsKey(LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE) 
                && !string.IsNullOrEmpty(sessionAttributes[LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE]))
            {
                lastRequest = DeserializeChemicalRequest(sessionAttributes[LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE]);
            }

            return lastRequest;
        }

        /// <summary>
        /// Convertes a PubChem Response object into a Lex Response.
        /// </summary>
        /// <param name="response">PubChem Response data.</param>
        /// <param name="lexEvent">Lex event data.</param>
        /// <param name="sessionAttributes">Session attributes for the current.</param>
        /// <returns></returns>
        private LexResponse ConvertChemicalResponseToLexResponse( ChemicalResponseData response , LexEvent lexEvent, 
            IDictionary<string, string> sessionAttributes)
        {
            LexResponse result = null;

            // Evaluate errors and give the user the opportunity to retry if needed.
            if (!string.IsNullOrEmpty(response.ExceptionMessage))
            {
                if(response.CanRetry)
                {
                    var serializedRequest = SerializeChemicalRequest(response.Request);
                    if (sessionAttributes.ContainsKey(LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE))
                    {
                        sessionAttributes[LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE] = serializedRequest;
                    }
                    else
                    {
                        sessionAttributes.Add(LAST_CONFIRMED_REQUEST_SESSION_ATTRIBUTE, serializedRequest);
                    }

                    result = ConfirmIntent(sessionAttributes, ChemBotText.INTENT_COMPOUND_INFO,
                        lexEvent.CurrentIntent.Slots, new LexResponse.LexMessage
                        {
                            ContentType = ChemBotText.MESSAGE_CONTENT_TYPE,
                            Content = response.ResultText.ToString()
                        });
                }
                else
                {
                    result = Close(sessionAttributes, ChemBotText.INTENT_FULLFILMENT_FAILED, new LexResponse.LexMessage
                        {
                            ContentType = ChemBotText.MESSAGE_CONTENT_TYPE,
                            Content = response.ResultText.ToString()
                        });
                }
            }
            else
            {
                result = Close(sessionAttributes, ChemBotText.INTENT_FULLFILMENT_FULFILLED, new LexResponse.LexMessage
                {
                    ContentType = ChemBotText.MESSAGE_CONTENT_TYPE,
                    Content = $"*CID*: _{response.CID}_\n{response.ResultText.ToString()}"
                });

                if (response.Attachments.Count > 0)
                {
                    var ResponseCard = new LexResponse.LexResponseCard();

                    foreach(var attachment in response.Attachments)
                    {
                        var newAttachment = CreateGenericAttachment(attachment);

                        AddAttachmentsToResponseCard(ResponseCard, newAttachment);
                    }
                    
                    result.DialogAction.ResponseCard = ResponseCard;
                }
            }

            return result;
        }

        /// <summary>
        /// Logs information into the Lambda logging framework.
        /// </summary>
        /// <param name="response">Chemical Response data</param>
        /// <param name="lexEvent">Lex Event data.</param>
        /// <param name="context">Context of the Lambda function.</param>
        private void LogResponseInformation(ChemicalResponseData response, LexEvent lexEvent, ILambdaContext context)
        {
            string messageToLog = null;

            if(string.IsNullOrEmpty(response.ExceptionMessage))
            {
                messageToLog = $"User Id: {lexEvent.UserId}, Response from PubChem Successful!";
            }
            else
            {
                messageToLog = $"User Id: {lexEvent.UserId}, Error: {response.ExceptionMessage}, Stack Trace: {response.ExceptionStackTrace}";
            }

            context.Logger.LogLine(messageToLog);
        }

        #endregion
    }
}
