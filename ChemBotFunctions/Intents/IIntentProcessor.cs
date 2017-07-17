// AWS Namespaces
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

namespace ChemBotFunctions.Intents
{
    /// <summary>
    /// Represents an intent processor that the Lambda function will invoke to process the event.
    /// </summary>
    public interface IIntentProcessor
    {
        /// <summary>
        /// Main method for processing the Lex event for the intent.
        /// </summary>
        /// <param name="lexEvent">Instance of the Lex Event.</param>
        /// <param name="context">Context of the Lambda Execution.</param>
        /// <returns>Lex response for the bot.</returns>
        LexResponse Process(LexEvent lexEvent, ILambdaContext context);
    }
}
