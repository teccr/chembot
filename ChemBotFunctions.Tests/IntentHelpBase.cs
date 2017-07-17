// Dot Net Namespaces
using System.IO;

// AWS Namespaces
using Amazon.Lambda.LexEvents;
using Amazon.Lambda.TestUtilities;

// Third Party Namespaces
using Newtonsoft.Json;

namespace ChemBotFunctions.Tests
{
    public class IntentHelpBase
    {
        #region Overridable

        /// <summary>
        /// Folder containing files.
        /// </summary>
        protected virtual string BaseFolder
        {
            get
            {
                return "";
            }
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Executes a local Lambda function and returns a lex response.
        /// </summary>
        /// <param name="jsonFileWithLexRequest">Lex Request as JSON</param>
        /// <returns>LexResponse object.</returns>
        protected LexResponse GetLexResponse(string jsonFileWithLexRequest)
        {
            var json = File.ReadAllText(BaseFolder + System.IO.Path.DirectorySeparatorChar + jsonFileWithLexRequest);
            var lexEvent = JsonConvert.DeserializeObject<LexEvent>(json);
            var function = new Function();
            var context = new TestLambdaContext();
            var response = function.FunctionHandler(lexEvent, context);

            return response;
        }

        /// <summary>
        /// Gets the first attachment in the LexResponse.
        /// </summary>
        /// <param name="response">Instance of Lex Response</param>
        /// <returns>Instance of the first generic attachment.</returns>
        protected LexResponse.LexGenericAttachments GetFirstAttachment(LexResponse response)
        {
            if (response.DialogAction.ResponseCard != null
                && response.DialogAction.ResponseCard.GenericAttachments != null
                && response.DialogAction.ResponseCard.GenericAttachments.Count > 0)
            {
                return response.DialogAction.ResponseCard.GenericAttachments[0];
            }
            return null;
        }

        #endregion
    }
}
