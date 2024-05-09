using System;
using System.Collections.Generic;

namespace Sra.P2rmis.CrossCuttingServices.Models
{
    /// <summary>
    /// Model representing summary statement data to initially populate a document
    /// </summary>
    public interface ISummaryDocumentModel
    {
        /// <summary>
        /// Gets or sets the application information for the summary document.
        /// </summary>
        /// <value>
        /// The application information for the summary document.
        /// </value>
        SummaryDocumentModel.ApplicationInfo App { get; set; }

        /// <summary>
        /// Gets or sets the critiques to be displayed.
        /// </summary>
        /// <value>
        /// The critiques to be displayed.
        /// </value>
        IEnumerable<SummaryDocumentModel.CritiqueData> Critiques { get; set; }

        /// <summary>
        /// Gets or sets the other data1 generic data collection.
        /// </summary>
        /// <value>
        /// The other data1 generic data collection.
        /// </value>
        IEnumerable<SummaryDocumentModel.OtherData> OtherData1 { get; set; }

        /// <summary>
        /// Gets or sets the other data2 generic data collection.
        /// </summary>
        /// <value>
        /// The other data2 generic data collection.
        /// </value>
        IEnumerable<SummaryDocumentModel.OtherData> OtherData2 { get; set; }

        /// <summary>
        /// Gets or sets the other data3 generic data collection.
        /// </summary>
        /// <value>
        /// The other data3 generic data collection.
        /// </value>
        IEnumerable<SummaryDocumentModel.OtherData> OtherData3 { get; set; }
    }

    /// <summary>
    /// Model representing summary statement data to initially populate a document
    /// </summary>
    public class SummaryDocumentModel : ISummaryDocumentModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the application information for the summary document.
        /// </summary>
        /// <value>
        /// The application information for the summary document.
        /// </value>
        public ApplicationInfo App { get; set; }
        /// <summary>
        /// Gets or sets the critiques to be displayed.
        /// </summary>
        /// <value>
        /// The critiques to be displayed.
        /// </value>
        public IEnumerable<CritiqueData> Critiques { get; set; }
        
        /// <summary>
        /// Gets or sets the other data1 generic data collection.
        /// </summary>
        /// <value>
        /// The other data1 generic data collection.
        /// </value>
        public IEnumerable<OtherData> OtherData1 { get; set; }

        /// <summary>
        /// Gets or sets the other data2 generic data collection.
        /// </summary>
        /// <value>
        /// The other data2 generic data collection.
        /// </value>
        public IEnumerable<OtherData> OtherData2 { get; set; }

        /// <summary>
        /// Gets or sets the other data3 generic data collection.
        /// </summary>
        /// <value>
        /// The other data3 generic data collection.
        /// </value>
        public IEnumerable<OtherData> OtherData3 { get; set; }
        #endregion
        #region Nested types
        /// <summary>
        /// Container for information related to an application's general information
        /// </summary>
        public class ApplicationInfo
        {
            /// <summary>
            /// Gets or sets the log number.
            /// </summary>
            /// <value>
            /// The log number.
            /// </value>
            public string LogNumber { get; set; }

            /// <summary>
            /// Gets or sets the application title.
            /// </summary>
            /// <value>
            /// The application title.
            /// </value>
            public string ApplicationTitle { get; set; }

            /// <summary>
            /// Gets or sets the program description.
            /// </summary>
            /// <value>
            /// The program description.
            /// </value>
            public string ProgramDesc { get; set; }

            /// <summary>
            /// Gets or sets the program abbreviation.
            /// </summary>
            /// <value>
            /// The program abbreviation.
            /// </value>
            public string ProgramAbrv { get; set; }

            /// <summary>
            /// Gets or sets the award description.
            /// </summary>
            /// <value>
            /// The award desc.
            /// </value>
            public string AwardDesc { get; set; }

            /// <summary>
            /// Gets or sets the award abbreviation.
            /// </summary>
            /// <value>
            /// The award abbreviation.
            /// </value>
            public string AwardAbrv { get; set; }

            /// <summary>
            /// Gets or sets the panel start date.
            /// </summary>
            /// <value>
            /// The panel start date.
            /// </value>
            public DateTime? PanelStartDate { get; set; }

            /// <summary>
            /// Gets or sets the panel end date.
            /// </summary>
            /// <value>
            /// The panel end date.
            /// </value>
            public DateTime? PanelEndDate { get; set; }

            /// <summary>
            /// Gets or sets the project start date.
            /// </summary>
            /// <value>
            /// The project start date.
            /// </value>
            public DateTime? ProjectStartDate { get; set; }

            /// <summary>
            /// Gets or sets the project end date.
            /// </summary>
            /// <value>
            /// The project end date.
            /// </value>
            public DateTime? ProjectEndDate { get; set; }

            /// <summary>
            /// Gets or sets the year.
            /// </summary>
            /// <value>
            /// The year.
            /// </value>
            public string Year { get; set; }

            /// <summary>
            /// Gets or sets the receipt cycle.
            /// </summary>
            /// <value>
            /// The receipt cycle.
            /// </value>
            public int? ReceiptCycle { get; set; }

            /// <summary>
            /// Gets or sets the duration.
            /// </summary>
            /// <value>
            /// The duration.
            /// </value>
            public decimal? Duration { get; set; }

            /// <summary>
            /// Gets or sets the name of the panel.
            /// </summary>
            /// <value>
            /// The name of the panel.
            /// </value>
            public string PanelName { get; set; }

            /// <summary>
            /// Gets or sets the panel abbreviation.
            /// </summary>
            /// <value>
            /// The panel abbreviation.
            /// </value>
            public string PanelAbbreviation { get; set; }

            /// <summary>
            /// Gets or sets the total budget.
            /// </summary>
            /// <value>
            /// The total budget.
            /// </value>
            public decimal? TotalBudget { get; set; }

            /// <summary>
            /// Gets or sets the direct costs.
            /// </summary>
            /// <value>
            /// The direct costs.
            /// </value>
            public decimal? DirectCosts { get; set; }

            /// <summary>
            /// Gets or sets the indirect costs.
            /// </summary>
            /// <value>
            /// The indirect costs.
            /// </value>
            public decimal? IndirectCosts { get; set; }

            /// <summary>
            /// Gets or sets the first name of the principal investigator.
            /// </summary>
            /// <value>
            /// The first name of the principal investigator.
            /// </value>
            public string PiFirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name of the principal investigator.
            /// </summary>
            /// <value>
            /// The last name of the principal investigator.
            /// </value>
            public string PiLastName { get; set; }

            /// <summary>
            /// Gets or sets the name of the principal investigator organization.
            /// </summary>
            /// <value>
            /// The name of the principal investigator organization.
            /// </value>
            public string PiOrgName { get; set; }

            /// <summary>
            /// Gets or sets the first name of the admin (contracting) representative.
            /// </summary>
            /// <value>
            /// The first name of the admin (contracting) representative.
            /// </value>
            public string AdminFirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name of the admin (contracting) representative.
            /// </summary>
            /// <value>
            /// The last name of the admin (contracting) representative.
            /// </value>
            public string AdminLastName { get; set; }

            /// <summary>
            /// Gets or sets the name of the admin (contracting) representative.
            /// </summary>
            /// <value>
            /// The name of the admin (contracting) representative.
            /// </value>
            public string AdminOrgName { get; set; }

            /// <summary>
            /// Gets or sets the field1.
            /// </summary>
            /// <value>
            /// The field1.
            /// </value>
            public string Field1 { get; set; }

            /// <summary>
            /// Gets or sets the field2.
            /// </summary>
            /// <value>
            /// The field2.
            /// </value>
            public string Field2 { get; set; }

            /// <summary>
            /// Gets or sets the field3.
            /// </summary>
            /// <value>
            /// The field3.
            /// </value>
            public string Field3 { get; set; }

            /// <summary>
            /// Gets or sets the field4.
            /// </summary>
            /// <value>
            /// The field4.
            /// </value>
            public string Field4 { get; set; }

            /// <summary>
            /// Gets or sets the field5.
            /// </summary>
            /// <value>
            /// The field5.
            /// </value>
            public string Field5 { get; set; }

            /// <summary>
            /// Gets or sets the field6.
            /// </summary>
            /// <value>
            /// The field6.
            /// </value>
            public string Field6 { get; set; }

            /// <summary>
            /// Gets or sets the field7.
            /// </summary>
            /// <value>
            /// The field7.
            /// </value>
            public string Field7 { get; set; }

            /// <summary>
            /// Gets or sets the field8.
            /// </summary>
            /// <value>
            /// The field8.
            /// </value>
            public string Field8 { get; set; }

            /// <summary>
            /// Gets or sets the field9.
            /// </summary>
            /// <value>
            /// The field9.
            /// </value>
            public string Field9 { get; set; }

            /// <summary>
            /// Gets or sets the field10.
            /// </summary>
            /// <value>
            /// The field10.
            /// </value>
            public string Field10 { get; set; }

            /// <summary>
            /// Gets or sets the field11.
            /// </summary>
            /// <value>
            /// The field11.
            /// </value>
            public string Field11 { get; set; }

            /// <summary>
            /// Gets or sets the field12.
            /// </summary>
            /// <value>
            /// The field12.
            /// </value>
            public string Field12 { get; set; }

            /// <summary>
            /// Gets or sets the field13.
            /// </summary>
            /// <value>
            /// The field13.
            /// </value>
            public string Field13 { get; set; }

            /// <summary>
            /// Gets or sets the field14.
            /// </summary>
            /// <value>
            /// The field14.
            /// </value>
            public string Field14 { get; set; }

            /// <summary>
            /// Gets or sets the field15.
            /// </summary>
            /// <value>
            /// The field15.
            /// </value>
            public string Field15 { get; set; }
        }
        /// <summary>
        /// Container for information related to a piece of critique data
        /// </summary>
        public class CritiqueData
        {
            /// <summary>
            /// Gets or sets the participant type description of the reviewer.
            /// </summary>
            /// <value>
            /// The participant type description.
            /// </value>
            public string PartTypeDesc { get; set; }
            /// <summary>
            /// Gets or sets the participant type abbreviation of the reviewer.
            /// </summary>
            /// <value>
            /// The type of the part.
            /// </value>
            public string PartType { get; set; }
            /// <summary>
            /// Gets or sets the name of the role.
            /// </summary>
            /// <value>
            /// The name of the role.
            /// </value>
            public string RoleName { get; set; }
            /// <summary>
            /// Gets or sets the role abbreviation.
            /// </summary>
            /// <value>
            /// The role abbreviation.
            /// </value>
            public string RoleAbbreviation { get; set; }
            /// <summary>
            /// Gets or sets the element desc.
            /// </summary>
            /// <value>
            /// The element desc.
            /// </value>
            public string ElementDesc { get; set; }
            /// <summary>
            /// Gets or sets the client element identifier.
            /// </summary>
            /// <value>
            /// The client element identifier.
            /// </value>
            public int ClientElementId { get; set; }
            /// <summary>
            /// Gets or sets the panel application reviewer assignment identifier.
            /// </summary>
            /// <value>
            /// The panel application reviewer assignment identifier.
            /// </value>
            public int? PanelApplicationReviewerAssignmentId { get; set; }
            /// <summary>
            /// Gets or sets the application workflow step element identifier.
            /// </summary>
            /// <value>
            /// The application workflow step element identifier.
            /// </value>
            public int? ApplicationTemplateElementId { get; set; }
            /// <summary>
            /// Gets or sets the element type identifier.
            /// </summary>
            /// <value>
            /// The element type identifier.
            /// </value>
            public int ElementTypeId { get; set; }
            /// <summary>
            /// Gets or sets the element order.
            /// </summary>
            /// <value>
            /// The element order.
            /// </value>
            public int ElementOrder { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [score flag].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [score flag]; otherwise, <c>false</c>.
            /// </value>
            public bool ScoreFlag { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [text flag].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [text flag]; otherwise, <c>false</c>.
            /// </value>
            public bool TextFlag { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [overall flag].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [overall flag]; otherwise, <c>false</c>.
            /// </value>
            public bool OverallFlag { get; set; }
            /// <summary>
            /// Gets or sets the score.
            /// </summary>
            /// <value>
            /// The score.
            /// </value>
            public decimal? Score { get; set; }
            /// <summary>
            /// Gets or sets the average score for the criteria.
            /// </summary>
            /// <value>
            /// The average score for the criteria.
            /// </value>
            public decimal? AvgScore { get; set; }
            /// <summary>
            /// Gets or sets the standard deviation for the criteria.
            /// </summary>
            /// <value>
            /// The standard deviation for the criteria.
            /// </value>
            public decimal? StandardDeviation { get; set; }
            /// <summary>
            /// Gets or sets the content text.
            /// </summary>
            /// <value>
            /// The content text.
            /// </value>
            public string ContentText { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="CritiqueData"/> is abstain.
            /// </summary>
            /// <value>
            ///   <c>true</c> if abstain; otherwise, <c>false</c>.
            /// </value>
            public bool Abstain { get; set; }
            /// <summary>
            /// Gets or sets the reviewer order.
            /// </summary>
            /// <value>
            /// The reviewer order.
            /// </value>
            public int? ReviewerOrder { get; set; }
            /// <summary>
            /// Gets or sets the display name of the reviewer.
            /// </summary>
            /// <value>
            /// The display name of the reviewer.
            /// </value>
            public string ReviewerDisplayName { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [discussion note flag].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [discussion note flag]; otherwise, <c>false</c>.
            /// </value>
            public bool DiscussionNoteFlag { get; set; }
            /// <summary>
            /// Gets or sets the adjectival score label.
            /// </summary>
            /// <value>
            /// The adjectival score label.
            /// </value>
            public string AdjectivalScoreLabel { get; set; }
            /// <summary>
            /// Gets or sets the type of the score.
            /// </summary>
            /// <value>
            /// The type of the score.
            /// </value>
            public string ScoreType { get; set; }
            /// <summary>
            /// Gets or sets the field1.
            /// </summary>
            /// <value>
            /// The field1.
            /// </value>
            public string Field1 { get; set; }
            /// <summary>
            /// Gets or sets the field2.
            /// </summary>
            /// <value>
            /// The field2.
            /// </value>
            public string Field2 { get; set; }
            /// <summary>
            /// Gets or sets the field3.
            /// </summary>
            /// <value>
            /// The field3.
            /// </value>
            public string Field3 { get; set; }
            /// <summary>
            /// Gets or sets the field4.
            /// </summary>
            /// <value>
            /// The field4.
            /// </value>
            public string Field4 { get; set; }
            /// <summary>
            /// Gets or sets the field5.
            /// </summary>
            /// <value>
            /// The field5.
            /// </value>
            public string Field5 { get; set; }
            /// <summary>
            /// Gets or sets the field6.
            /// </summary>
            /// <value>
            /// The field6.
            /// </value>
            public string Field6 { get; set; }
            /// <summary>
            /// Gets or sets the field7.
            /// </summary>
            /// <value>
            /// The field7.
            /// </value>
            public string Field7 { get; set; }
            /// <summary>
            /// Gets or sets the field8.
            /// </summary>
            /// <value>
            /// The field8.
            /// </value>
            public string Field8 { get; set; }
            /// <summary>
            /// Gets or sets the field9.
            /// </summary>
            /// <value>
            /// The field9.
            /// </value>
            public string Field9 { get; set; }
            /// <summary>
            /// Gets or sets the field10.
            /// </summary>
            /// <value>
            /// The field10.
            /// </value>
            public string Field10 { get; set; }
        }
        /// <summary>
        /// Generic data fields that can be used for storing other data to be displayed in a summary statement
        /// </summary>
        public class OtherData
        {
            /// <summary>
            /// Gets or sets the field1.
            /// </summary>
            /// <value>
            /// The field1.
            /// </value>
            public string Field1 { get; set; }
            /// <summary>
            /// Gets or sets the field2.
            /// </summary>
            /// <value>
            /// The field2.
            /// </value>
            public string Field2 { get; set; }
            /// <summary>
            /// Gets or sets the field3.
            /// </summary>
            /// <value>
            /// The field3.
            /// </value>
            public string Field3 { get; set; }
            /// <summary>
            /// Gets or sets the field4.
            /// </summary>
            /// <value>
            /// The field4.
            /// </value>
            public string Field4 { get; set; }
            /// <summary>
            /// Gets or sets the field5.
            /// </summary>
            /// <value>
            /// The field5.
            /// </value>
            public string Field5 { get; set; }
            /// <summary>
            /// Gets or sets the field6.
            /// </summary>
            /// <value>
            /// The field6.
            /// </value>
            public string Field6 { get; set; }
            /// <summary>
            /// Gets or sets the field7.
            /// </summary>
            /// <value>
            /// The field7.
            /// </value>
            public string Field7 { get; set; }
            /// <summary>
            /// Gets or sets the field8.
            /// </summary>
            /// <value>
            /// The field8.
            /// </value>
            public string Field8 { get; set; }
            /// <summary>
            /// Gets or sets the field9.
            /// </summary>
            /// <value>
            /// The field9.
            /// </value>
            public string Field9 { get; set; }
            /// <summary>
            /// Gets or sets the field10.
            /// </summary>
            /// <value>
            /// The field10.
            /// </value>
            public string Field10 { get; set; }
            /// <summary>
            /// Gets or sets the field11.
            /// </summary>
            /// <value>
            /// The field11.
            /// </value>
            public string Field11 { get; set; }
            /// <summary>
            /// Gets or sets the field12.
            /// </summary>
            /// <value>
            /// The field12.
            /// </value>
            public string Field12 { get; set; }
            /// <summary>
            /// Gets or sets the field13.
            /// </summary>
            /// <value>
            /// The field13.
            /// </value>
            public string Field13 { get; set; }
            /// <summary>
            /// Gets or sets the field14.
            /// </summary>
            /// <value>
            /// The field14.
            /// </value>
            public string Field14 { get; set; }
            /// <summary>
            /// Gets or sets the field15.
            /// </summary>
            /// <value>
            /// The field15.
            /// </value>
            public string Field15 { get; set; }
        }
        #endregion




    }
}
