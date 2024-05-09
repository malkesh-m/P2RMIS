using Sra.P2rmis.WebModels.TaskTracking;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices.HttpServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;
using Sra.P2rmis.Bll.Mail;
using System.Net;
using System.Net.Http;

namespace Sra.P2rmis.Bll.TaskTracking
{
    public class TaskTrackingService : ServerBase, ITaskTrackingService
    {
        #region Services
        /// <summary>
        /// Submits a task to the internal ticket management system.
        /// </summary>
        /// <param name="model">The model containing task information.</param>
        /// <returns>Ticket ID of the newly created task; empty string if task was not created</returns>
        public string SubmitTask(ISubmitTaskModel model, IEnumerable<HttpPostedFileBase> attachments, IMailService theMailService)
        {
            var json = string.Empty;
            //Transform the model to JSON
            json = ConvertSubmitTaskJson(model);
            //Setup web client and credentials
            JiraHttpClient jiraClient = GetJiraClientForEnvironment();
            //Submit the model and record new ticket Id
            var result = jiraClient.SubmitNewJiraTicket(json);
            var newTicketId = GetJiraTicketIdFromResponse(result);
            jiraClient.AddNoCheckHeader();
            //Now loop through and add attachments
            foreach (HttpPostedFileBase attachment in attachments)
            {
                if (attachment != null)
                {
                    byte[] attachmentByteArray = ConvertAttachmentToByteArray(attachment);
                    jiraClient.AddFile(newTicketId, attachment.ContentType, Path.GetFileName(attachment.FileName), attachmentByteArray);
                }
            }
            //Finally send email to user with their ticket information
            theMailService.SendTicketNotification(model.RequestorEmail, newTicketId);
            return newTicketId;
        }

        /// <summary>
        /// Gets the component list.
        /// </summary>
        /// <returns>List of components</returns>
        public ITaskMetadataModel GetTicketMetaData()
        {
            //Setup web client and credentials
            JiraHttpClient jiraClient = GetJiraClientForEnvironment();
            var jsonResponse = jiraClient.GetTicketMetaDataJson();
            return GetMetatadataFromResponse(jsonResponse);
        }

        /// <summary>
        /// Gets the ticket information.
        /// </summary>
        /// <param name="ticketId">The ticket identifier in Jira.</param>
        /// <returns>Details regarding the specified ticket</returns>
        public ISubmitTaskModel GetTicketInformation(string ticketId)
        {
            //Setup web client and credentials
            JiraHttpClient jiraClient = GetJiraClientForEnvironment();

            //Get ticket information from Jira
            var jsonResponse = jiraClient.GetTicketInformation(ticketId);
            return GetTicketInformationFromResponse(jsonResponse);
        }

        /// <summary>
        /// Modifies a Jira task via REST API.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="comment">The comment to add, if any.</param>
        /// <param name="attachments">The attachments to add, if any.</param>
        /// <returns>
        /// true if successful; otherwise exception
        /// </returns>
        public bool EditTask(string ticketId, string comment, IEnumerable<HttpPostedFileBase> attachments, string usersName)
        {
            bool didSomething = false;
            //Setup web client and credentials
            JiraHttpClient jiraClient = GetJiraClientForEnvironment();
            //Add comment information
            if (!string.IsNullOrWhiteSpace(comment))
            {
                jiraClient.AddComment(ticketId, ConvertCommentToJson(comment, usersName));
                didSomething = true;
            }
            jiraClient.AddNoCheckHeader();
            //Loop through and add attachments
            foreach (HttpPostedFileBase attachment in attachments)
            {
                if (attachment != null)
                {
                    byte[] attachmentByteArray = ConvertAttachmentToByteArray(attachment);
                    jiraClient.AddFile(ticketId, attachment.ContentType, Path.GetFileName(attachment.FileName), attachmentByteArray);
                    didSomething = true;
                }
            }
            return didSomething;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts the submit task model to jira json format.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Json string representation of converted model</returns>
        internal string ConvertSubmitTaskJson(ISubmitTaskModel model)
        {
            //populate JSON model, we use anonymous wrapper to create root name
            var jsonModel =
                new
                {
                    fields = new SubmitTaskJsonModel(ConfigManager.JiraProjectId, ConfigManager.JiraDefaultAssignee, model.RequestorName, model.RequestorEmail, model.SelectedClient,
                        model.Subject, model.RequestType, model.DueDate.ToString("yyyy-MM-dd"), model.Component,
                        model.RequestDescription, model.ProjectJustification, model.DocumentLink, model.TaskTypeId)
                };
            //serialize to JSON string
            return JsonConvert.SerializeObject(jsonModel);
        }
        /// <summary>
        /// Gets the jira ticket identifier from response.
        /// </summary>
        /// <param name="response">The response json.</param>
        /// <returns>string representation of the new Jira ticket Id</returns>
        internal string GetJiraTicketIdFromResponse(string response)
        {
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            var newTicketId = string.Empty;
            responseDictionary.TryGetValue("key", out newTicketId);
            return newTicketId;
        }

        /// <summary>
        /// Gets the metatadata from the json response payload.
        /// </summary>
        /// <param name="response">The response json.</param>
        /// <returns>TaskMetadataModel object containing ticket metadata</returns>
        internal ITaskMetadataModel GetMetatadataFromResponse(string response)
        {
            dynamic responseObject = JsonConvert.DeserializeObject(response);

            var components = responseObject.projects[0].issuetypes[0].fields.customfield_11601.allowedValues;
            var taskTypes = responseObject.projects[0].issuetypes[0].fields.customfield_11900.allowedValues;
            var componentList = new List<Tuple<int, string>>();
            foreach (var component in components)
            {
                componentList.Add(new Tuple<int, string>((int)component.id, (string)component.value));
            }
            var taskTypeList = new List<Tuple<int, string>>();
            foreach (var taskType in taskTypes)
            {
                taskTypeList.Add(new Tuple<int, string>((int)taskType.id, (string)taskType.value));
            }
            return new TaskMetadataModel(componentList, taskTypeList);
        }

        /// <summary>
        /// Gets the ticket information from json response.
        /// </summary>
        /// <param name="jsonResponse">The json response.</param>
        /// <returns>SubmitTaskModel object</returns>
        internal ISubmitTaskModel GetTicketInformationFromResponse(string jsonResponse)
        {
            dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);
            IDictionary<string, JToken> dictionary = responseObject;
            var componentExists = dictionary["fields"]["customfield_11601"].HasValues;
            var taskTypeExists = dictionary["fields"]["customfield_11900"].HasValues;
            var assigneeExists = dictionary["fields"]["assignee"].HasValues;
            return new SubmitTaskModel((string)responseObject.fields.customfield_10303, string.Empty, (string)responseObject.fields.customfield_10300.value, (string)responseObject.fields.summary, (string)responseObject.fields.issuetype.name,
                Convert.ToDateTime(responseObject.fields.duedate), componentExists ? (int?)responseObject.fields.customfield_11601[0].id : null, taskTypeExists ? (int?)responseObject.fields.customfield_11900.id : null, (string)responseObject.fields.description, string.Empty, string.Empty,
                componentExists ? (string)responseObject.fields.customfield_11601[0].value : string.Empty, taskTypeExists ? (string)responseObject.fields.customfield_11900.value : string.Empty, assigneeExists ? (string)responseObject.fields.assignee.name : string.Empty, (string)responseObject.fields.status.name);
        }

        /// <summary>
        /// Converts the comment to json format required for the request.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>json string of comment</returns>
        internal string ConvertCommentToJson(string comment, string usersName)
        {
            //we use a dictionary object as a holding pattern for this simple Json conversion
            IDictionary<string, string> temp = new Dictionary<string, string>
            {
                ["body"] = $"{usersName} - {comment}"
            };
            return JsonConvert.SerializeObject(temp);
        }

        /// <summary>
        /// Converts the attachment to byte array.
        /// </summary>
        /// <param name="attachment">The attachment file to convert.</param>
        /// <returns>attachment as byte for shipping out</returns>
        /// <remarks>
        /// MemoryStream conversion recommended by Jon Skeet - http://stackoverflow.com/questions/7852102/convert-httppostedfilebase-to-byte
        /// </remarks>
        internal byte[] ConvertAttachmentToByteArray(HttpPostedFileBase attachment)
        {
            MemoryStream target = new MemoryStream();
            attachment.InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            return data;
        }
        /// <summary>
        /// Gets an outgoing proxy handler for a http client.
        /// </summary>
        /// <param name="attachment">The attachment file to convert.</param>
        /// <returns>attachment as byte for shipping out</returns>
        internal HttpClientHandler GetProxyHandler()
        {
            HttpClientHandler handler = null;
            handler = new HttpClientHandler()
            {
                Proxy = new WebProxy(ConfigManager.OutgoingProxy),
                UseProxy = true,
            };
            return handler;
        }

        internal JiraHttpClient GetJiraClientForEnvironment()
        {
            JiraHttpClient jiraClient = null;
            if (!String.IsNullOrEmpty(ConfigManager.OutgoingProxy))
            {
                jiraClient = new JiraHttpClient(GetProxyHandler());
            }
            else
            {
                jiraClient = new JiraHttpClient();
            }
            return jiraClient;
        }
        #endregion
    }
}
