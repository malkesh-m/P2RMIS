using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    /// <summary>
    /// Builder class constructs the Web Model for the PanelAssignmentModel model.
    /// </summary>
    internal class PanelAssignmentModalModelBuilder : ModelBuilderBase
    {
        #region Construction & Setup
        public const string IPM = "Integration Panel Member";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public PanelAssignmentModalModelBuilder(IUnitOfWork unitOfWork, int sessionPanelId, int userId)
            : base(unitOfWork, userId)
        {
            this.SessionPanelId = sessionPanelId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        private int SessionPanelId { get; set; }
        /// <summary>
        /// SessionPanel entity
        /// </summary>
        private SessionPanel SessionPanelEntity { get; set; }
        #endregion
        #region PanelAssignmentModalModelBuilder services
        /// <summary>
        /// Build the model
        /// </summary>
        public override IBuiltModel Build()
        {
            IPanelAssignmentModalModel model = new PanelAssignmentModalModel();
            PopulateWithHeaderData(model);
            PopulateWithGridData(model);
            return model;
        }
        /// <summary>
        /// Populates the PanelAssignmentModalModel with the data for the header sections.
        /// <param name="model">Web model for the modal</param>
        /// </summary>
        /// <param name="model">Web model for the modal</param>
        protected void PopulateWithGridData(IPanelAssignmentModalModel model)
        {
            User userEntity = GetThisUser(this.UserId);
            int panelUserAssignmentRetrievalLimit = DetermineRetrievalLimit(ConfigManager.PanelManagementAssignmentRetrievalLimit);
            int panelUserPotentialAssignmentRetrievalLimit = DetermineRetrievalLimit(ConfigManager.PanelManagementPotentialAssignmentRetrievalLimit);
            //
            // Only retrieve PanelUserAssigments that are within the last fiscalYearLimit
            PopulateGridData(model, userEntity.PanelUserAssignments.Where(x => x.FiscalYearGreaterThan(panelUserAssignmentRetrievalLimit)).ToList<IPanelReviewer>());

            // Likewise only retrieve PanelUserPotentialAssignments that are within the last fiscalYearLimit.  Excluding
            //
            PopulateGridData(model, userEntity.PanelUserPotentialAssignments.Where(x => (!x.RecruitedFlag) && (x.FiscalYearGreaterThan(panelUserPotentialAssignmentRetrievalLimit))).ToList<IPanelReviewer>());
            PopulateProgramGridData(model, userEntity.ProgramUserAssignments);
            //
            // and now we sort the entries
            //
            model.Sort();
        }

        private void PopulateProgramGridData(IPanelAssignmentModalModel model, ICollection<Dal.ProgramUserAssignment> programUserAssignments)
        {
            //
            // Just iterate over the entries in the collection and add them to the model of history entries.
            //
            foreach (Dal.ProgramUserAssignment entity in programUserAssignments)
            {
                var gridModel = new ProgramUserAssignmentHistory();
                var getProgramYear = UnitOfWork.ProgramYearRepository.GetByID(entity.ProgramYearId);
                gridModel.SetProgramHistory(entity.ProgramYearId,
                                            entity.ClientParticipantTypeId);
                gridModel.SetProgramYear(getProgramYear.ClientProgram.ProgramAbbreviation,
                                            getProgramYear.Year);
                model.ProgramParticipationHistory.Add(gridModel);
            }
        }

        /// <summary>
        /// Determines the retrieval limit.
        /// </summary>
        /// <returns>Retrieval limit</returns>
        protected int DetermineRetrievalLimit(int fiscalYearLimit)
        {
            return this.SessionPanelEntity.GetNumericFiscalYear() - fiscalYearLimit;
        }

        /// <summary>
        /// Populates the ParticipationHistory property with one or more ParticipationHistoryModel models
        /// representing a user's participation history
        /// </summary>
        /// <param name="model">PanelAssignmentModelModel</param>
        /// <param name="collection">Collection of IPanelReviewer entities</param>
        protected void PopulateGridData(IPanelAssignmentModalModel model, ICollection<IPanelReviewer> collection)
        {
            //
            // Just iterate over the entries in the collection and add them to the model of history entries.
            //
            foreach (IPanelReviewer entity in collection)
            {
                var gridModel = new ParticipationHistoryModel();
                //
                // Start with the client information
                //
                gridModel.SetClientStuff(entity.SessionPanel.ClientAbbreviation(),
                                         entity.SessionPanel.GetFiscalYear(),
                                         entity.SessionPanel.GetProgramAbbreviation(),
                                         entity.SessionPanel.PanelAbbreviation,
                                         entity.SessionPanel.SessionPanelId,
                                         entity.SessionPanel.GetProgramYearId());
                //
                // Now set the meeting information
                //
                gridModel.SetMeetingStuff(entity.SessionPanel.MeetingType(),
                                          entity.SessionPanel.EndDate);
                //
                // Now set the reviewer information.  A note about the negation on IsAssigned().  IsAssigned()
                // always returns true  for PanelUserAssignment entities.  Hence the negation.  Also any assigned
                // PanelUserPotentialAssignments are filtered out before being set to this method.
                //
                gridModel.SetReviewerStuff(entity.ClientRole?.RoleName,
                                           entity.ClientParticipantType?.ParticipantTypeAbbreviation,
                                           entity.Level(),
                                           entity.IsRegistrationComplete(),
                                           entity.ParticipationMethod?.ParticipationMethodLabel,
                                           !entity.IsAssigned());
                // Set SRO list
                gridModel.SetSroList(entity.SessionPanel.PanelUserAssignments.Where(x => x.ClientParticipantType.IsSro())
                    .Select(y => new Tuple<string, string, string>(y.User.FirstName(),
                                     y.User.LastName(),
                                     y.User.PrimaryUserEmailAddress())).ToList());
                //
                // Now we just add the grid entry to the model model
                //
                model.ParticipationHistory.Add(gridModel);
            }
        }
        /// <summary>
        /// Populates the PanelAssignmentModalModel with the data for the header sections.
        /// </summary>
        /// <param name="model">Web model for the modal</param>
        /// <returns></returns>
        public IPanelAssignmentModalModel PopulateWithHeaderData(IPanelAssignmentModalModel model)
        {
            SessionPanelEntity = GetThisSessionPanel(this.SessionPanelId);
            //
            // Get whatever assignment types the user has
            //
            PanelUserAssignment panelUserAssignmentEntity = SessionPanelEntity.PanelUserAssignments.FirstOrDefault(x => x.UserId == this.UserId);
            PanelUserPotentialAssignment panelUserPotentialAssignmentEntity = SessionPanelEntity.PanelUserPotentialAssignments.FirstOrDefault(x => x.UserId == this.UserId);
            //
            // then update the model with the values
            //
            PopulateReviewerAssignment(model, panelUserPotentialAssignmentEntity);
            PopulateReviewerAssignment(model, panelUserAssignmentEntity);
            //
            // And finally handle the special case where a potential reviewer has been assigned
            //
            UpdatePotentialDate(model, panelUserPotentialAssignmentEntity);

            return model;
        }
        /// <summary>
        /// Set the PotentialAddedDate when the reviewer has been assigned.
        /// </summary>
        /// <param name="model">Web model for the modal</param>
        /// <param name="panelUserPotentialAssignmentEntity">PanelUserPotentialAssignment entity</param>
        protected void UpdatePotentialDate(IPanelAssignmentModalModel model, PanelUserPotentialAssignment panelUserPotentialAssignmentEntity)
        {
            if ((panelUserPotentialAssignmentEntity != null) && (panelUserPotentialAssignmentEntity.IsRecruited()))
            {
                model.SetPotentialDate(panelUserPotentialAssignmentEntity.CreatedDate);
            }
        }
        /// <summary>
        /// Populate the model
        /// </summary>
        /// <param name="model">Web model for the modal</param>
        /// <param name="entity">PanelUserPotentialAssignment or PanelUserAssignment assignment</param>
        /// <returns>Updated model</returns>
        protected IPanelAssignmentModalModel PopulateReviewerAssignment(IPanelAssignmentModalModel model, IPanelReviewer entity)
        {
            if ((entity != null) && (!entity.IsRecruited()))
            {
                //
                // First thing we need to do is set the indicator if the reviewer is assigned.  This is because
                // the model set up will use that to determine other values.
                //
                model.SetAssignment(entity.IsAssigned());
                //
                // set the stuff for the header section on the right hand side
                //
                model.SetHeaderStuff(entity.CreatedDate, SessionPanelEntity.MeetingType());
                //
                // Set the participation drop downs
                //
                model.SetParticipationDropdowns(entity.ClientRoleId, entity.BoxClientParticipationTypeId());
                //
                // Set the participation method indicator & level
                //
                model.SetParticipationMethod(entity.BoxParticipationMethodId());
                model.SetParticipationLevelAndClientApproval(entity.Level(), entity.ClientApprovalFlag);
                //
                // And set the remaining indexes
                //
                model.SetIndexesStuff(this.SessionPanelId, this.UserId, entity.EntityId());
            }
            return model;
        }
    }
    #endregion
}