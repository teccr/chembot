// Dot Net Namespaces
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;
using ChemBotFunctions.Validation;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Process a chemical property help request.
    /// </summary>
    internal class HelpChemicalProperties : HelpComposer
    {
        #region Overrides
        /// <summary>
        /// Returns a lits of all chemical properties supported by ChemBot.
        /// </summary>
        /// <param name="helpRequest">Help request</param>
        /// <returns>Instance of response.</returns>
        public override HelpResponse ComposeHelpMessage(string helpRequest)
        {
            if(helpRequest == ChemBotText.HELP_CRITERIA_CHEMICAL_PROPERTIES)
            {
                StringBuilder userMessage = CreateHelpWithHeader(ChemBotText.TEXT_SUPPORTED_PROPERTIES_INTRO);
                userMessage.AppendLine(ChemBotText.TEXT_SUPPORTED_PROPERTIES);

                foreach(var item in TypeValidators.FRIENDLY_USER_NAMES_CHEMICAL_PROPERTIES)
                {
                    userMessage.AppendLine($"{item.Value} (_{item.Key}_).\n");
                }

                userMessage.AppendLine(ChemBotText.TEXT_SUPPORTED_PROPERTIES_END);

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
