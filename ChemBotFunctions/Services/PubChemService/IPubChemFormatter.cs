// Dot Net Namespaces
using System;

// ChemBot Namespaces
using ChemBotFunctions.Services.PubChemService.TokenFormatters;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService
{
    /// <summary>
    /// Service to format PubChem request results.
    /// </summary>
    public interface IPubChemFormatter : IDisposable
    {
        /// <summary>
        /// Operation to format PubChem request results.
        /// </summary>
        /// <param name="pubChemResponse">JSON response from PubChem.</param>
        /// <returns>Instance of FormatResult with data about the search.</returns>
        FormatResult ProcessMessage(JObject pubChemResponse);
    }
}
