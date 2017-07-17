// Dot Net Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;

// Third Party Namespaces
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService
{
    /// <summary>
    /// Service consuming the PubChem web service.
    /// </summary>
    public class PubChemRequestService : IPubChemRequest
    {
        #region IPubChemRequest Implementation

        /// <summary>
        /// Get chemical properties from one of the valid compound IDs supported by PubChem.
        /// </summary>
        /// <param name="compoundId">Value of the compound identifier use in the search. Example: glucose, CCCC, 5084, etc.</param>
        /// <param name="compoundIdType">Compound identifier used in the search. Example: sid, cid, name, smiles, etc.</param>
        /// <param name="chemicalProperties"></param>
        /// <returns>JSON response of the PubChem service.</returns>
        JObject IPubChemRequest.GetChemicalPropertiesFromId(string compoundId, string compoundIdType, string chemicalProperties)
        {
            return GetGenericResponse(
                $"{GetUrlWithSearchCriteria(compoundId, compoundIdType)}property/{chemicalProperties}/json");
        }

        /// <summary>
        /// Get the synonyms for the compound specified in the search.
        /// </summary>
        /// <param name="compoundId">Value of the compound identifier use in the search. Example: glucose, CCCC, 5084, etc.</param>
        /// <param name="compoundIdType">Compound identifier used in the search. Example: sid, cid, name, smiles, etc.</param>
        /// <returns>JSON response of the PubChem service.</returns>
        JObject IPubChemRequest.GetSynonymsFromId(string compoundId, string compoundIdType)
        {
            return GetGenericResponse(
                $"{GetUrlWithSearchCriteria(compoundId, compoundIdType)}synonyms/json");
        }

        /// <summary>
        /// Freeup resources.
        /// </summary>
        void IDisposable.Dispose()
        {

        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get PubChem service URL with search criteria appended.
        /// </summary>
        /// <param name="compoundId">Value of the compound identifier use in the search. Example: glucose, CCCC, 5084, etc.</param>
        /// <param name="compoundIdType">Compound identifier used in the search. Example: sid, cid, name, smiles, etc.</param>
        /// <returns>Partial URL of the PubChem service with search criterias.</returns>
        private string GetUrlWithSearchCriteria(string compoundId, string compoundIdType)
        {
            return $"compound/{compoundIdType}/{compoundId}/";
        }

        /// <summary>
        /// Executes an HTTP call to the PubChem server.
        /// </summary>
        /// <param name="urlParameters">Parameters of the HTTP call.</param>
        /// <returns>Response of the PubChem Service.</returns>
        private HttpResponseMessage GetResponseFromService(string urlParameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ChemBotText.PUBCHEM_SERVER);
                client.DefaultRequestHeaders.Add("Connection", "close");
                var response = client.GetAsync(urlParameters).Result;
                
                if(response.StatusCode  != HttpStatusCode.OK)
                {
                    int statusCode = (int)response.StatusCode;
                    if (ChemBotText.HttpErrors.ContainsKey(statusCode))
                    {
                        var errorMessage = ChemBotText.HttpErrors[statusCode];
                        throw new ChemBotWebException(errorMessage, statusCode);
                    }

                    response.EnsureSuccessStatusCode();
                }
                return response;
            }
        }

        /// <summary>
        /// Gets JSON response from PubChem service.
        /// </summary>
        /// <param name="urlParameters">URL parameters for the search.</param>
        /// <returns>JSON response.</returns>
        private JObject GetGenericResponse(string urlParameters)
        {
            var response = GetResponseFromService(urlParameters);
            var stringResult = response.Content.ReadAsStringAsync().Result;
            var rawPubChem = JsonConvert.DeserializeObject<JObject>(stringResult);
            return rawPubChem;
        }

        /// <summary>
        /// Utility to get file responses from PubChem.
        /// </summary>
        /// <param name="urlParameters">URL parameters for the search.</param>
        /// <returns>IO Stream of the file coming from PubChem.</returns>
        private Stream GetFileResponse(string urlParameters)
        {
            var response = GetResponseFromService(urlParameters);
            return response.Content.ReadAsStreamAsync().Result;
        }

        /// <summary>
        /// Validate Chemical properties in a string.
        /// </summary>
        /// <param name="validationList">List with content to validate.</param>
        /// <param name="stringsToValidate">String containing the Chemical properties.</param>
        /// <param name="valueType">Kind of criteria to validate (identifier, search criteria, chemical property)</param>
        private void ValidateStringWithList(List<string> validationList, string stringsToValidate, string valueType)
        {
            if (string.IsNullOrEmpty(stringsToValidate) || string.IsNullOrWhiteSpace(stringsToValidate))
            {
                throw new WebException($"There are no properties to extract from the PubChem API.", WebExceptionStatus.ProtocolError);
            }

            if (stringsToValidate.Contains(","))
            {
                string[] chemicalProperties = stringsToValidate.Split(',');
                StringBuilder builder = new StringBuilder();
                int ocurrencies = 0;

                foreach (var chemicalProperty in chemicalProperties)
                {
                    if (!validationList.Contains(chemicalProperty))
                    {
                        if (ocurrencies > 0) builder.Append(",");
                        ocurrencies += 1;
                        builder.Append(chemicalProperty);
                    }
                }
            }
            else
            {
                if (!validationList.Contains(stringsToValidate))
                {
                    throw new WebException($"The {valueType} with value {stringsToValidate} is not recognized by ChemBot", WebExceptionStatus.ProtocolError);
                }
            }
        }
        #endregion
    }
}
