using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    public partial class ProgramCycleDeliverable : IStandardDateFields
    {
        #region setter methods
        /// <summary>
        /// Marks the deliverable qc'd.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void MarkQc(int userId)
        {
            this.QcFlag = true;
            this.QcDate = GlobalProperties.P2rmisDateTimeNow;
            this.QcUserId = userId;
        }

        /// <summary>
        /// Regenerates the deliverable.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="deliverableFile">The deliverable file.</param>
        public void RegenerateDeliverable(int userId, string deliverableFile, byte[] qcFile)
        {
            this.GeneratedDate = GlobalProperties.P2rmisDateTimeNow;
            this.GeneratedUserId = userId;
            this.QcDate = null;
            this.QcFlag = false;
            this.QcUserId = null;
            this.DeliverableFile = deliverableFile;
            this.QcDataFile = qcFile;
        }

        /// <summary>
        /// Populates the specified program year identifier.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDataDeliverableId">The client data deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="deliverableFile">The deliverable file.</param>
        /// <param name="qcFile">The qc file.</param>
        public void Populate(int programYearId, int? receiptCycle, int clientDataDeliverableId, int userId, string deliverableFile, byte[] qcFile)
        {
            this.ProgramYearId = programYearId;
            this.ReceiptCycle = receiptCycle;
            this.ClientDataDeliverableId = clientDataDeliverableId;
            this.GeneratedDate = GlobalProperties.P2rmisDateTimeNow;
            this.GeneratedUserId = userId;
            this.DeliverableFile = deliverableFile;
            this.QcDataFile = qcFile;
        }
        #endregion
    }
}
