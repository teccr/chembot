// Dot Net Namespaces
using System.Collections.Generic;
using System.Text;

namespace ChemBotFunctions.Data
{
    /// <summary>
    /// This class represents the response of the PubChem Service. It conmtains data required to build the Lex Bot response.
    /// </summary>
    public class ChemicalResponseData
    {
        #region Properties
        /// <summary>
        /// String builder containing th result of the operation against the PubChem Service.
        /// </summary>
        /// <remarks>
        /// The PubChem Service execution will format the message to be user friendly. Exception messages will be logged separetely in the AWS Lambda log utilities.
        /// </remarks>
        public StringBuilder ResultText { get; set; }

        /// <summary>
        /// Current state of the response.
        /// </summary>
        public ResponseState State { get; set; }

        /// <summary>
        /// Error message coming from the .NET Exception catched by the code.
        /// </summary>
        /// <remarks>
        /// This message will be logged into the AWS Lambda logging utilities.
        /// </remarks>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Stack Trace of the .NET Exception catched by the code.
        /// </summary>
        /// <remarks>
        /// This message will be logged into the AWS Lambda logging utilities.
        /// </remarks>
        public string ExceptionStackTrace { get; set; }

        /// <summary>
        /// Indicates if the bot can ask the user for Retry the execution.
        /// </summary>
        /// <remarks>
        /// It will be set to true if the busy was too busy to process the request. Other type of errors will be set to false.
        /// </remarks>
        public bool CanRetry { get; set; }

        /// <summary>
        /// List of Attachments for this message.
        /// </summary>
        public List<ChemicalResponseAttachment> Attachments { get; set; }

        /// <summary>
        /// Identifier of the compound.
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// Reference to the Service request. 
        /// </summary>
        public ChemicalCompoundRequest Request { get; set; }
        #endregion

        #region Constructors
        public ChemicalResponseData()
        {
            Init();
        }

        public ChemicalResponseData(ChemicalCompoundRequest request)
        {
            Init();
            Request = request;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Initializes all default data for the response data.
        /// </summary>
        private void Init()
        {
            ResultText = new StringBuilder();
            State = ResponseState.NONE;
            ExceptionMessage = null;
            ExceptionStackTrace = null;
            Attachments = new List<ChemicalResponseAttachment>();
        }

        #endregion
    }
}
