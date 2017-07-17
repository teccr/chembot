// Dot Net Namespaces
using Xunit;

namespace ChemBotFunctions.Tests
{
    public class ChemicalCompoundHelpIntentTest : IntentHelpBase
    {
        #region Help

        protected override string BaseFolder => "JSON_Help";

        #endregion

        #region Help Intent Tests

        [Fact]
        public void GetSearchCriteriaHelp()
        {
            var response = GetLexResponse("ChemBot_Help_Search_Criteria.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
        }

        [Fact]
        public void GetIDTypesHelp()
        {
            var response = GetLexResponse("ChemBot_Help_ID_Types.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
        }

        [Fact]
        public void GetChemicalPropertiesHelp()
        {
            var response = GetLexResponse("ChemBot_Help_Chemical_Properties.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
        }

        [Fact]
        public void GetFlashCardsHelp()
        {
            var response = GetLexResponse("ChemBot_Help_Flash_Card.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
        }

        [Fact]
        public void GetHelpFailure()
        {
            var response = GetLexResponse("ChemBot_Help_Failure.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Failed", response.DialogAction.FulfillmentState);
        }

        #endregion
    }
}
