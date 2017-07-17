// Dot Net Namespaces
using System.Text.RegularExpressions;

// ChemBot Namespaces
using ChemBotFunctions.Validation;

// Third Party Namespaces
using Newtonsoft.Json;

namespace ChemBotFunctions.Data
{
    /// <summary>
    /// Class reprenting the data for a chemical compound information request.
    /// </summary>
    public class ChemicalCompoundRequest
    {
        #region Properties
        /// <summary>
        /// Type of identifier used as search criteria for the web service. 
        /// </summary>
        /// <remarks>
        /// The number of identifiers supported are defined in the PubChem Web service. As it is, the service only supports one identifier.
        /// </remarks>
        /// <example>
        /// cid, sid, name, etc.
        /// </example>
        public string IdentifierType { get; set; }

        /// <summary>
        /// Search criteria entered by the user. 
        /// </summary>
        /// <remarks>
        /// This will depend on the identifier specified by the user. Unfortunately the web service is pretty specific and supports exact searches for exact criterias.
        /// </remarks>
        /// <example>
        /// Name: Benzene, Butane, Glucose, 
        /// </example>
        public string SearchCriteria { get; set; }

        /// <summary>
        /// Chemical property to retrieve from the service.
        /// </summary>
        /// <remarks>
        /// A set of properties are supported by the web service. it is possible to add many of them separated by commas. 
        /// </remarks>
        /// <example>
        /// Molecular weight, Molecular formula, canonical smiles, etc.
        /// </example>
        public string PropertyToRetrieve { get; set; }

        /// <summary>
        /// Attachment sent from Slack. 
        /// </summary>
        /// <remarks>
        /// The attachment will be transform to an AWS Response Card. 
        /// </remarks>
        /// <example>
        /// Supported values: None, Structure, SDF.
        /// </example>
        public string Attachment { get; set; }

        /// <summary>
        /// Marks if the lambda function previously failed trying to execute this request.
        /// </summary>
        /// <remarks>
        /// This property will be use to allow the user to send request that failed becuase of server busy errors.
        /// </remarks>
        public bool FailedInPreviousExecution { get; set; }
        #endregion

        #region Non-Serializable helpers
        /// <summary>
        /// Will return true if there is any value for the required files in the lambda function.
        /// </summary>
        [JsonIgnore]
        public bool HasRequiredFields
        {
            get
            {
                return !string.IsNullOrEmpty(this.IdentifierType)
                    && !string.IsNullOrEmpty(this.PropertyToRetrieve)
                    && !string.IsNullOrEmpty(this.SearchCriteria);
            }
        }

        /// <summary>
        /// Converts the current permutation of the chemical property in the value used in pubchem search.
        /// </summary>
        /// <example>
        /// MW into MolecularWeight
        /// Mol Weight into MolecularWeight
        /// </example>
        [JsonIgnore]
        public string PubChemChemicalPropertyName
        {
            get
            {

                if (string.IsNullOrEmpty(this.PropertyToRetrieve)) return null;

                var originalProperty = Regex.Replace(this.PropertyToRetrieve.Trim().ToLower(), @"\s+", " ");

                if (!TypeValidators.PERMUTATIONS_CHEMICAL_PROPERTIES.ContainsKey(originalProperty)) return null;

                return TypeValidators.PERMUTATIONS_CHEMICAL_PROPERTIES[originalProperty];
            }
        }
        #endregion

    }
}
