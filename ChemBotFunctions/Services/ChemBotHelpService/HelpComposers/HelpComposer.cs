// Dot Net Namespaces
using System;
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;

namespace ChemBotFunctions.Services.ChemBotHelpService.HelpComposers
{
    /// <summary>
    /// Base class for a help request operation. 
    /// </summary>
    internal abstract class HelpComposer : IDisposable
    {
        #region Variables

        /// <summary>
        /// Reference to the next help composer in the Chain.
        /// </summary>
        private HelpComposer nextComposer;

        #endregion

        #region Class Services

        /// <summary>
        /// Sets the next help composer in the chain of responsability. If this node cannot manage the help request, the next node will try and so on.
        /// </summary>
        /// <param name="nextComposerInstance">Instance of the formatter to register.</param>
        public void SetNextComposer(HelpComposer nextComposerInstance)
        {
            nextComposer = nextComposerInstance;
        }

        /// <summary>
        /// Process help request and composes a user friendly message.
        /// </summary>
        /// <param name="helpRequest">Help request string.</param>
        /// <returns>Help response.</returns>
        public virtual HelpResponse ComposeHelpMessage(string helpRequest)
        {
            if (nextComposer != null)
            {
                return nextComposer.ComposeHelpMessage(helpRequest);
            }

            StringBuilder textResult = new StringBuilder();
            textResult.AppendLine($"{ChemBotText.TEXT_REQUEST_NOT_PROCESSED} for {helpRequest}.");

            return new HelpResponse
            {
                Successful = false,
                Result = textResult
            };
        }

        /// <summary>
        /// Initializes a help message.
        /// </summary>
        /// <param name="header">Header to attach to the message after the default title.</param>
        /// <returns>New initialized message.</returns>
        public StringBuilder CreateHelpWithHeader(string header)
        {
            StringBuilder textResult = new StringBuilder();
            textResult.AppendLine(ChemBotText.TEXT_HELP_TITLE);
            textResult.AppendLine(header);
            return textResult;
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
                    if(nextComposer != null)
                    {
                        ((IDisposable)nextComposer).Dispose();
                        nextComposer = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~HelpComposer() {
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
