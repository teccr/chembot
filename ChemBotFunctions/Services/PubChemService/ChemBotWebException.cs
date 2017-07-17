// Dot Net Namespaces
using System.Net.Http;

namespace ChemBotFunctions.Services.PubChemService
{
    /// <summary>
    /// Exception used to wrap http codes with results from a chemical web request.
    /// </summary>
    public class ChemBotWebException : HttpRequestException
    {
        #region Properties
        
        /// <summary>
        /// Numerical code for the Http status returned for this exception.
        /// </summary>
        public int HttpStatusCode { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor supporting HTTP status codes.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="httpStatusCode">Http status code.</param>
        public ChemBotWebException(string message, int httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }
        #endregion

    }
}
