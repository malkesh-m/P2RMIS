using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModel = Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions
        /// <summary>
        /// action result for the Compose Email page
        /// </summary>
        /// <returns>the view of the Compose Email page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult Communication()
        {
            CommunicationViewModel theViewModel = new CommunicationViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                int userId = GetUserId();
                var userInfoId = theUserProfileManagementService.GetUserInfoId(GetUserId());

                theViewModel.SenderEmailAddress = theUserProfileManagementService.GetPrimaryInstitutionalUserEmailAddress(userInfoId).Address;
                ////
                //// This may be the first time through.  In which case the panelId will be 0
                //// and we do not need to call to get the application information
                ////
                if (theViewModel.SelectedPanel > 0)
                {
                    theViewModel.SessionPanelId = theViewModel.SelectedPanel;
                    theViewModel.AvailableReviewerEmailAddresses = theMailService.ListPanelReviewersEmailAddresses(theViewModel.SelectedPanel).ModelList.ToList();

                    theViewModel.PanelAdministrators = theMailService.ListPanelSroRtaEmailAddresses(theViewModel.SelectedPanel).ModelList.ToList();
                    theViewModel.EmailTemplates = theFileService.RetriveEmailTemplatesList(theViewModel.SelectedProgramYear, theViewModel.SelectedPanel).ModelList.ToList();
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new CommunicationViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }
        /// <summary>
        /// Displays the communication screen from the registration status view.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="selectedProgramYear">ProgramYear entity identifier</param>
        /// <returns>Communications View</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult SendStatusCommunication(int panelUserAssignmentId, int sessionPanelId, int selectedProgramYear)
        {
            CommunicationViewModel theViewModel = new CommunicationViewModel();
            try
            {
                //
                // Set the session variables for the communication page
                //
                int? meetingSessionId = this.thePanelManagementService.GetMeetingSessionId(sessionPanelId);
                SetSessionVariables(selectedProgramYear, meetingSessionId, sessionPanelId);

                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                //
                // Get who is sending the email 
                //
                var userInfoId = theUserProfileManagementService.GetUserInfoId(GetUserId());
                theViewModel.SenderEmailAddress = theUserProfileManagementService.GetPrimaryInstitutionalUserEmailAddress(userInfoId).Address;
                //
                // Set the selected value so the drop down displays the selected value
                //
                SetProgramYearSession((int)selectedProgramYear);

                theViewModel.SessionPanelId = sessionPanelId;
                theViewModel.AvailableReviewerEmailAddresses = theMailService.ListPanelReviewersEmailAddresses(sessionPanelId).ModelList.ToList();
                theViewModel.PanelAdministrators = theMailService.ListPanelSroRtaEmailAddresses(sessionPanelId).ModelList.ToList();

                List<string> selected = new List<string>();
                selected.Add(panelUserAssignmentId.ToString());
                theViewModel.SelectedPanelUserAssignmentIds = selected;
                //
                // Set the selected value so the drop down displays the selected value & panel menus
                //
                SetPanelSession(sessionPanelId);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new CommunicationViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(ViewNames.Communication, theViewModel);
        }
        /// <summary>
        /// action result for the Compose Email page
        /// </summary>
        /// <param name="SelectedProgramYear">the selected Program/Year identifier</param>
        /// <param name="SelectedPanel">the selected Panel identifier</param>
        /// <returns>the view of the Compose Email page</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult Communication(int? SelectedProgramYear, int SelectedPanel)
        {
            CommunicationViewModel theViewModel = new CommunicationViewModel();
            try
            {
                var userInfoId = theUserProfileManagementService.GetUserInfoId(GetUserId());

                if (SelectedPanel > 0)
                {
                    //
                    // Set the selected value so the drop down displays the selected value
                    //
                    int? meetingSessionId = this.thePanelManagementService.GetMeetingSessionId(SelectedPanel);
                    SetSessionVariables(SelectedProgramYear, meetingSessionId, SelectedPanel);
                }

                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);

                theViewModel.SenderEmailAddress = theUserProfileManagementService.GetPrimaryInstitutionalUserEmailAddress(userInfoId).Address;

                if (theViewModel.SelectedPanel > 0)
                {
                    theViewModel.SessionPanelId = theViewModel.SelectedPanel;
                    theViewModel.AvailableReviewerEmailAddresses = theMailService.ListPanelReviewersEmailAddresses(theViewModel.SelectedPanel).ModelList.ToList();

                    theViewModel.PanelAdministrators = theMailService.ListPanelSroRtaEmailAddresses(theViewModel.SelectedPanel).ModelList.ToList();
                    theViewModel.EmailTemplates = theFileService.RetriveEmailTemplatesList(theViewModel.SelectedProgramYear, theViewModel.SelectedPanel).ModelList.ToList();
                }
                //
                // Set the selected value so the drop down displays the selected value
                //
                SetPanelSession(SelectedPanel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new CommunicationViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }

        /// <summary>
        /// action result for sending an email
        /// </summary>
        /// <param name="senderEmail">the sender's email address</param>
        /// <param name="recipientEmail">the recipient's email addresses</param>
        /// <param name="ccEmail">the carbon copy email addresses</param>
        /// <param name="files">the files</param>
        /// <returns>the view of the result</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult SendEmail(FormCollection form, IEnumerable<HttpPostedFileBase> files, int sessionPanelId)
        {
            CommunicationViewModel theViewModel1 = new CommunicationViewModel();
            int dummy;

            ConfirmCommunicationContentViewModel theViewModel = new ConfirmCommunicationContentViewModel();
            try
            {
                theViewModel.SessionPanelId = sessionPanelId;
                theViewModel.Subject = form.GetValues("subject").ToList().FirstOrDefault();

                // remove non integer convertible 'header' and convert to integer
                theViewModel.To = form.GetValues("SelectedPanelUserAssignmentIds").Where(x => int.TryParse(x, out dummy) == true).ToList();
                List<int> pua = theViewModel.To.Select(s => int.Parse(s)).ToList();

                // obtain email addresses for this sessionPanelId and the collection of selected panel user assignments
                List<IEmailAddress> reviewerAddresses = theMailService.ListPanelReviewersEmailAddresses(sessionPanelId).ModelList.ToList();

                List<string> puaEmailAddresses = (from ra in reviewerAddresses
                                                  join ua in pua
                                                  on (int)ra.PanelUserAssignmentId equals ua
                                                  select ra.UserEmailAddress).ToList();

                // format ';' delineated list
                theViewModel.To = puaEmailAddresses;
                string formattedTo = theViewModel.FormatCommunicationsList(theViewModel.To);

                theViewModel.From = form.GetValues("senderEmail").ToList().FirstOrDefault();

                // deal with cc
                List<string> cc = form.GetValues("PanelAdministrators") == null ? new List<string>() : form.GetValues("PanelAdministrators").Where(x => int.TryParse(x, out dummy) == true).ToList<string>();

                List<int> ccpua = cc.Select(s => int.Parse(s)).ToList();

                List<IEmailAddress> ccAddresses = theMailService.ListPanelSroRtaEmailAddresses(sessionPanelId).ModelList.ToList();

                List<string> ccpuaEmailAddresses = (from ra in ccAddresses
                                                    join ua in ccpua
                                                    on (int)ra.PanelUserAssignmentId equals ua
                                                    select ra.UserEmailAddress).ToList();

                // format to ';' delineated list
                theViewModel.Cc = ccpuaEmailAddresses;
                string formattedCc = theViewModel.FormatCommunicationsList(theViewModel.Cc);

                // comes formatted
                theViewModel.Bcc = form.GetValues("bccEmail").ToList().FirstOrDefault();

                theViewModel.Subject = form.GetValues("subject").ToList().FirstOrDefault();

                theViewModel.Message = form.GetValues("communicationEditor").ToList().FirstOrDefault();

                List<AttachmentToSend> attachments = new List<AttachmentToSend>();
                if (files != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            AttachmentToSend attachment = new AttachmentToSend(file.FileName, file.InputStream);
                            attachments.Add(attachment);
                        }
                    }
                }

                MailService.MailStatus o = this.theMailService.SendEmail(theViewModel.SessionPanelId, pua, theViewModel.From, formattedTo, ccpua, formattedCc, theViewModel.Bcc, theViewModel.Subject, HttpUtility.HtmlDecode(theViewModel.Message), attachments, theViewModel.To, theViewModel.Cc, GetUserId());

                TempData["SuccessMessage"] = GetMessage(o);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel1 = new CommunicationViewModel();
                HandleExceptionViaElmah(e);
            }
            return RedirectToAction("CommunicationLog");
        }

        /// <summary>
        /// action result for the Compose Email page
        /// </summary>
        /// <returns>the view of the Compose Email page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult CommunicationLog()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            CommunicationLogViewModel theViewModel = new CommunicationLogViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                //
                // This may be the first time through.  In which case the panelId will be 0
                // and we do not need to call to get the application information
                //
                if (theViewModel.SelectedPanel > 0)
                {
                    var emailList = thePanelManagementService.ListPanelCommunicationMessages(theViewModel.SelectedPanel);
                    theViewModel.Communications = emailList.ModelList.ToList<WebModel.ISessionPanelCommunicationsList>();
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new CommunicationLogViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }

        /// <summary>
        /// action result for the Compose Email page
        /// </summary>
        /// <returns>the view of the Compose Email page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult ViewCommunication(int communicationLogId)
        {
            CommunicationContentViewModel theViewModel = new CommunicationContentViewModel();

            theViewModel.Content = thePanelManagementService.GetEmailContent(communicationLogId) as WebModel.EmailContent;

            return PartialView(ViewNames.ViewCommunication, theViewModel);
        }
        /// <summary>
        /// action result for the selection of reviews to receive emails
        /// </summary>
        /// <returns>the view of the Compose Email page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult SelectReviewersEmailAddress(int sessionPanelId)
        {
            CommunicationViewModel theViewModel = new CommunicationViewModel();

            theViewModel.AvailableReviewerEmailAddresses = theMailService.ListPanelReviewersEmailAddresses(sessionPanelId).ModelList.ToList();

            return PartialView(ViewNames.SelectReviewersEmailAddress, theViewModel);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Gets message for MailService status value
        /// </summary>
        /// <param name="status">The MailService.MailStatus value</param>
        /// <returns>the message if failed. Otherwise return an empty string.</returns>
        private string GetMessage(MailService.MailStatus status)
        {
            string message = string.Empty;
            switch (status)
            {
                case MailService.MailStatus.AttachmentsExceedSize:
                    message = Messages.AttachmentsExceedSize;
                    break;
                case MailService.MailStatus.FailedToSend:
                    message = Messages.FailedToSend;
                    break;
                case MailService.MailStatus.Failure:
                    message = Messages.Failure;
                    break;
            }
            return message;
        }
        #endregion
    }
}