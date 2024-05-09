using System;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelStageStep object. 
    /// </summary>
    public partial class PanelStageStep : IStandardDateFields
    {
        /// <summary>
        /// Populates the ReOpen & ReClose date/time & updates the Modified date/time fields
        /// </summary>
        /// <param name="reOpenDate">New reopen open date/time</param>
        /// <param name="closeDate">New reopen close date/time</param>
        /// <param name="userId">User entity identifier</param>
        /// <remarks>Needs unit tests</remarks>
        public void UpdateDates(DateTime reOpenDate, DateTime closeDate, int userId)
        {
            this.ReOpenDate = reOpenDate;
            this.ReCloseDate = closeDate;

            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Returns the StepType's StepTypeName
        /// </summary>
        /// <returns>StepTypeName</returns>
        /// <remarks>Needs unit tests</remarks>
        public string StypeTypeName()
        {
            return this.StepType.StepTypeName;
        }
        /// <summary>
        /// Determines if the PanelStageStep is in the Final phase
        /// </summary>
        /// <returns>True if the PanelPhaseStep is Final; false otherwise</returns>
        /// <remarks>Needs unit tests</remarks>
        public bool IsFinalPhase()
        {
            return (this.StepTypeId == StepType.Indexes.Final);
        }
        /// <summary>
        /// Determines if the PanelStageStep is in the meeting phase
        /// </summary>
        /// <returns>True if the PanelPhaseStep is meeting phase; false otherwise</returns>
        /// <remarks>Needs unit tests</remarks>
        public bool IsMeetingPhase()
        {
            return (this.StepTypeId == StepType.Indexes.Meeting);
        }
        /// <summary>
        /// Indicates if the stage step is open.  (Current date time is within the
        /// StartDate and EndDate of the PanelStageStep.
        /// </summary>
        /// <returns>True if the PanelStageStep is open; false otherwise</returns>
        public bool IsStageStepOpen()
        {
            return ((StartDate.HasValue) && (EndDate.HasValue)) ?
                ViewHelpers.WithInDates(GlobalProperties.P2rmisDateTimeNow, StartDate.Value, EndDate.Value, ReOpenDate, ReCloseDate)
                : false;
        }
        /// <summary>
        /// Indicates if the PanelStageStep can have an MOD.  (The phase has a specific StepType)
        /// </summary>
        /// <param name="panelStageStep">PanelStageStep entity</param>
        /// <returns>True if the current phase is an MOD phase' false otherwise</returns>
        public static bool IsModPhase(PanelStageStep panelStageStep)
        {
            return (panelStageStep != null) ? panelStageStep.IsFinalPhase() : false;
        }
        /// <summary>
        /// Indicates if the MOD is active (PanelStageStep is a final phase step & there is at least one comment)
        /// </summary>
        /// <param name="panelStageStep">PanelStageStep entity</param>
        /// <param name="isPanelStageStepOpen">PanelStageStep open indicator</param>
        /// <param name="isModActive">Indicates if the step is active</param>
        /// <returns>True if the MOD is active; false otherwise</returns>
        public static bool IsModActive(PanelStageStep panelStageStep, bool isPanelStageStepOpen, bool isModActive)
        {
            return ((panelStageStep != null) && (panelStageStep.IsFinalPhase())) ? (isPanelStageStepOpen & isModActive) : false;
        }
        /// <summary>
        /// Indicates if the MOD is Ready.  (MOD can be started)
        /// </summary>
        /// <param name="panelStageStep">PanelStageStep entity</param>
        /// <param name="isPanelStageStepOpen">PanelStageStep open indicator</param>
        /// <param name="isModActive">Indicates if the step is active</param>
        /// <returns>True if the MOD can be started; false otherwise</returns>
        public static bool IsModReady(PanelStageStep panelStageStep, bool isPanelStageStepOpen, bool isModActive)
        {
            return ((panelStageStep != null) && (panelStageStep.IsFinalPhase())) ? (isPanelStageStepOpen & !isModActive) : false;
        }
        /// <summary>
        /// Indicates if the MOD is closed.  Current date is past the phase end date or reclose date if 
        /// the phase has been reopened.
        /// </summary>
        /// <param name="panelStageStep">PanelStageStep entity</param>
        /// <param name="isPanelStageStepOpen"></param>
        /// <returns>True if the MOD phase is close; false otherwise</returns>
        public static bool IsModClosed(PanelStageStep panelStageStep, bool isPanelStageStepOpen)
        {
            return ((panelStageStep != null) && (panelStageStep.IsFinalPhase())) ? !isPanelStageStepOpen : false;
        }
        /// <summary>
        /// Determine if the on line discussion is done.
        /// </summary>
        /// <param name="panelStageStep">PanelStageStep entity</param>
        /// <returns>True if the panel stage step is within the dates or before; false if Now is after the last close date.</returns>
        public static bool IsModDone(PanelStageStep panelStageStep)
        {
            bool result = false;
            if (panelStageStep != null)
            {
                //
                // We are making an assumption here.  
                //
                if (panelStageStep.ReCloseDate.HasValue)
                {
                    result = GlobalProperties.P2rmisDateTimeNow > panelStageStep.ReCloseDate.Value;
                }
                else
                {
                    result = GlobalProperties.P2rmisDateTimeNow > panelStageStep.EndDate.Value;
                }
            }
            return result;
        }
        /// <summary>
        /// Determines whether the specified step type identifier is mod.
        /// </summary>
        /// <param name="stepTypeId">The step type identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified step type identifier is mod; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMod(int stepTypeId)
        {
            return stepTypeId == StepType.Indexes.Final;
        }

        /// <summary>
        /// Retrieve the ApplicationStageStep for the PanelApplication
        /// </summary>
        /// <param name="panelApplicationEntityId">PanelApplication entity identifier</param>
        /// <returns>ApplicationStageStep entity</returns>
        public ApplicationStageStep RetrieveApplicationStageStep(int panelApplicationEntityId)
        {
            //
            // Out of the ApplicationStageSteps
            //
            return this.ApplicationStageSteps.
                //
                // Locate the one that matches the PanelApplication
                //
                FirstOrDefault(x => x.ApplicationStage.PanelApplicationId == panelApplicationEntityId);
        }
        /// <summary>
        /// Determines if the specified phase is open but not ReOpened.
        /// </summary>
        /// <param name="stepTypeId"></param>
        /// <returns>True if the PanelStageStep is open but not reopened; false otherwise</returns>
        public bool IsStageStepOpenButNotReopened()
        {
            return (
                    (DateTime.Compare(StartDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) < 0) &&
                    (DateTime.Compare(EndDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) > 0)
                    );
        }
    }
}
