// Dot Net Namespaces
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;
using ChemBotFunctions.Validation;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Generates help message to specify the user the supported Identifier types.
    /// </summary>
    internal class HelpIDTypes : HelpComposer
    {
        #region Overrides

        /// <summary>
        /// Composes a help message describing the supported identifier types.
        /// </summary>
        /// <param name="helpRequest">Help request.</param>
        /// <returns>Instance of a help response with an user friendly message.</returns>
        public override HelpResponse ComposeHelpMessage(string helpRequest)
        {
            if(helpRequest == ChemBotText.HELP_CRITERIA_ID_TYPE)
            {
                StringBuilder userMessage = CreateHelpWithHeader(ChemBotText.TEXT_SUPPORTED_ID_TYPES);
                userMessage.AppendLine(ChemBotText.TEXT_SUPPORTED_ID_TYPES_END);

                foreach (var item in TypeValidators.VALID_SEARCH_CRITERIA)
                {
                    userMessage.AppendLine($"_{item}_\n");
                }

                return new HelpResponse
                {
                    Result = userMessage,
                    Successful = true
                };
            }

            return base.ComposeHelpMessage(helpRequest);
        }

        #endregion
    }
}
