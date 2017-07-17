// Dot Net Namespaces
using System.Collections.Generic;

// ChemBot Namespaces
using ChemBotFunctions.Services.ChemBotHelpService;

// AWS Namespaces
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;
using ChemBotFunctions.Utils;

namespace ChemBotFunctions.Intents
{
    /// <summary>
    /// This intent will take care of the help requests.
    /// </summary>
    public class ChemicalCompoundInfoHelp : BaseIntent
    {
        #region Intent Slots
        /// <summary>
        /// Slots containg the help option choose by the user.
        /// </summary>
        private const string SLOT_HELP_CRITERIA = "helpCriteria";

        #endregion

        #region Overrides
        /// <summary>
        /// Process the help intent.
        /// </summary>
        /// <param name="lexEvent">Instance of the Lex Event.</param>
        /// <param name="context">Context of the Lambda function.</param>
        /// <returns>LexResponse containg the help.</returns>
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            // Get data from the Lex intent
            var slots = lexEvent.CurrentIntent.Slots;
            var sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();

            // Choose action and return result
            LexResponse response = null;
            var helpCriteria = slots.ContainsKey(SLOT_HELP_CRITERIA) ? slots[SLOT_HELP_CRITERIA] : null;
            
            // Process the help Intent
            using(IHelpRequestService helpService = new HelpRequestService())
            {
                var helpRawResponse = helpService.ResolveHelpRequest(helpCriteria);
                var message = new LexResponse.LexMessage
                {
                    ContentType = ChemBotText.MESSAGE_CONTENT_TYPE,
                    Content = helpRawResponse.Result.ToString()
                };

                if (helpRawResponse.Successful)
                {
                    response = Close(sessionAttributes, ChemBotText.INTENT_FULLFILMENT_FULFILLED, message);
                }
                else
                {
                    response = Close(sessionAttributes, ChemBotText.INTENT_FULLFILMENT_FAILED, message);
                }
            }

            return response;
        }

        #endregion
    }
}
