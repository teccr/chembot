// Dot Net Namespaces
using System;

// AWS Namespaces
using Amazon.Lambda.LexEvents;

// ChemBot Namespaces
using ChemBotFunctions.Services.ChemBotHelpService.HelpComposers;

namespace ChemBotFunctions.Services.ChemBotHelpService
{
    /// <summary>
    /// Provides a modular way to resovle help requests.
    /// </summary>
    internal interface IHelpRequestService : IDisposable
    {
        /// <summary>
        /// Returns the correct Lex response accordingly to the criteria requested by the user.
        /// </summary>
        /// <param name="helpRequest">Help Request sent by the user.</param>
        /// <returns>Lex Response for the help request.</returns>
        HelpResponse ResolveHelpRequest(string helpRequest);
    }
}
