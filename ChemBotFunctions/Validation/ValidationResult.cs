// AWS Namespaces
using Amazon.Lambda.LexEvents;

namespace ChemBotFunctions.Validation
{
    /// <summary>
    /// This class contains the results of validating the current state of all slot values. This is used to send information
    /// back to the user to fix bad slot values.
    /// </summary>
    /// <remarks>
    /// The validations in this class should be moved to a separate server in the future. Because of the slow pace in features rollout in
    /// PubChem it is not a high priority right now. However, it needs to be done in the near future.
    /// </remarks>
    public class ValidationResult
    {
        #region Static Members
        /// <summary>
        /// Result representing a valid set of slots values.
        /// </summary>
        public static readonly ValidationResult VALID_RESULT = new ValidationResult(true, null, null);
        #endregion

        #region Properties
        /// <summary>
        /// If the slot values are currently correct.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Which slot value is invalid so the user can correct the value.
        /// </summary>
        public string ViolationSlot { get; }

        /// <summary>
        /// User Friendly name for slot which value is invalid so the user can correct the value.
        /// </summary>
        public string ViolationSlotFriendlyName { get; }

        /// <summary>
        /// The message explaining to the user what is wrong with the slot value.
        /// </summary>
        public LexResponse.LexMessage Message { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a validation result.
        /// </summary>
        /// <param name="isValid">Is result valid or not.</param>
        /// <param name="violationSlot">Slot failing validation.</param>
        public ValidationResult(bool isValid, string violationSlot, string message)
        {
            this.IsValid = isValid;
            this.ViolationSlot = violationSlot;

            if (!string.IsNullOrEmpty(message))
            {
                this.Message = new LexResponse.LexMessage { ContentType = "PlainText", Content = message };
            }
        }
        #endregion
    }
}
