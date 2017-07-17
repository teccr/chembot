// Dot Net Namespaces
using System;

// AWS Namespaces
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

// ChemBot Namespaces.
using ChemBotFunctions.Intents;
using ChemBotFunctions.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ChemBotFunctions
{
    /// <summary>
    /// This class is the entry point for the AWS Lambda function.
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Then entry point for the Lambda function that looks at the current intent and calls 
        /// the appropriate intent process.
        /// </summary>
        /// <param name="lexEvent">AWS Lex Event information.</param>
        /// <param name="context">Context of the lambda function.</param>
        /// <returns>Response for the Lex bot.</returns>
        public LexResponse FunctionHandler(LexEvent lexEvent, ILambdaContext context)
        {
            IIntentProcessor process;

            switch (lexEvent.CurrentIntent.Name)
            {
                case ChemBotText.INTENT_COMPOUND_INFO:
                    process = new ChemicalCompoundInfoIntent();
                    break;
                case ChemBotText.INTENT_BOT_HELP:
                    process = new ChemicalCompoundInfoHelp();
                    break;
                default:
                    throw new Exception($"Intent with name {lexEvent.CurrentIntent.Name} not supported");
            }


            return process.Process(lexEvent, context);
        }

    }
}
