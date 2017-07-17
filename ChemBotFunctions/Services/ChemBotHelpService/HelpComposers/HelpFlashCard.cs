// Dot Net Namespaces
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Returns a description of the ChemBot flash card feature.
    /// </summary>
    internal class HelpFlashCard : HelpComposer
    {
        #region Overrides

        /// <summary>
        /// Returns a small description about what are Flash Cards in the application.
        /// </summary>
        /// <param name="helpRequest">Help request</param>
        /// <returns>Help response.</returns>
        public override HelpResponse ComposeHelpMessage(string helpRequest)
        {
            if(helpRequest == ChemBotText.HELP_CRITERIA_FLASH_CARD)
            {
                StringBuilder userMessage = CreateHelpWithHeader(ChemBotText.TEXT_SUPPORTED_FLASHCARD_INTRO);
                userMessage.AppendLine(ChemBotText.TEXT_SUPPORTED_FLASHCARD_MIDDLE);
                userMessage.AppendLine(ChemBotText.TEXT_SUPPORTED_FLASHCARD_END);

                return new HelpResponse {
                    Result = userMessage,
                    Successful = true
                };
            }

            return base.ComposeHelpMessage(helpRequest);
        }

        #endregion
    }
}
