// Dot Net Namespaces
using System;
using System.Text;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService.TokenFormatters
{
    /// <summary>
    /// Base class for a PubChem result formatter.
    /// </summary>
    public abstract class PubChemTokenFormatter : IDisposable
    {
        #region Properties

        /// <summary>
        /// Compound Identifier.
        /// </summary>
        public string CID { get; set; }

        #endregion

        #region Variables

        /// <summary>
        /// Reference to the next formatter in the Chain.
        /// </summary>
        private PubChemTokenFormatter nextFormatter;

        #endregion


        #region Class services

        /// <summary>
        /// Sets the next formatter in the chain of responsability. If this node cannot manage the PubChem output, the next node will try and so on.
        /// </summary>
        /// <param name="nextFormatterInstance">Instance of the formatter to register.</param>
        public void SetNextFormatter(PubChemTokenFormatter nextFormatterInstance)
        {
            nextFormatter = nextFormatterInstance;
        }

        /// <summary>
        /// Apply format to a PubChem result. It will process the JSON output and will generate an user friendly message.
        /// </summary>
        /// <param name="propertyName">Property name to look up for.</param>
        /// <param name="pubChemResponseFragment">JSON fragment with the result from PubChem.</param>
        /// <param name="messageBuilder">Instance of the StringBuilder that will contain the friendly error message.</param>
        /// <returns></returns>
        public virtual FormatResult FormatMessage(string propertyName, JToken pubChemResponseFragment, StringBuilder messageBuilder)
        {
            if (nextFormatter != null)
            {
                return nextFormatter.FormatMessage(propertyName, pubChemResponseFragment, messageBuilder);
            }

            return new FormatResult
            {
                CID = "",
                TextResult = messageBuilder
            };
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
                    ((IDisposable)nextFormatter).Dispose();
                    nextFormatter = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PubChemTokenFormatter() {
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