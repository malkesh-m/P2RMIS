using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Security;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Security;

namespace Sra.P2rmis.Web.Controllers
{
    public class SecurityController : SecurityBaseController
    {
        protected ILookupService theLookupService { get; set; }

        protected ISecurityService theSecurityService { get; set; }

        public SecurityController(ILookupService lookupService, ISecurityService securityService)
        {
            theLookupService = lookupService;
            theSecurityService = securityService;
        }
        [Common.Authorize(Operations = Permissions.SecurityManagement.ViewSecurityInformation)]
        public ActionResult Menu()
        {
            return RedirectToAction("SecurityPolicy");
        }
        // GET: Security
        [Common.Authorize(Operations = Permissions.SecurityManagement.ViewSecurityInformation)]
        public ActionResult SecurityPolicy()
        {
            var model = new SecurityPolicyViewModel();
            return View(model);
        }

        public ActionResult SecurityPolicyHistoryLog()
        {
            var model = new SecurityPolicyHistoryLogViewModel();
            return View(model);
        }
        [Common.Authorize(Operations = Permissions.SecurityManagement.ViewSecurityInformation)]
        public ActionResult UserPolicy()
        {
            return View();
        }

        [Common.Authorize(Operations = Permissions.SecurityManagement.ViewSecurityInformation)]
        public ActionResult GetPolicies()
        {
            var policies = theSecurityService.GetPolicies()
                .OrderByDescending(p => p.StartDateTime)
                .Select(p => new PolicyGridViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type,
                    StartDateTime = p.StartDateTime,
                    EndDateTime = p.EndDateTime,
                    RestrictionStartTime = p.RestrictionStartTime,
                    RestrictionEndTime = p.RestrictionEndTime,
                    DaysApplied = p.DaysApplied,
                    NetworkRanges = p.NetworkRanges,
                    Status = p.Status,
                    CreatedBy = p.CreatedBy,
                    CreatedDateTime = p.CreatedDateTime.ToString()
                })
                .ToList();
            return Json(policies, JsonRequestBehavior.AllowGet);
        }

        [Common.Authorize(Operations = Permissions.SecurityManagement.PolicyManagement)]
        public ActionResult AddPolicy(int? policyId)
        {
            PolicyViewModel viewModel = new PolicyViewModel();
            ConfigurePolicyViewModel(viewModel, (int)Client.ClientName.CPRIT);
            if (policyId != null)
            {
                var opolicy = theSecurityService.GetPolicyModelById(policyId.Value);
                viewModel.Policy = opolicy;
            }
            return PartialView("_Policy", viewModel);
        }

        [HttpPost]
        [Common.Authorize(Operations = Permissions.SecurityManagement.PolicyManagement)]
        public ActionResult AddPolicy(int? policyId, int clientId, int type, string name, string details, DateTime startDate, string startTime, DateTime? endDate, string endTime, int restrictionType, string restrictionStartTime, string restrictionEndTime, string weekDays, string networkRanges)
        {
            bool flag = false;
            var messages = new List<string>();
            try
            {
                int userId = GetUserId();
                ServiceState s = theSecurityService.AddPolicy(policyId, clientId, type, name, details, startDate, startTime, endDate, endTime, restrictionType, restrictionStartTime, restrictionEndTime, weekDays, userId, networkRanges);
                messages = s.Messages.ToList();
                flag = (messages.Count == 0);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, policyId = 1, messages = messages });
        }
        /// <summary>
        /// Archive Policy.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SecurityManagement.PolicyManagement)]
        public ActionResult ArchivePolicy(int policyId)
        {
            bool flag = false;
            try
            {
                flag = theSecurityService.ArchivePolicy(policyId, GetUserId());
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag });
        }
        /// <summary>
        /// Enable/Disable Policy.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SecurityManagement.PolicyManagement)]
        public ActionResult ActivateOrDeactivatePolicy(int policyId)
        {
            bool flag = false;
            try
            {
                flag = theSecurityService.ActivateOrDeactivatePolicy(policyId, GetUserId());
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag });
        }
        /// <summary>
        /// Configures the view model with values for Add/Edit
        /// </summary>
        /// <param name="model">PolicyViewModel view model to populate</param>
        /// <param name="clientId">Client entity identifier</param>
        private void ConfigurePolicyViewModel(PolicyViewModel model, int clientId)
        {
            // Retrieve the lists
            //
            Container<IListEntry> clientTypeList = theLookupService.ListClientAbbreviation(clientId);
            Container<IListEntry> policyTypeList = theLookupService.ListPolicyTypes();
            Container<IListEntry> policyRestrictionList = theLookupService.ListPolicyRestrictionTypes();
            Container<IListEntry> weekDayList = theLookupService.ListWeekDays();
            //
            // Set the lists into the view model
            //
            model.ConfigureLists(ConvertListType(clientTypeList.ModelList), ConvertListType(policyTypeList.ModelList), ConvertListType(policyRestrictionList.ModelList), ConvertListType(weekDayList.ModelList));
        }

        /// <summary>
        /// Converts a Enumeration of IListEntry's to a list of KeyValuePair<int, string>'s.
        /// </summary>
        /// <param name="listIn">List of values to convert</param>
        /// <returns>List of <KeyValuePair<int, string>></returns>
        protected List<KeyValuePair<int, string>> ConvertListType(IEnumerable<IListEntry> listIn)
        {
            List<KeyValuePair<int, string>> outList = new List<KeyValuePair<int, string>>();
            listIn.ToList().ForEach(x => outList.Add(new KeyValuePair<int, string>(x.Index, x.Value)));
            return outList;
        }
        public ActionResult GetPolicyNetworkRange(string octet1, string octet2, string octet3, string octet4, int maskBits)
        {
            ActionResult result;
            try
            {
                var broadcastAddress = $"{int.Parse(octet1)}.{int.Parse(octet2)}.{int.Parse(octet3)}.{int.Parse(octet4)}";
                var range = NetTools.IPAddressRange.Parse($"{broadcastAddress}/{maskBits}");
                var begin = range.Begin?.ToString();
                var end = range.End?.ToString();
                var beginAndEnd = $"{begin}, {end}";
                result = Json(beginAndEnd, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid IP Address");
            }
            return result;
        }
    }
}