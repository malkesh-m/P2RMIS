using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sra.P2rmis.WebModels.TaskTracking
{
   

    public class SubmitTaskJsonModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitTaskJsonModel" /> class.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="assignee">The assignee.</param>
        /// <param name="requestorName">Name of the requestor.</param>
        /// <param name="requestorEmail">The requestor email.</param>
        /// <param name="selectedClient">The selected client.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="requestType">Type of the request.</param>
        /// <param name="dueDate">The due date.</param>
        /// <param name="component">The component.</param>
        /// <param name="requestDescription">The request description.</param>
        /// <param name="projectJustification">The project justification.</param>
        /// <param name="documentLink">The document link.</param>
        /// <param name="taskTypeId">The task type identifier.</param>
        public SubmitTaskJsonModel(string projectId, string assignee, string requestorName, string requestorEmail, string selectedClient, string subject, string requestType, string dueDate, int? component, string requestDescription, string projectJustification, string documentLink, int? taskTypeId)
        {
            Project1 = new Project() { Id = projectId};
            Assignee1 = new Assignee() { Name = assignee};
            Custom_Field_10300 = new CustomField_10300() {Value = selectedClient};
            Summary = subject;
            IssueType1 = new IssueType() { Name = requestType};
            DueDate = dueDate;
            Custom_Field_11601 = new List<CustomField_11601>() { new CustomField_11601() { Id = component.ToString()}};
            Description = GetJiraDescription(requestDescription, projectJustification, documentLink);
            CustomField_10303 = requestorName;
            Custom_Field_11900 = new CustomField_11900() { Id = taskTypeId.ToString() } ;
        }
        /// <summary>
        /// Gets or sets the project object.
        /// </summary>
        /// <value>
        /// The project object.
        /// </value>
        [JsonProperty(PropertyName = "project")]
        public Project Project1 { get; set; }
        /// <summary>
        /// Gets or sets the assignee object.
        /// </summary>
        /// <value>
        /// The assignee object.
        /// </value>
        [JsonProperty(PropertyName = "assignee")]
        public Assignee Assignee1 { get; set; }
        /// <summary>
        /// Gets or sets the issue type object.
        /// </summary>
        /// <value>
        /// The issue type object.
        /// </value>
        [JsonProperty(PropertyName = "issuetype")]
        public IssueType IssueType1 { get; set; }
        /// <summary>
        /// Gets or sets the custom_ field_10300 object (client).
        /// </summary>
        /// <value>
        /// The custom_ field_10300 object (client).
        /// </value>
        [JsonProperty(PropertyName = "customfield_10300")]
        public CustomField_10300 Custom_Field_10300 { get; set; }
        /// <summary>
        /// Gets or sets the custom_ field_11601 object (component).
        /// </summary>
        /// <value>
        /// The custom_ field_11601.
        /// </value>
        [JsonProperty(PropertyName = "customfield_11601")]
        public IList<CustomField_11601> Custom_Field_11601 { get; set; }
        /// <summary>
        /// Gets or sets the custom_ field_11900 object (tasktype).
        /// </summary>
        /// <value>
        /// The custom_ field_11900.
        /// </value>
        [JsonProperty(PropertyName = "customfield_11900")]
        public CustomField_11900 Custom_Field_11900 { get; set; }
        /// <summary>
        /// The project jira object
        /// </summary>
        public class Project
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }
        }

        /// <summary>
        /// The assignee jira object
        /// </summary>
        public class Assignee
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
        }

        /// <summary>
        /// The issuetype jira object
        /// </summary>
        public class IssueType
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
        }

        /// <summary>
        /// The CustomField_10300 (client) jira object
        /// </summary>
        public class CustomField_10300
        {
            /// <summary>
            /// Gets or sets the client name value.
            /// </summary>
            /// <value>
            /// The client name value.
            /// </value>
            [JsonProperty(PropertyName = "value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// The CustomField_11601 (component) jira object
        /// </summary>
        public class CustomField_11601
        {
            /// <summary>
            /// Gets or sets the component name value array.
            /// </summary>
            /// <value>
            /// The component values.
            /// </value>
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }
        }

        public class CustomField_11900
        {
            /// <summary>
            /// Gets or sets the task type value array.
            /// </summary>
            /// <value>
            /// The task type value.
            /// </value>
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }
        }
        /// <summary>
        /// Gets or sets the jira ticket summary object.
        /// </summary>
        /// <value>
        /// The jira ticket summary object.
        /// </value>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }
        /// <summary>
        /// Gets or sets the due date jira object.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [JsonProperty(PropertyName = "duedate")]
        public string DueDate { get; set; }
        /// <summary>
        /// Gets or sets the ticket description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the custom field10303 (requestor name) for Jira.
        /// </summary>
        /// <value>
        /// The custom field10303.
        /// </value>
        [JsonProperty(PropertyName = "customfield_10303")]
        public string CustomField_10303 { get; set; }
        /// <summary>
        /// Gets the description for Jira.
        /// </summary>
        /// <param name="requestDescription">The request description.</param>
        /// <param name="projectJustification">The project justification.</param>
        /// <param name="documentLink">The document link.</param>
        /// <returns>Description field formatted with Jira markup</returns>
        internal string GetJiraDescription(string requestDescription, string projectJustification, string documentLink)
        {
            return
                $"Description: {requestDescription}\\\\Project Justification: {projectJustification}\\\\DocumentLink: {documentLink}";
        }
        /*
        "project" : {
          "id" : "10103"
       },
	   "assignee" : {
		  "name" : "' & variables.owner & '"
	   },
       "summary" : "' & FORM.subject & '",
	   "duedate" : "' & DateFormat(variables.duedate, "yyyy-mm-dd") & '",
       "description" : "' & variables.description & '",
       "issuetype" : {
          "name" : "' & FORM.type & '"
	   },
	   "customfield_10303" : "' & FORM.requestorName & '",
	   "customfield_10300" : {
			"value" : "' & FORM.Client & '"
	   },
	   "customfield_11601" : [{
			"value" : "' & FORM.Component & '"
        */
        #endregion
        #region Properties
        #endregion

    }
}
