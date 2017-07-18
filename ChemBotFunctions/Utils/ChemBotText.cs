// Dot Net Namespaces
using System.Collections.Immutable;
using System.Collections.Generic;

namespace ChemBotFunctions.Utils
{
    /// <summary>
    /// This class contains constants for text in ChemBot.
    /// </summary>
    /// <remarks>
    /// At some point in the future using different languages for the bot will require to localize all strings in this class. 
    /// A service connected to Dynamo or S3 files could be the best way to go. For now we are not sure how to handle multiple languages with Lex backends.
    /// </remarks>
    internal class ChemBotText
    {
        #region Intents
        /// <summary>
        /// Name of the intent used to resolve Chemical Compound information requests from Slack.
        /// </summary>
        internal const string INTENT_COMPOUND_INFO = "ChemicalCompoundInformation";

        /// <summary>
        /// Name of the intent used to resolve requests for help.
        /// </summary>
        internal const string INTENT_BOT_HELP = "ChemBotHelpInformation";

        /// <summary>
        /// Fullfilment string for a failed operation.
        /// </summary>
        internal const string INTENT_FULLFILMENT_FAILED = "Failed";

        /// <summary>
        /// Fullfilment string for a successful operation.
        /// </summary>
        internal const string INTENT_FULLFILMENT_CONFIRMED = "Confirmed";

        /// <summary>
        /// Fullfilment string for a successful operation.
        /// </summary>
        internal const string INTENT_FULLFILMENT_FULFILLED = "Fulfilled";
        #endregion

        #region General Settings

        /// <summary>
        /// Type of the content that the Lambda function will send to Lex.
        /// </summary>
        internal const string MESSAGE_CONTENT_TYPE = "PlainText";

        #region Flash Card Configuration
        /// <summary>
        /// Indicates which property will start the request for a flash card.
        /// </summary>
        internal const string FLASH_CARD_PROPERTY = "FlashCard";

        /// <summary>
        /// This constant contains the properties to be show as part of a flash card and how they will be send in the request URL.
        /// </summary>
        /// <remarks>
        /// It needs to be according to the PUbChem specification to return multiple properties.
        /// </remarks>
        /// <example>
        /// Multiple properties: ExactMass,CanonicalSmiles,MolecularWeight
        /// </example>
        internal const string FLASH_CARD_URL_TABLE_PROPERTIES = "MolecularFormula,MolecularWeight,IUPACName,CanonicalSMILES,IsomericSMILES,InChI,ExactMass,XLogP,MonoisotopicMass";

        /// <summary>
        /// Unique PubChem Units for mass.
        /// </summary>
        internal const string PUBCHEM_UNIQUE_MASS_UNITS = "g/mol";
        #endregion

        #region Chemical Request Intent Labels

        internal const string SLOT_LBL_COMPOUND_TO_SEARCH = "Search Criteria";
        internal const string SLOT_LBL_PROPERTY_TO_SEARCH = "Chemical Property";
        internal const string SLOT_LBL_ID_TYPE = "Identifier Type";

        #endregion

        #endregion

        #region Error Messages
        /// <summary>
        /// Generic Error message to show  in .NET exceptions. 
        /// </summary>
        internal const string MESSAGE_GENERIC_REQUEST_ERROR = "It was not possible to execute your request.";

        /// <summary>
        /// List of HTTP error codes and its respective error messages. 
        /// </summary>
        internal static ImmutableDictionary<int, string> HttpErrors = ImmutableDictionary.ToImmutableDictionary<int,string>(
                new Dictionary<int, string>
                {
                    { 400, "Request is improperly formed. Wrong parameters." },
                    { 404, "The search criteria was not found." }, // "The input record was not found."
                    { 405, "Invalid request." },
                    { 504, "The request timed out, from server overload or too broad a request. Please try again later." },
                    { 501, "The requested operation has not (yet) been implemented by the server." },
                    { 500, "Some problem on the server side. Please try again later." }
                }
            );

        /// <summary>
        /// Code to identify PubChem server to busy errors.
        /// </summary>
        /// <remarks>
        /// The PubChem service is a free and public service. Sometimes it will return a server too busy error and the client can retry the query to get the result again.
        /// This error code allows to identify which HTTP code allows retry of the query.
        /// </remarks>
        internal const int SERVER_BUSY_ERROR_CODE = 504;

        /// <summary>
        /// Error message to let the user know 
        /// </summary>
        internal const string ERROR_TALK_TO_YOU = "I did not understand the request. Talk to you soon.";

        /// <summary>
        /// First part of the message when the bot needs to elicit a slot.
        /// </summary>
        internal const string ERROR_ELICIT_ROOT_MESSAGE = "Please enter the information for";

        // Validaton Errors
        internal const string ERROR_ID_NOT_SUPPORTED = "We currently do not support {0} as a valid Identifier Type. Can you try again?";
        internal const string ERROR_PROPERTY_NOT_SUPPORTED = "We currently do not support {0} as a valid Chemical Property to search. Can you try again?";
        internal const string ERROR_SEARCH_CRITERIA_EMPTY = "The search criteria cannot be empty. Please try again.";

        #endregion

        #region Attachments

        internal const string ATTACHMENT_SDF = "sdf";

        internal const string ATTACHMENT_SDF_TITLE = "Download SDF";

        internal const string ATTACHMENT_STRUCTURE = "structure";

        internal const string ATTACHMENT_STRUCTURE_TITLE = "Chemical Structure";

        internal const string ATTACHMENT_PUBCHEM_LINK = "PubChemLink";

        internal const string ATTACHMENT_PUBCHEM_LINK_TITLE = "PubChem Reference";
        #endregion

        #region Configuration
        /// <summary>
        /// Based domain for the PubChem services.
        /// </summary>
        internal const string PUBCHEM_DOMAIN = "https://pubchem.ncbi.nlm.nih.gov/";

        /// <summary>
        /// Base PubChem Server URL.
        /// </summary>
        internal const string PUBCHEM_SERVER = PUBCHEM_DOMAIN + "rest/pug/";
        #endregion

        #region Help Intent
        // The following are the possible options for help.
        internal const string HELP_CRITERIA_CHEMICAL_PROPERTIES = "Chemical Properties";
        internal const string HELP_CRITERIA_SEARCH_CRITERIA = "Search Criteria";
        internal const string HELP_CRITERIA_ID_TYPE = "ID Type";
        internal const string HELP_CRITERIA_FLASH_CARD = "Flash Card";

        internal const string TEXT_REQUEST_NOT_PROCESSED = "It was not possible to process your help request";
        internal const string TEXT_HELP_TITLE = "*ChemBot Help*\n";
        internal const string TEXT_SUPPORTED_PROPERTIES_INTRO = "ChemBot allows you to retrieve chemical properties from a compound by querying PubChem Web services.\n";
        internal const string TEXT_SUPPORTED_PROPERTIES = "ChemBot supports the following Chemical Property requests:\n";
        internal const string TEXT_SUPPORTED_PROPERTIES_END = "To retrieve more information you can try attaching the SDF file during the request.";
        internal const string TEXT_SUPPORTED_SEARCH_CRITERIA_INTRO = "Use compound names, Cid and other values supported by the different Identifier types in PubChem.\n";
        internal const string TEXT_SUPPORTED_SEARCH_CRITERIA_END = "Some examples: glucose, 2662, acetic acid, CC(=O)O, etc.\n";
        internal const string TEXT_SUPPORTED_ID_TYPES = "ChemBot uses the PubChem web services to execute exact searches.\n";
        internal const string TEXT_SUPPORTED_ID_TYPES_END = "ChemBot supports the following identifiers as search criteria: \n";
        internal const string TEXT_SUPPORTED_FLASHCARD_INTRO = "Flash Card is a quick summary of important compound properties.\n";
        internal const string TEXT_SUPPORTED_FLASHCARD_MIDDLE = "The cards contain the image structure, a link to the SDF file and a small set of chemical properties.\n";
        internal const string TEXT_SUPPORTED_FLASHCARD_END = "Do not hesitate to download SDF file to explore all the information in the compound.\n";
        #endregion
    }
}
