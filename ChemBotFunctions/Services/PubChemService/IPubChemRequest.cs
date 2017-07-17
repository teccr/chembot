// Dot Net Namespaces
using System;
using System.IO;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService
{
    /// <summary>
    /// Interface require to consume the PubChem Web Service.
    /// </summary>
    public interface IPubChemRequest : IDisposable
    {
        /// <summary>
        /// Get chemical properties from one of the valid compound IDs supported by PubChem.
        /// </summary>
        /// <param name="compoundId">Value of the compound identifier use in the search. Example: glucose, CCCC, 5084, etc.</param>
        /// <param name="compoundIdType">Compound identifier used in the search. Example: sid, cid, name, smiles, etc.</param>
        /// <param name="chemicalProperties"></param>
        /// <returns>JSON response of the PubChem service.</returns>
        JObject GetChemicalPropertiesFromId(string compoundId, string compoundIdType, string chemicalProperties);

        /// <summary>
        /// Get the synonyms for the compound specified in the search.
        /// </summary>
        /// <param name="compoundId">Value of the compound identifier use in the search. Example: glucose, CCCC, 5084, etc.</param>
        /// <param name="compoundIdType">Compound identifier used in the search. Example: sid, cid, name, smiles, etc.</param>
        /// <returns>JSON response of the PubChem service.</returns>
        JObject GetSynonymsFromId(string compoundId, string compoundIdType);
    }
}
