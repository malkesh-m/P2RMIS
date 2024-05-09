namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Description of data returned representing a single cycle for a program/fiscal year.
    /// </summary>
    public class CycleModel: ICycleModel
    {
        /// <summary>
        /// The receipt cycle.
        /// </summary>
        private int _receiptCycle;
        public int ReceiptCycle 
        {
            get { return _receiptCycle; } 
            set
            {
                this._receiptCycle = value;
                this._cycle = _receiptCycle.ToString();
            } 
        }
        /// <summary>
        /// The receipt cycle as a string.
        /// </summary>
        private string _cycle = string.Empty;
        public string Cycle
        {
            get { return _cycle; }
        }
    }
}
