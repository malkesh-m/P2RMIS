using System;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Wrapper containing the set order of review changeable values.
    /// </summary>
    public class SetOrderOfReviewToSave: ToSaveObject
    {
        #region Constructor & Setup
        /// <summary>
        /// Enforce only a single way to crate this object.
        /// </summary>
        private SetOrderOfReviewToSave() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logNumber">Application's log number</param>
        /// <param name="newOrder">New order (1, 2,3 ...)</param>
        /// <param name="isTriage">Indicator that the application was triaged.  In which case old/new order does not apply.</param>
        public SetOrderOfReviewToSave(string logNumber, int? newOrder, bool isTriaged)
        {
            this.LogNumber = logNumber;
            this.NewOrder = newOrder;
            this.IsTriaged = isTriaged;
        }
        /// <summary>
        /// Application log number
        /// </summary>
        public string LogNumber { get; private set; }
        /// <summary>
        /// New sort order
        /// </summary>
        public int? NewOrder { get; private set; }
        /// <summary>
        /// Indicator if the application is triaged.
        /// </summary>
        public bool IsTriaged { get; private set; }
        #endregion
        #region Validation
        /// <summary>
        /// VAlidateion method for this ToSave object:
        ///  - Log Number is not null, empty or whitespace
        ///  - Cannot supply both an order & set triage to true
        ///  - Did not supply an order & set triaged to true
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(LogNumber))
            {
                string message = string.Format("SetOrderOfReviewToSave.Validate()  Invalid LogNumber detected; null, empty string or whitespace");
                throw new ArgumentException(message);
            }
            if ((NewOrder > 0) && (IsTriaged))
            {
                string message = string.Format("SetOrderOfReviewToSave.Validate()  Invalid combination detected NewOrder: {0} IsTriaged: {1}", NewOrder, IsTriaged);
                throw new ArgumentException(message);
            }
            if ((NewOrder == null) && (!IsTriaged))
            {
                string message = string.Format("SetOrderOfReviewToSave.Validate()  Invalid combination detected NewOrder: {0} IsTriaged: {1}", NewOrder, IsTriaged);
                throw new ArgumentException(message);
            }
        }
        #endregion
    }
}
