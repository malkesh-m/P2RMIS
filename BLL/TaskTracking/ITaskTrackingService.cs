using System;
using System.Collections.Generic;
using System.Web;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.WebModels.TaskTracking;

namespace Sra.P2rmis.Bll.TaskTracking
{
    public interface ITaskTrackingService
    {
        /// <summary>
        /// Submits a task to the internal ticket management system.
        /// </summary>
        /// <param name="model">The model containing task information.</param>
        /// <returns>Ticket ID of the newly created task; empty string if task was not created</returns>
        string SubmitTask(ISubmitTaskModel model, IEnumerable<HttpPostedFileBase> attachments, IMailService theMailService);
        /// <summary>
        /// Gets the metadata associated with a P2RMIS IT ticket.
        /// </summary>
        /// <returns>Container of ticket metadata</returns>
        ITaskMetadataModel GetTicketMetaData();
        /// <summary>
        /// Gets the ticket information.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns></returns>
        ISubmitTaskModel GetTicketInformation(string ticketId);
        /// <summary>
        /// Modifies a Jira task via REST API.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="comment">The comment to add, if any.</param>
        /// <param name="attachments">The attachments to add, if any.</param>
        /// <param name="usersName">Full name of the user making the change.</param>
        /// <returns>
        /// true if successful; otherwise exception
        /// </returns>
        bool EditTask(string ticketId, string comment, IEnumerable<HttpPostedFileBase> attachments, string usersName);
    }
}