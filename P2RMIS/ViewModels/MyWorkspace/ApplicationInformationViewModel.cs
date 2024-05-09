using System;
using System.Collections.Generic;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ApplicationInformationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationInformationViewModel class.
        /// </summary>
        public ApplicationInformationViewModel() { }
        /// <summary>
        /// Initializes a new instance of the ApplicationInformationViewModel class.
        /// </summary>
        /// <param name="applicationInformation">The application information.</param>
        /// <param name="critiquePhase">The critique phase.</param>
        /// <param name="critiqueReviewerOrderedList">The critique reviewer ordered list.</param>
        public ApplicationInformationViewModel(IApplicationInformationModel applicationInformation, IEditCritiquePhaseModel critiquePhase, List<ICritiqueReviewerOrderedModel> critiqueReviewerOrderedList)
        {
            ApplicationId = applicationInformation.ApplicationId;
            LogNumber = applicationInformation.LogNumber;
            Title = applicationInformation.Title;
            TitleCropped = ViewHelpers.CropText(Title, Invariables.MyWorkspace.MaxLongTitleLengthBeforeCropping);
            PiName = applicationInformation.Blinded ? Invariables.MyWorkspace.Blinded : ViewHelpers.ConstructNameWithSpace(applicationInformation.PiFirstName, applicationInformation.PiLastName);
            AwardMechanism = applicationInformation.AwardMechanism;
            Phase = critiquePhase.CritiquePhase;
            CritiqueDueDate = ViewHelpers.FormatEtDateTime(critiquePhase.CritiqueDueDate);
            var list = GetReviewerOrderedList(critiqueReviewerOrderedList);
            CritiqueReviewerOrderedList = string.Join(", ", list);
        }
        /// <summary>
        /// Unique identifier for the application
        /// </summary>
        public int ApplicationId { get; private set; }
        /// <summary>
        /// Log number of the application
        /// </summary>
        public string LogNumber { get; private set; }
        /// <summary>
        /// Principal investigator name of the application
        /// </summary>
        public string PiName { get; private set; }
        /// <summary>
        /// Mechanism abbreviation of the application
        /// </summary>
        public string AwardMechanism { get; private set; }
        /// <summary>
        /// Title of the application
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Title of the application cropped to a shorter length
        /// </summary>
        public string TitleCropped { get; private set; }
        /// <summary>
        /// Phase
        /// </summary>
        public string Phase { get; private set; }
        /// <summary>
        /// Critique due date
        /// </summary>
        public string CritiqueDueDate { get; private set; }
        /// <summary>
        /// Critique reviewer ordered list
        /// </summary>
        public string CritiqueReviewerOrderedList { get; private set; }
        /// <summary>
        /// Get reviewer ordered list in string format
        /// </summary>
        /// <param name="critiqueReviewerOrderedList">List of ICritiqueReviewerOrderedModel</param>
        /// <returns>List of reviewers in string format</returns>
        private List<String> GetReviewerOrderedList(List<ICritiqueReviewerOrderedModel> critiqueReviewerOrderedList)
        {
            var list = new List<string>();
            foreach(ICritiqueReviewerOrderedModel reviewer in critiqueReviewerOrderedList) 
            {
                list.Add(ViewHelpers.FormatReviewerName(reviewer.ReviewerFirstName, reviewer.ReviewerLastName, reviewer.clientAssignmentTypeAbbreviation));
            }
            return list;
        }
    }
}