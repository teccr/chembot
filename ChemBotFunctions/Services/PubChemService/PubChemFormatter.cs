// Dot Net Namespaces
using System;
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Services.PubChemService.TokenFormatters;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService
{
    /// <summary>
    /// Service used to format the result of a PubChem request.
    /// </summary>
    public class PubChemFormatter : IPubChemFormatter
    {
        #region Pub Chem Constants
        /// <summary>
        /// Name of the JSON property in PubCHem results storing a list of chemical properties.
        /// </summary>
        const string PC_PROPERTY_TABLE = "PropertyTable";

        /// <summary>
        /// Name of the JSON property in PubCHem results storing a list of chemical properties, it is a child of the main chemical property element.
        /// </summary>
        const string PC_PROPERTIES = "Properties";

        /// <summary>
        /// Compound identifier string.
        /// </summary>
        const string PC_IDENTIFIER = "CID";

        /// <summary>
        /// Name of the JSON property in PubChem results storing a list of synonyms.
        /// </summary>
        const string PC_INFORMATION_LIST = "InformationList";

        /// <summary>
        /// Name of the JSON property in PubChem results storing a list of synonyms. It is a child of the InformationList JSON element.
        /// </summary>
        const string PC_INFORMATION = "Information";
        #endregion

        #region Variables
        /// <summary>
        /// Instances of formattters linked in a Chain of responsability to format PuChem responses.
        /// </summary>
        PubChemTokenFormatter formatters;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructors.
        /// </summary>
        public PubChemFormatter()
        {
            InitFormatters();
        }
        #endregion

        #region IPubChemFormatter Implementation

        /// <summary>
        /// Format Chembot response into a user friendly message.
        /// </summary>
        /// <param name="pubChemResponse">JSON response of the PubChem service.</param>
        /// <returns>Return isntance of FormatResult with friendly user message and Compound ID.</returns>
        FormatResult IPubChemFormatter.ProcessMessage(JObject pubChemResponse)
        {
            FormatResult result = new FormatResult();
            StringBuilder messageBuilder = new StringBuilder();

            foreach(var property in pubChemResponse)
            {
                string propertyName = property.Key;
                JToken token = property.Value;

                result = formatters.FormatMessage(propertyName, token, messageBuilder);
            }
            return result;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Intializes state of the current class.
        /// </summary>
        private void InitFormatters()
        {
            formatters = new PropertiesTableFormatter();
            var synonymsFormatter = new SynonymsFormatter();
            formatters.SetNextFormatter(synonymsFormatter);
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    ((IDisposable)formatters).Dispose();
                    formatters = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PubChemFormatter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
