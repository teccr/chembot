// Dot Net Namespaces
using System.Collections.Generic;

// AWS Namespaces
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

// ChemBot Namespaces
using ChemBotFunctions.Data;

// Third Party Namespaces
using Newtonsoft.Json;

namespace ChemBotFunctions.Intents
{
    /// <summary>
    /// All intents in ChemBot will use this class as base class.
    /// </summary>
    public abstract class BaseIntent : IIntentProcessor
    {
        
        #region IIntentProcessor implementation
        /// <summary>
        /// Main function to process an intent.
        /// </summary>
        /// <param name="lexEvent">Lex event information</param>
        /// <param name="context">Context of the intent.</param>
        /// <returns>Returns the bot response.</returns>
        public abstract LexResponse Process(LexEvent lexEvent, ILambdaContext context);
        #endregion

        #region Helpers to Deal with Lex Interactions
        /// <summary>
        /// Creates a Lex Response with a CLose State.
        /// </summary>
        /// <param name="sessionAttributes">Session attributes to save</param>
        /// <param name="fulfillmentState">Fulfillment state string (Failed, etc.)</param>
        /// <param name="message">Message to display.</param>
        /// <returns>Response of the Lex Bot.</returns>
        protected LexResponse Close(IDictionary<string, string> sessionAttributes, string fulfillmentState, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Close",
                    FulfillmentState = fulfillmentState,
                    Message = message
                }
            };
        }

        /// <summary>
        /// Creates a Lex response to delegate the action to the user.
        /// </summary>
        /// <param name="sessionAttributes">Session attributes to save</param>
        /// <param name="slots">Current slots for Lex session.</param>
        /// <returns>Response of the Lex Bot.</returns>
        protected LexResponse Delegate(IDictionary<string, string> sessionAttributes, IDictionary<string, string> slots)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Delegate",
                    Slots = slots
                }
            };
        }

        /// <summary>
        /// Creates a response to Elicit an slot to the user.
        /// </summary>
        /// <param name="sessionAttributes">Session attributes to save</param>
        /// <param name="intentName">Intent to elicit.</param>
        /// <param name="slots">Current slots for Lex session.</param>
        /// <param name="slotToElicit">Slot to elicit.</param>
        /// <param name="message">Current slots for Lex session.</param>
        /// <returns>Response of the Lex Bot.</returns>
        protected LexResponse ElicitSlot(IDictionary<string, string> sessionAttributes, string intentName, 
            IDictionary<string, string> slots, string slotToElicit, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ElicitSlot",
                    IntentName = intentName,
                    Slots = slots,
                    SlotToElicit = slotToElicit,
                    Message = message
                }
            };
        }

        /// <summary>
        /// Creates a response asking he user for confirmation to an intent action.
        /// </summary>
        /// <param name="sessionAttributes">Session attributes to save</param>
        /// <param name="intentName">Intent to confirm.</param>
        /// <param name="slots">Current slots for Lex session.</param>
        /// <param name="message">Current slots for Lex session.</param>
        /// <returns>Response of the Lex Bot.</returns>
        protected LexResponse ConfirmIntent(IDictionary<string, string> sessionAttributes, string intentName, 
            IDictionary<string, string> slots, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ConfirmIntent",
                    IntentName = intentName,
                    Slots = slots,
                    Message = message
                }
            };
        }
        #endregion

        #region Helpers to deal with Serialization
        /// <summary>
        /// Serializes a chemical compound request to JSON.
        /// </summary>
        /// <param name="compoundRequest">Instances of the chemical request data.</param>
        /// <returns>REquest serialized as JSON string.</returns>
        protected string SerializeChemicalRequest(ChemicalCompoundRequest compoundRequest)
        {
            return JsonConvert.SerializeObject(compoundRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        /// <summary>
        /// Deserialize a Compound information request from a JSON string.
        /// </summary>
        /// <param name="json">JSON string containing the Compound Information request.</param>
        /// <returns>Instance of a ChemicaLCompoundRequest.</returns>
        protected ChemicalCompoundRequest DeserializeChemicalRequest(string json)
        {
            return JsonConvert.DeserializeObject<ChemicalCompoundRequest>(json);
        }

        /// <summary>
        /// Creates a new instance of a generic attachment.
        /// </summary>
        /// <param name="title">Attachment title.</param>
        /// <param name="subTitle">Attachment subTitle.</param>
        /// <param name="imageUrl">URL of the image to attach. If it is null.</param>
        /// <param name="attachmentLinkUrl">Link to Attach.</param>
        /// <returns>New instance of a generic attachment.</returns>
        protected LexResponse.LexGenericAttachments CreateGenericAttachment(ChemicalResponseAttachment attachmentData)
        {
            LexResponse.LexGenericAttachments attachment = new LexResponse.LexGenericAttachments();
            attachment.Title = attachmentData.Title;
            if (!string.IsNullOrEmpty(attachmentData.ImageURL)) attachment.ImageUrl = attachmentData.ImageURL;
            if (!string.IsNullOrEmpty(attachmentData.AttachmentURL)) attachment.AttachmentLinkUrl = attachmentData.AttachmentURL;

            return attachment;
        }

        /// <summary>
        /// Appends attachments to response Card.
        /// </summary>
        /// <param name="responseCard">Instance of Response Card.</param>
        /// <param name="attachements">Generic Attachments</param>
        protected void AddAttachmentsToResponseCard(LexResponse.LexResponseCard responseCard, params LexResponse.LexGenericAttachments[] attachements)
        {
            if(responseCard.GenericAttachments == null)
            {
                responseCard.GenericAttachments = new List<LexResponse.LexGenericAttachments>();
            }

            foreach(var item in attachements)
            {
                responseCard.GenericAttachments.Add(item);
            }
        }

        #endregion

    }
}
