// Dot Net Namespaces
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Composes a message to understand the help messages. 
    /// </summary>
    internal class HelpSearchCriteria : HelpComposer
    {

        #region Overrides
        /// <summary>
        /// Help to understand the Search criterias accepted by ChemBot.
        /// </summary>
        /// <param name="helpRequest">Help Requests</param>
        /// <returns>User Friendly message with the help contents.</returns>
        public override HelpResponse ComposeHelpMessage(string helpRequest)
        {
            if(helpRequest == ChemBotText.HELP_CRITERIA_SEARCH_CRITERIA)
            {
                StringBuilder userMessage = CreateHelpWithHeader(ChemBotText.TEXT_SUPPORTED_SEARCH_CRITERIA_INTRO);
                userMessage.Append(ChemBotText.TEXT_SUPPORTED_SEARCH_CRITERIA_END);

                return new HelpResponse
                {
                    Successful = true,
                    Result = userMessage
                };
            }

            return base.ComposeHelpMessage(helpRequest);
        }

        #endregion

    }
}
