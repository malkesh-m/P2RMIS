using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Bll;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.ConsumerManagement;
using Sra.P2rmis.Bll.UserProfileManagement;

namespace Sra.P2rmis.Web.Controllers.ConsumerManagement
{
    public class ConsumerManagementController : ConsumerManagementBaseController
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerManagementController"/> class.
        /// </summary>
        public ConsumerManagementController() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerManagementController"/> class.
        /// </summary>
        /// <param name="consumerManagementService">The consumer management service.</param>
        public ConsumerManagementController(IConsumerManagementService consumerManagementService,
            ILookupService lookupService, ICriteriaService criteriaService, IUserProfileManagementService userProfileManagementService)
        {
            theConsumerManagementService = consumerManagementService;
            theLookupService = lookupService;
            theCriteriaService = criteriaService;
            theUserProfileManagementService = userProfileManagementService;
        }
        #endregion

        /// <summary>
        /// Consumer management home page
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ConsumerManagementAccess)]
        public ActionResult Index()
        {
            var model = new ConsumerManagementViewModel();
            try
            {
                var nomineeTypes = theConsumerManagementService.GetNomineeTypes();
                model = new ConsumerManagementViewModel(nomineeTypes);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(model);
        }
        /// <summary>
        /// Adds or edits consumer
        /// </summary>
        /// <param name="nomineeId">The nominee identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult Consumer(int? nomineeId)
        {
            var model = new ConsumerViewModel();
            try
            {
                int userId = GetUserId();
                var nomineeTypes = theConsumerManagementService.GetNomineeTypes();
                var prefixes = theLookupService.ListPrefix().ModelList.ToList();
                var genders = theLookupService.ListGender().ModelList.ToList();
                var ethnicities = theLookupService.ListEthnicity().ModelList.ToList();
                var phoneTypes = theLookupService.ListPhoneType().ModelList.ToList();
                var usStates = theLookupService.ListStateByName().ModelList.ToList();
                var countries = theLookupService.ListCountryUsCanada().ModelList.ToList();
                var affected = theLookupService.ListNomineeAffected().ModelList.ToList();
                var clientIds = theUserProfileManagementService.GetAssignedActiveClients(userId).ModelList.ToList().ConvertAll(x => x.ClientId);
                var programs = theCriteriaService.GetOpenClientPrograms(clientIds).ModelList.ToList();
                var defaultCountryId = LookupService.LookupUsCountryId;
                var defaultPhoneType1Id = LookupService.LookupPhoneTypeIdForHome;
                var defaultPhoneType2Id = LookupService.LookupPhoneTypeIdForDesk;
                model = new ConsumerViewModel(nomineeTypes, prefixes, genders, ethnicities, phoneTypes, usStates,
                    countries, affected, programs, defaultCountryId, defaultPhoneType1Id, defaultPhoneType2Id);
                var editing = nomineeId != null;
                ViewBag.Editing = editing;
                if (editing)
                {
                    model.NomineeUpdateModel = theConsumerManagementService.GetNomineeUpdateModel((int)nomineeId);
                    model.NomineeProgramUpdateModel = theConsumerManagementService.GetNomineeProgramUpdateModel((int)nomineeId);
                    model.NomineeSponsorUpdateModel = theConsumerManagementService.GetNomineeSponsorUpdateModel((int)nomineeId);
                    if (model.NomineeProgramUpdateModel.NomineeTypeId == LookupService.LookupNomineeTypeIneligible)
                    {
                        model.NomineeTypes = model.NomineeTypes.Where(x => x.Key != LookupService.LookupNomineeTypeSelectedNovice).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(model);
        }       
        /// <summary>
        /// Gets nominees
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="nomineeTypeId">The nominee type identifier</param>
        /// <param name="nominatingOrganization">The nominating organization</param>
        /// <param name="score">The score</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult GetNominees(string name, int? nomineeTypeId, string nominatingOrganization, string score)
        {
            var model = new List<NomineeViewModel>();
            try
            {
                // Convert name to firstName and lastName
                var lastName = string.Empty;
                var firstName = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    var nameArray = name.Split(',');
                    lastName = nameArray[0].Trim();
                    if (nameArray.Length > 1)
                    {
                        firstName = nameArray[1].Trim();
                    }
                }
                // Convert score to integer
                int? scoreInteger = null;
                if (!string.IsNullOrWhiteSpace(score))
                {
                    int tmp;
                    int.TryParse(score.Trim(), out tmp);
                    scoreInteger = tmp;
                }
                var consumers = theConsumerManagementService.GetNominees(
                    firstName, lastName, nomineeTypeId, nominatingOrganization.Trim(), scoreInteger);
                model = consumers.ConvertAll(x => new NomineeViewModel(x.FirstName, x.LastName, x.Type,
                    x.NominatingOrganization, x.Program, x.FiscalYear, x.Score, x.UserId, x.UserInfoId, x.NomineeId));
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Saves nominee.
        /// </summary>
        /// <param name="model">The consumer view model</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult SaveNominee(ConsumerViewModel model)
        {
            var saveNomineeUpdateModel = new SaveNomineeUpdateModel();
            try
            {
                var userId = GetUserId();
                 saveNomineeUpdateModel = theConsumerManagementService.SaveNominee(model.NomineeUpdateModel, 
                    model.NomineeProgramUpdateModel,
                    model.NomineeSponsorUpdateModel,
                    userId);
                saveNomineeUpdateModel.Status = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return Json(saveNomineeUpdateModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Detects if the email is unavailable.
        /// </summary>
        /// <param name="nomineeEmail">Email address</param>
        /// <param name="nomineeId">Nominee identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult EmailIsUnavailable(NomineeUpdateModel nomineeUpdateModel)
        {
            var status = true;
            try
            {
                status = theConsumerManagementService.EmailIsUnavailable(nomineeUpdateModel.Email, nomineeUpdateModel.NomineeId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult GetConsumerProgramYears(int clientProgramId, bool editing)
        {
            var programYears = theConsumerManagementService.GetConsumerProgramYears(clientProgramId, editing);
            return Json(programYears, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ConsumerManagement.ManageConsumers)]
        public ActionResult GetNominatingOrganizations(string inprefix, string findtype)
        {
            var organizations = theConsumerManagementService.GetNominatingOrganizationList(inprefix, findtype) ;
            return Json(organizations, JsonRequestBehavior.AllowGet);
        }

    }
}