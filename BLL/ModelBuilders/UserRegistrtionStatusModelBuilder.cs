using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.ProgramRegistration;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.ModelBuilders
{
    /// <summary>
    /// Build a UserRegistrtionStatusModel which answers the questions
    /// 1) Does the user have any incomplete registrations for the ProgramYear?
    /// 2) Does the user have any registrations?
    /// </summary>
    internal class UserRegistrtionStatusModelBuilder: ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public UserRegistrtionStatusModelBuilder(IUnitOfWork unitOfWork, int programYearId, int userId)
            : base(unitOfWork, userId)
        {
            this.ProgramYearId = programYearId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        protected int ProgramYearId  { get; private set; }
        #endregion
        #region UserRegistrtionStatusModelBuilder services
        /// <summary>
        /// Build the model
        /// </summary>
        public override IBuiltModel Build()
        {

            //
            // First we collect all of the ProgramPanels for the specified ProgramYear
            //
            var result = UnitOfWork.PanelUserRegistrationRepository.Select().Where(x => x.PanelUserAssignment.UserId == UserId && x.PanelUserAssignment.SessionPanel.ProgramPanels.Any(y => y.ProgramYearId == ProgramYearId)).ToList();
            //
            // This will indicate if the user has no registrations in the ProgramYear
            //
            bool noRegistrations = result.Count() == 0;
            //
            //  Then we collect all of their PanelUserRegistrations which are complete.
            //
            bool oneCompleteRegistration = result.Any(x => x.RegistrationCompletedDate != null);
            //
            // Then we just build a model to return & bob is your uncle!
            //
            IBuiltModel model = new UserRegistrtionStatusModel(oneCompleteRegistration, noRegistrations, ProgramYearId, UserId);



            return model;
        }
        #endregion
    }
}
