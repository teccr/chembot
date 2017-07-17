// Dot Net Namespaces
using System;
using Xunit;

namespace ChemBotFunctions.Tests
{
    /// <summary>
    /// Tests for Chemical Compound info Intents.
    /// </summary>
    public class ChemicalCompoundInfoIntentTest : IntentHelpBase
    {
        #region Overrides

        protected override string BaseFolder => "JSON_Chem";

        #endregion

        #region ChemBot Intent Testing
        [Fact]
        public void GetGlucoseMolecularWeight()
        {
            var response = GetLexResponse("ChemBot_MW_Request.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
            Assert.Contains("g/mol", response.DialogAction.Message.Content);
        }

        [Fact]
        public void GetGlucoseSynonyms()
        {
            var response = GetLexResponse("ChemBot_Synonyms_Request.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
            Assert.Contains("D-Glucose", response.DialogAction.Message.Content);
        }

        [Fact]
        public void GetGlucoseFlashCard()
        {
            var response = GetLexResponse("ChemBot_MW_FlashCard_Request.json");

            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
            Assert.Contains("g/mol", response.DialogAction.Message.Content);
        }

        [Fact]
        public void GetGlucoseStructure()
        {
            var response = GetLexResponse("ChemBot_MW_Structure_Request.json");
            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
            Assert.Contains("g/mol", response.DialogAction.Message.Content);

            var structure = GetFirstAttachment(response);
            Assert.NotNull(structure);
            if (structure != null) Assert.EndsWith( "PNG", structure.ImageUrl, StringComparison.Ordinal );
        }

        [Fact]
        public void GetGlucoseSDF()
        {
            var response = GetLexResponse("ChemBot_MW_SDF_Request.json");
            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Fulfilled", response.DialogAction.FulfillmentState);
            Assert.Contains("g/mol", response.DialogAction.Message.Content);

            var sdf = GetFirstAttachment(response);
            Assert.NotNull(sdf);
            if (sdf != null) Assert.EndsWith("SDF", sdf.AttachmentLinkUrl, StringComparison.Ordinal);
        }

        #endregion

        #region Negative Testing

        [Fact]
        public void GetGlucoseElicitSlot()
        {
            var response = GetLexResponse("ChemBot_MW_Elicit_Request.json");
            Assert.Equal("ElicitSlot", response.DialogAction.Type);
        }

        [Fact]
        public void GetGlucoseErrorWrongSearchCriteria()
        {
            var response = GetLexResponse("ChemBot_Wrong_SearchCriteria.json");
            Assert.Equal("Close", response.DialogAction.Type);
            Assert.Equal("Failed", response.DialogAction.FulfillmentState);
            Assert.Equal("The search criteria was not found.\r\n", response.DialogAction.Message.Content);
        }

        [Fact]
        public void GetGlucoseErrorWrongIdentifierType()
        {
            var response = GetLexResponse("ChemBot_Wrong_IdType.json");
            Assert.Equal("ElicitSlot", response.DialogAction.Type);
            Assert.Equal("We currently do not support wrong as a valid Identifier Type. Can you try again?", response.DialogAction.Message.Content);
        }

        [Fact]
        public void GetGlucoseErrorWrongProperty()
        {
            var response = GetLexResponse("ChemBot_Wrong_ChemicalProperty.json");
            Assert.Equal("ElicitSlot", response.DialogAction.Type);
            Assert.Equal("We currently do not support wrong as a valid Chemical Property to search. Can you try again?", response.DialogAction.Message.Content);
        }

        #endregion
    }
}
