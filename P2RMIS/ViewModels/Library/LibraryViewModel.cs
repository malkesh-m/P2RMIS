using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.Library;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Library view model
    /// </summary>
    public class LibraryViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LibraryViewModel()
        {
            this.ProgramYears = new List<ListEntry>();
            this.TrainingDocuments = new List<TrainingDocumentViewModel>();
        }
        /// <summary>
        /// Initializes a new instance of the LibraryViewModel class.
        /// </summary>
        /// <param name="programYears">The program years.</param>
        /// <param name="programYearId">The selected program year identifier</param>
        /// <param name="accessRestricted">Flag to indicate if access is restricted</param>
        /// <param name="isSroRta">Flag to indicate if the current user is SRO or RTA</param>
        /// <param name="trainingDocuments">Training documents</param>
        public LibraryViewModel(List<IProgramYearModel> programYears, int programYearId, bool areUsersRegistrationIncomplete,
                bool accessRestricted, bool isSroRta, List<ITrainingDocumentModel> trainingDocuments)
        {
            ProgramYears = programYears.ConvertAll(x => new ListEntry(x.ProgramYearId, GetProgramYearDisplay(x.FY, x.ProgramDescription, x.ProgramAbbreviation)));
            ProgramYearId = programYearId;            
            TrainingDocuments = trainingDocuments.ConvertAll(x => new TrainingDocumentViewModel(x));
            if (areUsersRegistrationIncomplete)
            {
                IncompleteRegistrationMessage = MessageService.TrainingIncompleteRegistrations;
            }
            if (accessRestricted)
            {
                AccessRestrictedMessage = isSroRta ? MessageService.TrainingNoProgramAssignment : MessageService.TrainingAccessRestriction;
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSroRta">Flag to indicate if the current user is SRO or RTA</param>
        public LibraryViewModel(bool isSroRta): this()
        {
            AccessRestrictedMessage = isSroRta ? MessageService.TrainingNoProgramAssignment : MessageService.TrainingNoProgramAssignment;
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of program years
        /// </summary>
        public List<ListEntry> ProgramYears { get; private set; }
        /// <summary>
        /// The selected program year identifier
        /// </summary>
        public int ProgramYearId { get; private set; }
        /// <summary>
        /// Gets the incomplete registration message.
        /// </summary>
        /// <value>
        /// The incomplete registration message.
        /// </value>
        public string IncompleteRegistrationMessage { get; private set; }
        /// <summary>
        /// Gets the access restricted message.
        /// </summary>
        /// <value>
        /// The access restricted message.
        /// </value>
        public string AccessRestrictedMessage { get; private set; }
        /// <summary>
        /// Gets the training documents.
        /// </summary>
        /// <value>
        /// The training documents.
        /// </value>
        public List<TrainingDocumentViewModel> TrainingDocuments { get; private set; }
        #endregion

        #region Helpers         
        /// <summary>
        /// Gets the program year display.
        /// </summary>
        /// <param name="fy">The fiscal year.</param>
        /// <param name="description">The description.</param>
        /// <param name="abbreviation">The abbreviation.</param>
        /// <returns></returns>
        public string GetProgramYearDisplay(string fy, string description, string abbreviation) 
        {
            return string.Format("{0} {1} ({2})", fy, description, abbreviation);
        }
        #endregion
    }
}