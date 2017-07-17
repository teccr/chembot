// Dot Net 
using System.Text;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Contains the response of a help request. It will allow the services to know if the request failed or not.
    /// </summary>
    internal class HelpResponse
    {
        /// <summary>
        /// True: Successful request, False: Failed request.
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// User friendly message for the help request.
        /// </summary>
        public StringBuilder Result { get; set; }
    }
}
