// Dot net Namespaces
using System.Text;

namespace ChemBotFunctions.Services.PubChemService.TokenFormatters
{
    /// <summary>
    /// Result of a format operation, it contains information about the user friendly message and the compound ID retrieved in the search.
    /// </summary>
    public class FormatResult
    {
        #region Properties
        /// <summary>
        /// Compound Identifier
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// User friendly message.
        /// </summary>
        public StringBuilder TextResult { get; set; }
        #endregion
    }
}
