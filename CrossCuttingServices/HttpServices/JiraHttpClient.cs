using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Sra.P2rmis.CrossCuttingServices.HttpServices
{
    /// <summary>
    /// Http Client for making requests to Jira
    /// </summary>
    /// <seealso cref="System.Net.Http.HttpClient" />
    public class JiraHttpClient : HttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JiraHttpClient"/> class.  Used for Jira API communication that requires a HttpClientHandler.
        /// </summary>
        public JiraHttpClient(HttpClientHandler handler) : base(handler)
        {
            ClientSetup();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="JiraHttpClient"/> class.  Used for Jira API communication.
        /// </summary>
        public JiraHttpClient()
        {
            ClientSetup();
        }
        #region Methods
        /// <summary>
        /// set up authorization and other necessary headers for all Jira API calls
        /// </summary>
        private void ClientSetup()
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{JiraUserName}:{JiraUserPassword}"));
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            Timeout = new TimeSpan(0, 0, 10);
        }
        /// <summary>
        /// Submits the new jira ticket.
        /// </summary>
        /// <param name="content">The serialized JSON content.</param>
        /// <returns>Jira ticket ID if successful</returns>
        /// <remarks>Tried to make this async but kept hanging</remarks>
        public string SubmitNewJiraTicket(string content)
        {
            var response = this.PostAsync(JiraSubmitTaskUri, new StringContent(content, Encoding.UTF8, "application/json")).Result;
            //raises exception if unsuccessful response code is returned
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// Gets the ticket meta data json.
        /// </summary>
        /// <returns>Json representation of all ticket metadata</returns>
        public string GetTicketMetaDataJson()
        {
            var response = this.GetAsync(JiraEditMetaUri).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// Gets the ticket information.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>Json representation of ticket information</returns>
        public string GetTicketInformation(string ticketId)
        {
            var response = this.GetAsync(GetJiraTicketInfoUrl(ticketId)).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// Adds a comment to the specified ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="content">The json content to be posted with the request.</param>
        public void AddComment(string ticketId, string content)
        {
            var response = this.PostAsync(GetJiraTicketCommentUrl(ticketId), new StringContent(content, Encoding.UTF8, "application/json")).Result;
            response.EnsureSuccessStatusCode();
        }
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <remarks>Process seems heavy for preparing file</remarks>
        public void AddFile(string ticketId, string contentType, string fileName, byte[] data)
        {
            var requestContent = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(data);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            // Add the image
            requestContent.Add(fileContent, "file", fileName);
            var response = this.PostAsync(GetJiraTicketAttachmentUrl(ticketId), requestContent).Result;
            response.EnsureSuccessStatusCode();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the jira submit task URI.
        /// </summary>
        /// <value>
        /// The jira submit task URI.
        /// </value>
        internal static string JiraSubmitTaskUri => ConfigurationServices.ConfigManager.JiraUrl;
        /// <summary>
        /// Gets the name of the jira user.
        /// </summary>
        /// <value>
        /// The name of the jira user.
        /// </value>
        internal static string JiraUserName => ConfigurationServices.ConfigManager.JiraUserName;
        /// <summary>
        /// Gets the jira user password.
        /// </summary>
        /// <value>
        /// The jira user password.
        /// </value>
        internal static string JiraUserPassword => ConfigurationServices.ConfigManager.JiraPassword;
        /// <summary>
        /// Gets or sets the jira edit meta URI.
        /// </summary>
        /// <value>
        /// The jira edit meta URI used to retreive metadata such as accepted values.
        /// </value>
        internal static string JiraEditMetaUri => ConfigurationServices.ConfigManager.JiraMetadataUrl;
        #endregion
        #region Helpers
        /// <summary>
        /// Gets the jira ticket information URL.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>string representing the URL endpoint that provides information on a given ticket</returns>
        internal string GetJiraTicketInfoUrl(string ticketId)
        {
            return String.Concat(JiraSubmitTaskUri, ticketId);
        }
        /// <summary>
        /// Gets the jira ticket comment URL.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>URL to access comments for a specified ticket</returns>
        internal string GetJiraTicketCommentUrl(string ticketId)
        {
            return $"{JiraSubmitTaskUri}{ticketId}/comment";
        }
        /// <summary>
        /// Gets the jira ticket attachment URL.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>URL to access attachments for a specified ticket</returns>
        internal string GetJiraTicketAttachmentUrl(string ticketId)
        {
            return $"{JiraSubmitTaskUri}{ticketId}/attachments";
        }
        /// <summary>
        /// Adds the no check header, required for file posts.
        /// </summary>
        public void AddNoCheckHeader()
        {
            this.DefaultRequestHeaders.Add("X-Atlassian-Token", "no-check");
        }
        #endregion


    }
}
