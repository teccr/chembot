// Dot Net Namespaces
using System.Text;

// ChemBot Namespaces
using ChemBotFunctions.Utils;
using ChemBotFunctions.Validation;

// Third Party Namespaces
using Newtonsoft.Json.Linq;

namespace ChemBotFunctions.Services.PubChemService.TokenFormatters
{
    #region Overrides
    /// <summary>
    /// Formatter for PubChem requests returning chemical properties data. 
    /// </summary>
    public class PropertiesTableFormatter : PubChemTokenFormatter
    {
        /// <summary>
        /// Override for the FormatMessage function. It will detect if the results are Chemical properties to process them.
        /// </summary>
        /// <param name="propertyName">Name of the property to show.</param>
        /// <param name="pubChemResponseFragment">Current JSON Token to be evaluated.</param>
        /// <param name="messageBuilder">Instance of a StringBuilder containing the final message for the user.</param>
        /// <returns>Formatted result with metadata.</returns>
        public override FormatResult FormatMessage(string propertyName, JToken pubChemResponseFragment, StringBuilder messageBuilder)
        {
            if (propertyName == "PropertyTable")
            {
                var propertiesInResponse = pubChemResponseFragment.First as JProperty;

                foreach (JProperty chemicalProperty in propertiesInResponse.Value.First)
                {
                    if (chemicalProperty.Name.ToUpper() == "CID")
                    {
                        this.CID = chemicalProperty.Value.ToString();
                    }
                    else
                    {
                        string propertyData = $"*{TypeValidators.FRIENDLY_USER_NAMES_CHEMICAL_PROPERTIES[chemicalProperty.Name]}*:";

                        if (TypeValidators.MASS_PUBCHEM_PROPERTIES.Contains(chemicalProperty.Name))
                        {
                            propertyData = $"{propertyData} {chemicalProperty.Value.ToString()} {ChemBotText.PUBCHEM_UNIQUE_MASS_UNITS} \n";
                        }
                        else
                        {
                            propertyData = $"{propertyData} {chemicalProperty.Value.ToString()}\n";
                        }
                        messageBuilder.AppendLine(propertyData);
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
    }
    #endregion
}
