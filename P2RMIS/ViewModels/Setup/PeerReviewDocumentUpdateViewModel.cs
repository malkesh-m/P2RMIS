using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Library;
using Sra.P2rmis.CrossCuttingServices;
using System.Linq;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class PeerReviewDocumentUpdateViewModel
    {
        private const string All = "All";
        private const string Full = "Full";
        private const string Partial = "Partial";
        private const string UrlAccessMethod = "web";
        private const string VideoAccessMethod = "embed";
        private const string NonAppliable = "N/A";

        public PeerReviewDocumentUpdateViewModel() { }
        /// <summary>
        /// Sets the document.
        /// </summary>
        /// <param name="model">The model.</param>
        public void SetDocument(IPeerReviewDocumentModel model)
        {
            DocumentId = model.DocumentId;
            Heading = model.Heading;
            Description = model.Description;
            IsUrl = model.ContentTypeAccessMethod == UrlAccessMethod;
            IsVideo = model.ContentTypeAccessMethod == VideoAccessMethod;
            DocumentTypeId = model.DocumentTypeId;
            TrainingCategoryId = model.TrainingCategoryId;
            FiscalYear = (model.FiscalYear) == null ? All : model.FiscalYear;
            Program = (model.Program) == null ? All : model.Program;
            Path = !String.IsNullOrEmpty(model.ContentUrl)? model.ContentUrl : model.ContentFileLocation;
            FileType = (IsUrl || IsVideo) ? NonAppliable : System.IO.Path.GetExtension(Path);
            CreatedDate = ViewHelpers.FormatDate(model.CreatedDate);
            ModifiedDate = ViewHelpers.FormatDate(model.ModifiedDate);
            CreatedByName = model.CreatedByName;
            ModifiedByName = model.ModifiedByName;
            Active = model.Active;
        }
        /// <summary>
        /// Sets the access.
        /// </summary>
        /// <param name="models">The models.</param>
        public void SetAccess(IPeerReviewDocumentAccessModel model)
        {
            MeetingTypeIds = model.MeetingTypeIds;
            ParticipantTypeIds = model.ClientParticipantTypeIds;
            ParticipationMethodIds = model.ParticipationMethodIds;
            ParticipationLevel = model.RestrictedAssignedFlag;
        }
        /// <summary>
        /// Sets the lists.
        /// </summary>
        /// <param name="contentTypeList">The content type list.</param>
        /// <param name="documentTypeList">The document type list.</param>
        /// <param name="fiscalYearList">The fiscal year list.</param>
        /// <param name="programList">The program list.</param>
        /// <param name="trainingCategoryList">The training category list.</param>
        /// <param name="meetingTypeList">The meeting type list.</param>
        /// <param name="participantTypeList">The participant type list.</param>
        /// <param name="participationMethodList">The participation method list.</param>
        public void SetLists(List<KeyValuePair<int, string>> contentTypeList, List<KeyValuePair<int, string>> documentTypeList, List<string> fiscalYearList,
            List<KeyValuePair<int, string>> programList, List<KeyValuePair<int, string>> trainingCategoryList, List<KeyValuePair<int, string>> meetingTypeList, List<KeyValuePair<int, string>> participantTypeList,
            List<KeyValuePair<int, string>> participationMethodList)
        {
            ContentTypeList = contentTypeList;
            DocumentTypeList = documentTypeList;
            FiscalYearList = fiscalYearList;
            ProgramList = programList;
            TrainingCategoryList = trainingCategoryList;
            MeetingTypeList = meetingTypeList;
            ParticipantTypeList = participantTypeList;
            ParticipationMethodList = participationMethodList;
            ParticipationLevelList = new List<KeyValuePair<bool?, string>> {
                new KeyValuePair<bool?, string>(null, All), new KeyValuePair<bool?, string>(true, Full), new KeyValuePair<bool?, string>(false, Partial)
            };
        }
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public int DocumentId { get; set; }
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        public string Heading { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideo { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is URL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is URL; otherwise, <c>false</c>.
        /// </value>
        public bool IsUrl { get; set; }
        /// <summary>
        /// Gets or sets the document type identifier.
        /// </summary>
        /// <value>
        /// The document type identifier.
        /// </value>
        public int? DocumentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the training category identifier.
        /// </summary>
        /// <value>
        /// The training category identifier.
        /// </value>
        public int? TrainingCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        private string Path { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PeerReviewDocumentViewModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        private bool Active { get; set; }
        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        public string FileType { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public string CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the created by.
        /// </summary>
        /// <value>
        /// The name of the created by.
        /// </value>
        public string CreatedByName { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public string ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        public string ModifiedByName { get; set; }
        /// <summary>
        /// Gets or sets the meeting type ids.
        /// </summary>
        /// <value>
        /// The meeting type ids.
        /// </value>
        public string MeetingTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the participant type ids.
        /// </summary>
        /// <value>
        /// The participant type ids.
        /// </value>
        public string ParticipantTypeIds { get; set; }
        /// <summary>
        /// Gets or sets the participation method identifier.
        /// </summary>
        /// <value>
        /// The participation method identifier.
        /// </value>
        public string ParticipationMethodIds { get; set; }
        /// <summary>
        /// Gets or sets the participation level.
        /// </summary>
        /// <value>
        /// The participation level.
        /// </value>
        public bool? ParticipationLevel { get; set; }
        /// <summary>
        /// Gets or sets the content type list.
        /// </summary>
        /// <value>
        /// The content type list.
        /// </value>
        public List<KeyValuePair<int, string>> ContentTypeList { get; set; }
        /// <summary>
        /// Gets or sets the document type list.
        /// </summary>
        /// <value>
        /// The document type list.
        /// </value>
        public List<KeyValuePair<int, string>> DocumentTypeList { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year list.
        /// </summary>
        /// <value>
        /// The fiscal year list.
        /// </value>
        public List<string> FiscalYearList { get; set; }
        /// <summary>
        /// Gets or sets the program list.
        /// </summary>
        /// <value>
        /// The program list.
        /// </value>
        public List<KeyValuePair<int, string>> ProgramList { get; set; }
        /// <summary>
        /// Gets or sets the training category list.
        /// </summary>
        /// <value>
        /// The training category list.
        /// </value>
        public List<KeyValuePair<int, string>> TrainingCategoryList { get; set; }
        /// <summary>
        /// Gets or sets the meeting type list.
        /// </summary>
        /// <value>
        /// The meeting type list.
        /// </value>
        public List<KeyValuePair<int, string>> MeetingTypeList { get; set; }
        /// <summary>
        /// Gets or sets the participant type list.
        /// </summary>
        /// <value>
        /// The participant type list.
        /// </value>
        public List<KeyValuePair<int, string>> ParticipantTypeList { get; set; }
        /// <summary>
        /// Gets or sets the participation method list.
        /// </summary>
        /// <value>
        /// The participation method list.
        /// </value>
        public List<KeyValuePair<int, string>> ParticipationMethodList { get; set; }
        /// <summary>
        /// Gets or sets the participation level list.
        /// </summary>
        /// <value>
        /// The participation level list.
        /// </value>
        public List<KeyValuePair<bool?, string>> ParticipationLevelList { get; set; }
    }
}