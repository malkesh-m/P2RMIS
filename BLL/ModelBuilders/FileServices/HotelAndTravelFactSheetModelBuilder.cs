using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.ModelBuilders.FileServices
{
    /// <summary>
    /// Retrieve an Hotel & Travel FactSheet file.
    /// </summary>
    internal class HotelAndTravelFactSheetModelBuilder: LegacyFileModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="programMeetingInformationId">ProgramMeetingInformation entity identifier</param>
        public HotelAndTravelFactSheetModelBuilder(IUnitOfWork unitOfWork, int programMeetingInformationId) :
            base(unitOfWork)
        {
            this.ProgramMeetingInformationId = programMeetingInformationId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The ProgramYear entity identifier
        /// </summary>
        private int ProgramMeetingInformationId { get; set; }
        #endregion
        #region Overrides
        /// <summary>
        /// Retrieve the URL for the file resource.
        /// </summary>
        protected override void RetrieveLegacyUrl()
        {
            //
            // To get the fact sheet location on the P2RMIS legacy server we get the partial location the entity then build
            // a URI and just dump out the URL
            //
            ProgramMeetingInformation programMeetingInformationEntity = UnitOfWork.ProgramMeetingInformationRepository.GetByID(ProgramMeetingInformationId);

            UriBuilder builder = new UriBuilder(ConfigManager.P2RMISv1Server);
            builder.Path = programMeetingInformationEntity.FileLocation;
            this.Url = builder.Uri.ToString();

            return;
        }
        #endregion
    }
}
