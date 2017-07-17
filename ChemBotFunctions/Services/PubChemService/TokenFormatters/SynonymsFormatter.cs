// Dot net Namespaces
using System.Text;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService.TokenFormatters
{
    /// <summary>
    /// Formatter for PubChem requests returning synonyms. 
    /// </summary>
    public class SynonymsFormatter : PubChemTokenFormatter
    {
        #region Overrides
        /// <summary>
        /// Override for the FormatMessage function. It will detect if the results are synonyms to process them.
        /// </summary>
        /// <param name="propertyName">Name of the property to show.</param>
        /// <param name="pubChemResponseFragment">Current JSON Token to be evaluated.</param>
        /// <param name="messageBuilder">Instance of a StringBuilder containing the final message for the user.</param>
        /// <returns>Formatted result with metadata.</returns>
        public override FormatResult FormatMessage(string propertyName, JToken pubChemResponseFragment, StringBuilder messageBuilder)
        {
            if (propertyName == "InformationList")
            {
                var propertiesInResponse = pubChemResponseFragment.First.First.First;

                foreach(var chemicalProperty in propertiesInResponse.Values<JProperty>())
                {
                    if (chemicalProperty.Value is JArray)
                    {
                        ProcessArray(chemicalProperty.Value as JArray, messageBuilder);
                    }
                    else
                    {
                        ProcessProperty(chemicalProperty);
                    }
                }

                return new FormatResult
                {
                    CID = this.CID,
                    TextResult = messageBuilder
                };
            }

            return base.FormatMessage(propertyName, pubChemResponseFragment, messageBuilder);
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Process a property and adds its value to the message.
        /// </summary>
        /// <param name="property">Instance of JSON property.</param>
        private void ProcessProperty(JProperty property)
        {
            if (property.Name.ToUpper() == "CID")
            {
                this.CID = property.Value.ToString();
            }
        }

        /// <summary>
        /// Add JSON array values to the synonyms message.
        /// </summary>
        /// <param name="array">JArray instance</param>
        /// <param name="message">Message builder.</param>
        private void ProcessArray(JArray array, StringBuilder message)
        {
            message.AppendLine("Synonyms:");
            foreach (var synonym in array)
            {
                message.AppendLine($"{synonym.ToString()}\n");
            }
        }

        #endregion
    }
}
