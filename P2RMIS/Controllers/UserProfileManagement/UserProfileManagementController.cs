using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.Web.ViewModels.UserProfileManagement;
using System.Collections;


namespace Sra.P2rmis.Web.Controllers.UserProfileManagement
{
    /// <summary>
    /// UserProfileManagement controller provides access to:
    ///   - User creation
    ///   - User profile management
    ///   - searching for users
    ///   - 
    /// </summary>
    public partial class UserProfileManagementController : UserProfileManagementBaseController
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private UserProfileManagementController()
        {
        }

        /// <summary>
        /// Constructor for the UserProfileManagement controller.
        /// </summary>
        /// <param name="theLookupService">Lookup service</param>
        /// <param name="theFileService">File Service</param>
        public UserProfileManagementController(ILookupService theLookupService, IFileService theFileService, IUserProfileManagementService theProfileService, IMailService theMailService, IAccessAccountManagementService theAccessAccountService, IPanelManagementService thePanelManagementService)
        {
            this.theLookupService = theLookupService;
            this.theFileService = theFileService;
            this.theProfileService = theProfileService;
            this.theMailService = theMailService;
            this.theAccessAccountService = theAccessAccountService;
            this.thePanelManagementService = thePanelManagementService;
        }
        #endregion
        #region Controller Actions
        /// <summary>
        /// TODO: descriptions
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult Index()
        {
            try
            {
               //TODO: SetTabs(viewModel);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return View();
        }
        /// <summary>
        /// The method called when the user clicks create or update user
        /// User is directed to first perform a search
        /// </summary>
        /// <param name="context">the context of the search (create or update user)</param>
        /// <returns>the modal for searching a reviewer</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult SearchUser(string context)
        {
            SearchUserViewModel viewModel = new SearchUserViewModel();
            try
            {
                // save last url in session
                GetBackButtonUrl();

                // if the context is update
                if (context == SearchContexts.UpdateContext)
                {
                    // mark the update context as true
                    viewModel.IsUpdateUserSearch = true;
                    // set the session variable to remember context across pages
                    if (Session != null)
                        Session[ProfileManagementSessionContext] = SearchContexts.UpdateContext;
                }
                else
                {
                    // mark the update context as false (create context)
                    viewModel.IsUpdateUserSearch = false;
                    // set the session variable to remember context across pages
                    if (Session!= null)
                    Session[ProfileManagementSessionContext] = SearchContexts.CreateContext;
                }
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.SearchUser, viewModel);
        }
        /// <summary>
        /// the action called when the user searches for another user before update/create user 
        /// </summary>
        /// <param name="viewModel">the view model passed back from the page</param>
        /// <returns>0, 1, or multiple users along with its corresponding page.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult SearchResults(SearchUserViewModel viewModel)
        {
            // Set default view result
            ActionResult viewResult = View(ViewNames.SearchResults);
            try
            {
                Container<IFoundUserModel> results = new Container<IFoundUserModel>();
                // if the search is in the update context
                if (viewModel.IsUpdateUserSearch == false)
                {
                    //call the create user search method in BL
                    results = theProfileService.SearchUser(viewModel.FirstName, viewModel.LastName, viewModel.Email);
                }
                else
                {
                    //call update user search method in BL
                    results = theProfileService.SearchUser(viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.UserName, viewModel.UserId, viewModel.VendorId);
                }

                if (results.ModelList.ToList().Count == 0)
                {
                    if (viewModel.IsUpdateUserSearch)
                    {
                        // if update, show no records
                        var model = new SearchResultsViewModel(viewModel.FirstName, viewModel.LastName,
                            viewModel.Email, viewModel.UserName, viewModel.UserId);
                        // Sort by relevancy
                        model.Users = results.ModelList.OrderByDescending(o => o.RelevancyRank).ToList();
                        // Search criteria
                        SearchResultsViewModel.CriteriaFormatter = SearchResultsViewModel.FormatSearchCriteria;
                        viewResult = View(ViewNames.SearchResults, model);
                    }
                    else
                    {
                        //if create, show empty profile to create new
                        ProfileViewModel model = new ProfileViewModel { IsMyProfile = false, IsUpdateUser = false };
                        PopulateProfileDropdowns(model, GetUserId());

                        PopulateNewProfileViewModel(model);
                        PopulateViewModelWithSearchParameters(model, viewModel.FirstName, viewModel.LastName, viewModel.Email);

                        model.IsBadgeNameEditable = HasPermission(Permissions.UserProfileManagement.ManageUserAccounts);
                        model.SetTabContext(GetUserId(), model.GeneralInfo.UserId, model.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
                        model.SetMenuTitle(GetUserId(), model.GeneralInfo.UserId, HasMyWorkspacePermission());
                        // Set profile name
                        viewResult = View(ViewNames.Profile, model);
                    }
                }
                else if (results.ModelList.ToList().Count == 1)
                {
                    IFoundUserModel foundUser = results.ModelList.ElementAt(0);
                    // Redirection to avoid "out of memory" issues
                    viewResult = RedirectToAction("ViewUser", "UserProfileManagement", new { userInfoId = foundUser.UserInfoId });
                }
                else
                {
                    // Multiple users
                    var model = new SearchResultsViewModel(viewModel.FirstName, viewModel.LastName, 
                        viewModel.Email, viewModel.UserName, viewModel.UserId);
                    // Sort by relevancy
                    model.Users = results.ModelList.OrderByDescending(o => o.RelevancyRank).ToList();
                    // Search criteria
                    SearchResultsViewModel.CriteriaFormatter = SearchResultsViewModel.FormatSearchCriteria;
                    viewResult = View(ViewNames.SearchResults, model);
                }
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }

            return viewResult;
        }
        
        /// <summary>
        /// the action called when viewing create new user profile
        /// </summary>
        /// <returns>empty profile for a new user</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult ViewNewUser()
        {
            ProfileViewModel viewModel = new ProfileViewModel {IsMyProfile = false, IsUpdateUser = false};
            PopulateProfileDropdowns(viewModel, GetUserId());
            PopulateNewProfileViewModel(viewModel);
            viewModel.LastPageUrl = GetBackButtonUrl();
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.CanViewVendorManagement = false;
            return View(ViewNames.Profile, viewModel);
        }

        /// <summary>
        /// The action called when viewing create new user profile
        /// </summary>
        /// <param name="firstName">The new user's first name</param>
        /// <param name="lastName">The new user's last name</param>
        /// <param name="email">The new user's email address</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult ViewCreateNewUser(string firstName, string lastName, string email)
        {
            ProfileViewModel viewModel = new ProfileViewModel { IsMyProfile = false, IsUpdateUser = false };
            PopulateProfileDropdowns(viewModel, GetUserId());

            PopulateNewProfileViewModel(viewModel);
            PopulateViewModelWithSearchParameters(viewModel, firstName, lastName, email);
            viewModel.LastPageUrl = GetBackButtonUrl();
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.CanViewVendorManagement = false;
            return View(ViewNames.Profile, viewModel);
        }
        /// <summary>
        /// the action called when viewing the currently logged in user's profile
        /// </summary>
        /// <returns>the profile for that user</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewUser()
        {
            int userId = GetUserId();
            ProfileViewModel viewModel = CreateViewUserViewModel(userId);
            viewModel.SetTabContext(viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, theProfileService.IsPermanentCredentials(userId), !viewModel.IsVerify, HasMyWorkspacePermission(), viewModel.GeneralInfo.IsClient);
            viewModel.SetMenuTitle(userId, viewModel.GeneralInfo.UserId);
            viewModel.CanViewVendorManagement = false;
            return View(ViewNames.Profile, viewModel);
        }
        /// <summary>
        /// Vendor management modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult VendorManagementModal()
        {
            return PartialView(ViewNames.VendorManagementModal);
        }
        /// <summary>
        /// The action called when viewing the user is viewing their profile
        /// for verification
        /// </summary>
        /// <returns>the profile for that user</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewUserToVerify()
        {
            int userId = GetUserId();
            ProfileViewModel viewModel = CreateViewUserViewModel(userId);
            viewModel.SetTabContext(userId, viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, theProfileService.IsPermanentCredentials(userId), !viewModel.IsVerify, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(userId, viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.CanViewVendorManagement = false;
            return View(ViewNames.Profile, viewModel);
        }
        internal virtual ProfileViewModel CreateViewUserViewModel(int userId)
        {
            var userInfoId = theProfileService.GetUserInfoId(userId);
            ProfileViewModel viewModel = PopulateViewModelWithAUsersData(userInfoId);

            viewModel.LastPageUrl = GetBackButtonUrl();
            viewModel.IsMyProfile = true;
            viewModel.IsVerify = theProfileService.IsProfileVerificationRequired(userId);
            viewModel.IsUserProfileVerified = theProfileService.IsUserProfileVerified(userId);

            return viewModel;
        }
        /// <summary>
        /// the action called when viewing a specific user
        /// </summary>
        /// <param name="userInfoId">the users userInfoId</param>
        /// <returns>the profile for that user</returns>
        [ControllerHelpers.RequireRequestValueAttribute("userInfoId")]
        [Sra.P2rmis.Web.Common.RestrictedManageUserAccounts(Operations = Permissions.UserProfileManagement.ManageUserAccounts + "," +  Permissions.UserProfileManagement.RestrictedManageUserAccounts)]
        public ActionResult ViewUser(int userInfoId)
        {
            ProfileViewModel viewModel = PopulateViewModelWithAUsersData(userInfoId);
            SetTabs(viewModel);
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.LastPageUrl = GetBackButtonUrl();
            int loggedInUserId = GetUserId();
            var clientIds = theProfileService.GetAssignedActiveClients(loggedInUserId).ModelList.ToList().ConvertAll(x => x.ClientId);
            viewModel.ClientsBlocked = theProfileService.GetUserClientBlocks(userInfoId, clientIds);
            viewModel.CanViewVendorManagement = viewModel.IsReviewer;
            viewModel.IsSroValue = theProfileService.IsSro(GetUserId());

            return View(ViewNames.Profile, viewModel);
        }
        /// <summary>
        /// the action called when viewing a specific user
        /// </summary>
        /// <param name="userInfoId">the users userInfoId</param>
        /// <param name="showSendCredentialsModal">Show the SendCredential Modal</param>
        /// <returns>the profile for that user</returns>
        [ControllerHelpers.RequireRequestValueAttribute("userInfoId")]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult ViewNewUser(int userInfoId, bool showSendCredentialsModal)
        {
            ProfileViewModel viewModel = PopulateViewModelWithAUsersData(userInfoId);
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.AccountDetails.ShowSendCredentialsModal = showSendCredentialsModal;
            viewModel.CanViewVendorManagement = viewModel.IsReviewer;
            return View(ViewNames.Profile, viewModel);
        }
        /// <summary>
        /// Action for viewing a user's participation history
        /// </summary>
        /// <param name="userInfoId">the users userInfoId</param>
        /// <returns>the profile for that user</returns>
        [ControllerHelpers.RequireRequestValueAttribute("userInfoId")]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult ViewParticipationHistory(int userInfoId)
        {
            ParticipationHistoryViewModel viewModel = PopulateViewModelWithParticipationHistory(userInfoId);
            viewModel.SetTabContext(GetUserId(), null, viewModel.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), null, HasMyWorkspacePermission());
            viewModel.LastPageUrl = GetBackButtonUrl();

            viewModel.LastPageUrl = GetBackButtonUrl();
            return View(ViewNames.ParticipationHistory, viewModel);
        }
        /// <summary>
        /// Action for viewing a user's own participation history
        /// </summary>
        /// <returns>the profile for that user</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewParticipationHistory()
        {
            var userInfoId = theProfileService.GetUserInfoId(GetUserId());
            ParticipationHistoryViewModel viewModel = PopulateViewModelWithParticipationHistory(userInfoId);
            viewModel.IsMyProfile = true;
            viewModel.SetTabContext(GetUserId(), GetUserId(), viewModel.UserInfoId, theProfileService.IsPermanentCredentials(GetUserId()), theProfileService.IsUserProfileVerified(GetUserId()), HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), GetUserId(), HasMyWorkspacePermission());
            viewModel.LastPageUrl = GetBackButtonUrl();
            return View(ViewNames.ParticipationHistory, viewModel);
        }
      
        /// <summary>
        /// Create a user's profile.
        /// </summary>
        /// <param name="viewModel">The User Profile view's view model</param>
        /// <returns>Result of the Save action.  Success redirects to the ViewUser controller method.  Failure returns an updated view model with status information.</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts)]
        public ActionResult CreateProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = GetUserId();
                    int? profileUserId = null;
                    Dictionary<Type, DateTime> thisWillComeFromViewModel = new Dictionary<Type, DateTime>();
                    viewModel.CleanUpAddresses();

                    ICollection<SaveProfileStatus> status = this.theProfileService.CreateProfile(viewModel.GeneralInfo,
                                            viewModel.Websites,
                                            viewModel.InstitutionEmailAddress,
                                            viewModel.PersonalEmailAddress,
                                            viewModel.Addresses,
                                            viewModel.ProfessionalAffiliation,
                                            viewModel.W9Addresses, 
                                            viewModel.PhoneTypeDropdown,
                                            viewModel.AlternateContactTypeDropdown,
                                            viewModel.UserPhones,
                                            viewModel.AlternateContactPersons,
                                            viewModel.UserDegrees,
                                            viewModel.MilitaryServiceAndRank, viewModel.MilitaryStatus,
                                            viewModel.MilitaryServiceId,
                                            viewModel.VendorInfoIndividual, 
                                            viewModel.VendorInfoInstitutional, viewModel.UserClients, userId, viewModel.IsMyProfile, ref profileUserId);
                    
                    viewModel.StatusMessages = MessageService.GetMessages(status);
                    //
                    // Need to refresh the user data in case there was an add.
                    //
                    if ((status.Count == 1) && (status.First() == SaveProfileStatus.Success))
                    {
                        viewModel.AccountDetails.ShowSendCredentialsModal = this.theProfileService.IsSendCredentialsEnabledAtUserCreation(viewModel.GeneralInfo.ProfileTypeId.Value);
                        TempData[ViewHelpers.Constants.SuccessMessageKey] = MessageService.GetSuccessMessage(ViewNames.Profile);
                        return RedirectToAction(Routing.UserProfileManagement.ViewNewUser, new { userInfoId = viewModel.GeneralInfo.UserInfoId, showSendCredentialsModal = viewModel.AccountDetails.ShowSendCredentialsModal });
                    }
                    //
                    // If making it this far something went wrong
                    // Add the BL validation messages to the validation summary
                    //
                    AddErrorMessagesToModelState(viewModel);
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    ModelState.AddModelError(string.Empty, MessageService.FailedSave);
                }
            }
            PopulateProfileDropdowns(viewModel, GetUserId());
            PopulateUserClients(viewModel);
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            return View(Routing.UserProfileManagement.Views.Profile, viewModel);
        }
        /// <summary>
        /// Save the a user's profile information.
        /// </summary>
        /// <param name="viewModel">The User Profile view's view model</param>
        /// <returns>Result of the Save action.  Success redirects to the ViewUser controller method.  Failure returns an updated view model with status information.</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.UserProfileManagement.ManageUserAccounts + "," + Permissions.UserProfileManagement.RestrictedManageUserAccounts)]
        public ActionResult SaveProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Dictionary<Type, DateTime> thisWillComeFromViewModel = new Dictionary<Type, DateTime>();
                    bool w9Verify = false;
                    viewModel.CleanUpAddresses();

                    ICollection<SaveProfileStatus> status =
                        this.theProfileService.SaveProfile(viewModel.GeneralInfo.UserId, thisWillComeFromViewModel, w9Verify,
                            viewModel.GeneralInfo,
                            viewModel.Websites,
                            viewModel.InstitutionEmailAddress,
                            viewModel.PersonalEmailAddress,
                            viewModel.Addresses,
                            viewModel.ProfessionalAffiliation,
                            viewModel.W9Addresses, 
                            viewModel.PhoneTypeDropdown,
                            viewModel.AlternateContactTypeDropdown,
                            viewModel.UserPhones,
                            viewModel.AlternateContactPersons,
                            viewModel.UserDegrees,
                            viewModel.MilitaryServiceAndRank, viewModel.MilitaryStatus,
                            viewModel.MilitaryServiceId,
                            viewModel.VendorInfoIndividual,
                            viewModel.VendorInfoInstitutional, viewModel.UserClients, GetUserId(), viewModel.IsMyProfile, viewModel.IsUpdateUser);

                    viewModel.StatusMessages = MessageService.GetMessages(status);
                    //
                    // Need to refresh the user data in case there was an add.
                    //
                    if ((status.Count == 1) && (status.First() == SaveProfileStatus.Success))
                    {
                        
                        TempData[ViewHelpers.Constants.SuccessMessageKey] = MessageService.GetSuccessMessage(ViewNames.Profile);
                        return RedirectToAction(Routing.UserProfileManagement.ViewUser, new { userInfoId = viewModel.GeneralInfo.UserInfoId });
                    }
                    //
                    // If making it this far something went wrong
                    // Add the BL validation messages to the validation summary
                    //
                    AddErrorMessagesToModelState(viewModel);
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    ModelState.AddModelError(string.Empty, MessageService.FailedSave);
                }
            }
            //
            // Since the view model is reused in this case and the CanManagePassword flag
            // is not hidden, need to recalculate it.  Same thing with several other objects.
            //
            viewModel.CanManagePassword = CanManagePassword(viewModel.GeneralInfo.UserId, GetUserId());
            viewModel.AreRolesDisplayed = AreRolesDisplayed();
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, true, true, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            viewModel.PermanentCredentials = theProfileService.IsPermanentCredentials(viewModel.GeneralInfo.UserId);
            viewModel.AccountDetails = theProfileService.GetUserManageAccount(theLookupService, viewModel.GeneralInfo.UserInfoId);
            viewModel.VendorInfoIndividual = theProfileService.SaveUserVendorId(viewModel.GeneralInfo.UserInfoId, viewModel.VendorInfoIndividual.VendorId, viewModel.VendorInfoIndividual.VendorName, viewModel.GeneralInfo.UserId, true);
            viewModel.VendorInfoInstitutional = theProfileService.SaveUserVendorId(viewModel.GeneralInfo.UserInfoId, viewModel.VendorInfoInstitutional.VendorId, viewModel.VendorInfoInstitutional.VendorName, viewModel.GeneralInfo.UserId, false);
            PopulateProfileDropdowns(viewModel, GetUserId());
            PopulateUserClients(viewModel);
            PopulateUserResume(viewModel);
            PopulateW9Address(viewModel);
            return View(Routing.UserProfileManagement.Views.Profile, viewModel);
        }

        /// <summary>
        /// Save the a user's own profile information.
        /// </summary>
        /// <param name="viewModel">The User Profile view's view model</param>
        /// <returns>Result of the Save action.  Success redirects to the ViewUser controller method.  Failure returns an updated view model with status information.</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveMyProfile(ProfileViewModel viewModel)
        {
            HttpContext.Trace.Write("SaveMyProfile: Starts.");
            if (ModelState.IsValid)
            {
                HttpContext.Trace.Write("SaveMyProfile: ModelState is valid.");
                try
                {
                    Dictionary<Type, DateTime> thisWillComeFromViewModel = new Dictionary<Type, DateTime>();
                    bool w9Verify = viewModel.IsVerify;
                    viewModel.CleanUpAddresses();

                    ICollection<SaveProfileStatus> status =
                        this.theProfileService.SaveProfile(viewModel.GeneralInfo.UserId, thisWillComeFromViewModel, w9Verify,
                            viewModel.GeneralInfo,
                            viewModel.Websites,
                            viewModel.InstitutionEmailAddress,
                            viewModel.PersonalEmailAddress,
                            viewModel.Addresses,
                            viewModel.ProfessionalAffiliation,
                            viewModel.W9Addresses, viewModel.PhoneTypeDropdown,
                            viewModel.AlternateContactTypeDropdown,
                            viewModel.UserPhones,
                            viewModel.AlternateContactPersons,
                            viewModel.UserDegrees,
                            viewModel.MilitaryServiceAndRank, viewModel.MilitaryStatus,
                            viewModel.MilitaryServiceId,
                            viewModel.VendorInfoIndividual,
                            viewModel.VendorInfoInstitutional, viewModel.UserClients, GetUserId(), viewModel.IsMyProfile, viewModel.IsUpdateUser);

                    foreach (var s in status)
                    {                        
                        HttpContext.Trace.Write("SaveMyProfile: Status " + s.ToString());
                    }
                    HttpContext.Trace.Write("SaveMyProfile: Status Count " + status.Count());
                    viewModel.StatusMessages = MessageService.GetMessages(status);

                    viewModel.IsVerify = theProfileService.IsProfileVerificationRequired(GetUserId());
                    //
                    // Need to refresh the user data in case there was an add.
                    //
                    if ((status.Count == 1) && (status.First() == SaveProfileStatus.Success))
                    {
                        //
                        // If we are showing this view because the user needed to verify something then 
                        // we need to enable the menus
                        //
                        EnableMenus(theProfileService.IsUserProfileVerified(GetUserId()));
                        TempData[ViewHelpers.Constants.SuccessMessageKey] = MessageService.GetSuccessMessage(ViewNames.Profile);
                        return RedirectToAction(Routing.UserProfileManagement.ViewMyProfile);
                    }
                    //
                    // If making it this far something went wrong
                    // Add the BL validation messages to the validation summary
                    //
                    AddErrorMessagesToModelState(viewModel);
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    ModelState.AddModelError(string.Empty, MessageService.FailedSave);
                }
            }
            PopulateProfileDropdowns(viewModel, GetUserId());
            PopulateUserClients(viewModel);
            PopulateUserResume(viewModel);
            PopulateW9Address(viewModel);
            viewModel.SetTabContext(GetUserId(), viewModel.GeneralInfo.UserId, viewModel.GeneralInfo.UserInfoId, theProfileService.IsPermanentCredentials(GetUserId()), !viewModel.IsVerify, HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), viewModel.GeneralInfo.UserId, HasMyWorkspacePermission());
            return View(ViewNames.Profile, viewModel);
        }
        /// <summary>
        /// Enable the menus if we they were disabled for verification
        /// </summary>
        /// <param name="isVerify">Verification indicator</param>
        internal virtual void EnableMenus(bool isVerify)
        {
            //
            // We control the UI and other responses via a session variable.
            //
            if (isVerify)
            {
                Session[SessionVariables.Verified] = 1;
            }
        }
        /// <summary>
        /// View resume
        /// </summary>
        /// <param name="resumeId">The resume identifier</param>
        /// <param name="fileName">The resume file name</param>
        /// <returns>The fileContentResult containing the binary contents of the resume</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewResume(int resumeId, string fileName)
        {
            ActionResult result = null;
            try
            {
                byte[] resumeContents = theProfileService.RetrieveCV(theFileService, resumeId); 
                result = File(resumeContents, "application/pdf", fileName);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
                //
                // Redirect to file not found error page
                //
                return RedirectToAction("FileNotFound", "ErrorPage");
            }
            return result;
        }
        /// <summary>
        /// View resume file in embedded viewer
        /// </summary>
        /// <param name="resumeId">The resume identifier</param>
        /// <param name="fileName">The resume file name</param>
        /// <returns>The fileContentResult containing the binary contents of the resume</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewResumeFile(int resumeId, string fileName)
        {
            var fileUrl = $"/UserProfileManagement/ViewResume?resumeid={resumeId}&fileName={fileName}";
            //file and download URL is same as this is PDF document
            return PdfViewer(fileUrl,fileUrl);
        }
        /// <summary>
        /// Views the resume by user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns>The fileContentResult containing the binary contents of the resume</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewResumeByUserInfoId(int userInfoId)
        {
            IResumeModel userResume = theProfileService.GetUserResume(userInfoId);
            return ViewResumeFile(userResume.ResumeId, userResume.ResumeDisplayName);
        }
        /// <summary>
        /// Add resume
        /// </summary>
        /// <returns>The view for adding a resume</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult AddResume()
        {
            return PartialView(ViewNames.AddResume);
        }
        /// <summary>
        /// Upload resume
        /// </summary>
        /// <param name="fileResume">The resume file posted from client</param>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>The result in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult SaveResume(HttpPostedFileBase fileResume, int userInfoId)
        {
            ResumeModel resume = null;
            bool isSuccessful = false;
            try
            {
                // Upload resume
                resume = theProfileService.GetUserResume(userInfoId) as ResumeModel;
                var resumeId = (resume != null) ? resume.ResumeId : 0;
                SaveResumeResults results = theProfileService.Save(theFileService, fileResume.InputStream, fileResume.FileName, userInfoId, resumeId, GetUserId()) as SaveResumeResults;
                if (results.Status != null)
                {
                    List<SaveResumeStatus> statuses = results.Status.ToList<SaveResumeStatus>();
                    resume = theProfileService.GetUserResume(userInfoId) as ResumeModel;
                    resume.StatusMessages = MessageService.GetMessages(statuses);
                    isSuccessful = (statuses.Count == 1 && statuses[0] == SaveResumeStatus.Success);
                }
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
                resume.StatusMessages.Add(MessageService.FailedSave);
            }
            //TO BE REFACTORED: use camel case
            return Json(new { Success = isSuccessful, Resume = resume });
        }
        /// <summary>
        /// Retrieves the viewable SystemRoles for the provided profile type.
        /// </summary>
        /// <param name="profileTypeId">ProfileType identifier</param>
        /// <returns>SystemRole list in JSON format.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult GetSystemRolesForProfileType(int? profileTypeId)
        {
            IEnumerable<IListEntry> result = new List<IListEntry>();
            try
            {
                int userId = GetUserId();
                int? userSystemPriorityOrder = theProfileService.GetUsersSystemPriorityOrder(userId);
                int userProfileTypeId = theProfileService.GetUsersProfileType(userId);
                result = this.theLookupService.ListProfileTypesRoles(profileTypeId, null, userProfileTypeId, userSystemPriorityOrder).ModelList;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Populates military rank drop down menu
        /// </summary>
        /// <param name="service">The service name</param>
        /// <returns>The military ranks associated with the service in JSON string</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult PopulateMilitaryRankDropdown(string service)
        {
            var jsonResult = string.Empty;
            try
                {
                IEnumerable<WebModels.Lists.IListEntry> ranks = this.theLookupService.ListMilitaryRanks(service).ModelList;
                jsonResult = JsonConvert.SerializeObject(ranks);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Populates the password management view
        /// </summary>
        /// <returns>View to manage a password</returns>
        [HttpGet]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult PasswordManagement()
        {
            var userId = GetUserId();
            PasswordManagementViewModel viewModel = new PasswordManagementViewModel();
            try
            {
                viewModel.QuestionDropdown = theLookupService.ListRecoveryQuestions().ModelList.ToList();
                viewModel.SecurityInfo = theProfileService.GetUserPasswordAndSecurityQuestionInfo(userId);
                viewModel.SecurityInfo.SecurityQuestionsAndAnswers = theProfileService.EnsureSuffientModels<UserSecurityQuestionAnswerModel>(viewModel.SecurityInfo.SecurityQuestionsAndAnswers, UserSecurityQuestionAnswerModel.MinimumEntries, UserSecurityQuestionAnswerModel.InitializeModel);
                var passwordExpired = viewModel.SecurityInfo.EffectiveDaysUntilPasswordExpiration < 0;
                if (passwordExpired)
                {
                    Session[SessionVariables.AuthorizedActionList] = null;
                    Session[SessionVariables.PasswordAgeExpired] = 1;
                }

                var permCredentials = theProfileService.IsPermanentCredentials(userId) && !passwordExpired;

                // add in tabs filter
                viewModel.SetTabContext(userId, userId, null, permCredentials, theProfileService.IsUserProfileVerified(userId), HasMyWorkspacePermission());
                viewModel.SetMenuTitle(userId, userId);
                viewModel.LastPageUrl = GetBackButtonUrl();
            }

            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return View(viewModel);
        }
        /// <summary>
        /// Sends password and other security information to BL for saving.
        /// </summary>
        /// <returns>View on failure, redirect on success</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult PasswordManagement(PasswordManagementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isModify = viewModel.SecurityInfo.SecurityQuestionsAndAnswers.Exists(x => x.UserAccountRecoveryId != 0);
                    var currentSecurityInfo = theProfileService.GetUserPasswordAndSecurityQuestionInfo(GetUserId());

                    if ((isModify && viewModel.SecurityInfo.SecurityQuestionsAndAnswers.Count > 0) ||
                        (!isModify && currentSecurityInfo.SecurityQuestionsAndAnswers.Count == 0))
                    {
                        var status = theProfileService.SaveSecurityQuestions(viewModel.SecurityInfo, GetUserId(),theMailService);

                        // setup action status for display
                        if (status != SaveSecurityQuestionStatus.NoActionAttempted)
                        {

                            List<SaveSecurityQuestionStatus> results = new List<SaveSecurityQuestionStatus>();
                            results.Add(status);

                            TempData[ViewHelpers.Constants.SuccessMessageKey] =
                                MessageService.GetMessages(results).FirstOrDefault();
                            // Set session for the menu to display
                            if (status == SaveSecurityQuestionStatus.Success ||
                                status == SaveSecurityQuestionStatus.PasswordSuccess)
                            {
                                Session[SessionVariables.CredentialPermanent] = 1;
                                Session[SessionVariables.PasswordAgeExpired] = 0;
                            }
                        }

                        //the authorized action list will be null when saving a password which replaces an expired password
                        if (Session[SessionVariables.AuthorizedActionList] == null)
                        {
                            var authorizedList = theAccessAccountService.GetUserOperations(GetUserId());
                            Session[SessionVariables.AuthorizedActionList] = authorizedList;
                        }

                        return RedirectToAction(Routing.UserProfileManagement.PasswordManagement);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, MessageService.FailedSave);
                        return RedirectToAction(Routing.UserProfileManagement.PasswordManagement);
                    }
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    ModelState.AddModelError(string.Empty, MessageService.FailedSave);
                }   
            }

            //repopulate dropdowns and any other data not coming back from the view model
            viewModel.QuestionDropdown = theLookupService.ListRecoveryQuestions().ModelList.ToList();
            viewModel.SetTabContext(GetUserId(), GetUserId(), null, theProfileService.IsPermanentCredentials(GetUserId()), theProfileService.IsUserProfileVerified(GetUserId()), HasMyWorkspacePermission());
            viewModel.SetMenuTitle(GetUserId(), GetUserId(), HasMyWorkspacePermission());
            return View(Routing.UserProfileManagement.Views.PasswordManagementView, viewModel);
        }
        /// <summary>
        /// Gets the vendor information.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult GetVendorInfo(int userInfoId)
        {
            UserVendorModel results = new UserVendorModel();
            try
            {
                results.getIndividual = theProfileService.GetVendorId(userInfoId, true);
                results.getInstitutional = theProfileService.GetVendorId(userInfoId, false);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
                ModelState.AddModelError(string.Empty, MessageService.FailedSave);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }        
        /// <summary>
        /// Saves the institutional vendor information.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Setup.General)]
        public ActionResult SaveInstitutionalVendorInfo(int userInfoId, string vendorId, string vendorName)
        {
            UserVendorModel saveInfo = new UserVendorModel();
            try
            {
                saveInfo = theProfileService.SaveUserVendorId(userInfoId, vendorId, vendorName, GetUserId(), false);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
                ModelState.AddModelError(string.Empty, MessageService.FailedSave);
            }
            return Json(saveInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Validation support
        /// <summary>
        /// Determines if the email address is a duplicate of an existing email address
        /// </summary>
        /// <param name="address">Email address to check</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>True if the address is a duplicate; false otherwise</returns>
        public static bool IsDuplicateEmailAddress(string address, int userInfoId)
        {
            bool result = false;

            UserProfileManagementService service = new UserProfileManagementService();
            result = service.IsDuplicateEmailAddress(address, userInfoId);
            return result;
        }
        /// <summary>
        /// Determines whether [is duplicate individual vendor identifier] [the specified target].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate individual vendor identifier] [the specified target]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDuplicateVendorId(string target, int userInfoId, bool indVendorId)
        {
            bool result = false;

            UserProfileManagementService service = new UserProfileManagementService();
            result = service.IsDuplicateVendorId(target, userInfoId, indVendorId);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordToCheck"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DoesMatchCurrentPassword(string passwordToCheck, int userId)
        {
            UserProfileManagementService service = new UserProfileManagementService();
            var result = service.DoesMatchCurrentPassword(passwordToCheck, userId);
            return result;
        }
        /// <summary>
        /// Checks if a provided password matches the user's previous passwords
        /// </summary>
        /// <param name="passwordToCheck"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DoesMatchPreviousPasswords(string passwordToCheck, int userId)
        {
            UserProfileManagementService service = new UserProfileManagementService();
            var result = service.DoesMatchPreviousPasswords(passwordToCheck, userId);
            return result;
        }
        /// <summary>
        /// Returns a reference to the PanelManagement service.  This property is primarily intended
        /// for permission validation.
        /// </summary>
        internal IPanelManagementService GetPanelManagementService { get { return this.thePanelManagementService; } }
        #endregion
        #region Helpers
        /// <summary>
        /// populates all dropdowns in the profile page
        /// </summary>
        /// <param name="viewModel">the profile view model</param>
        /// <returns>the profile view model with dropdowns populated</returns>
        internal ProfileViewModel PopulateProfileDropdowns(ProfileViewModel viewModel, int userId)
        {
            viewModel.PrefixDropdown = theLookupService.ListPrefix().ModelList.ToList();
            viewModel.PhoneTypeDropdown = theLookupService.ListPhoneType().ModelList.ToList();
            viewModel.GenderDropdown = theLookupService.ListGender().ModelList.ToList();
            viewModel.EthnicityDropdown = theLookupService.ListEthnicity().ModelList.ToList();
            viewModel.DegreeDropdown = theLookupService.ListDegree().ModelList.ToList();
            viewModel.ProfileTypesDropdown = theLookupService.ListProfileType().ModelList.ToList();
            viewModel.MilitaryServiceDropdown = theLookupService.ListMilitaryService().ModelList;
            viewModel.MilitaryStatusDropdown = theLookupService.ListMilitaryStatusType().ModelList;
            viewModel.StateDropdown = theLookupService.ListStateByName().ModelList.ToList();
            viewModel.CountryDropdown = theLookupService.ListCountryByName().ModelList.ToList();
            viewModel.AlternateContactTypesDropdown = theLookupService.ListAlternateContactType().ModelList;
            viewModel.AccountStatusDropdown = theLookupService.ListDeActivateAccount().ModelList;
            viewModel.AcademicRankDropdown = theLookupService.ListAcademicRank().ModelList;
            viewModel.ProfessionalAffiliationDropdown = theLookupService.ListProfessionalAffiliation().ModelList;
            viewModel.AddressTypeDropdown = theLookupService.ListOrganizationalPersonalAddressType().ModelList;

            // MilitaryServiceId is derived from MilitaryRankId of database table MilitaryRank
            //  1) MilitaryRank is split into two dropdowns
            //     a) Service
            //     b) Rank (for the selected service)
            //
            //  2) need to cover two cases:
            //     a) Validation error when saving profile - view will already contain index values for those dropdowns
            //     b) Indexes need to be derived from MilitaryRankId and dropdowns

            viewModel.MilitaryServiceId = (viewModel.MilitaryServiceId == null || viewModel.MilitaryServiceId == 0) ? ComputeMilitaryServiceIndex(viewModel) : viewModel.MilitaryServiceId;

            //
            // There is a case where the rank drop down requires population.  This happens if the user has only 
            // selected the military service but not a rank.  In which case we need to populate the rank drop down.
            // Otherwise the cascading dropdowns do not work.
            //
            if (viewModel.MilitaryServiceId != null && viewModel.MilitaryServiceId > 0)
            {
                InitializeServiceAndRank(viewModel);
            }
            //
            // Now we need some information about the current user before the appropriate role list is returned.
            //
            int? userSystemPriorityOrder = theProfileService.GetUsersSystemPriorityOrder(userId);
            int userProfileTypeId = theProfileService.GetUsersProfileType(userId);
            viewModel.RoleDropdown = theLookupService.ListProfileTypesRoles(viewModel.GeneralInfo.ProfileTypeId, viewModel.GeneralInfo.RoleOrder, userProfileTypeId, userSystemPriorityOrder).ModelList;
            //
            // This was checking for same profile time, but I changed to be the system priority order.  Checked with Craig but he is looking into it.
            // if kept it will need to be redone, otherwise it can be deleted
            //
            //viewModel.EnableDropDownListForSameProfileType = EnableDropDownListForSameProfileType(viewModel.GeneralInfo.ProfileTypeId, userSystemPriorityOrder, userProfileTypeId, viewModel.GeneralInfo.RoleOrder);

            viewModel.EnableDropDownListForSameProfileType = EnableDropDownListBasedOnSystemPriorityOrder(viewModel, userSystemPriorityOrder);

            return viewModel;
        }
        internal virtual bool EnableDropDownListBasedOnSystemPriorityOrder(ProfileViewModel viewModel, int? userSystemPriorityOrder)
        {
            int? targetUserSystemPriorityOrder = (viewModel.GeneralInfo.UserId > 0) ? theProfileService.GetUsersSystemPriorityOrder(viewModel.GeneralInfo.UserId) : null;
            return EnableDropDownListBasedOnSystemPriorityOrder(targetUserSystemPriorityOrder, userSystemPriorityOrder.Value);
        }
        /// <summary>
        /// Determines if the dropdown list should be enabled for users with the same user profile type as the user under edit
        /// </summary>
        /// <param name="targetProfileTypeId"></param>
        /// <param name="targetSystemPriorityOrder"></param>
        /// <param name="userProfileTypeId"></param>
        /// <param name="userRoleOrder"></param>
        /// <returns></returns>
        internal virtual bool EnableDropDownListForSameProfileType(int? targetProfileTypeId, int? targetSystemPriorityOrder, int? userProfileTypeId, int? userRoleOrder)
        {
            return (targetProfileTypeId == userProfileTypeId) ? (userRoleOrder == null || targetSystemPriorityOrder >= userRoleOrder) : true;
        }

        /// <summary>
        /// Compute the military service dropdown index based on the military service dropdown
        /// and the user military service and rank
        /// </summary>
        /// <param name="viewModel">The profile view model</param>
        /// <returns>The military dropdown service index</returns>
        internal int? ComputeMilitaryServiceIndex(ProfileViewModel viewModel)
        {
            int? result = null;
            var model = viewModel.MilitaryServiceDropdown.Where(x => x.Value == viewModel.MilitaryServiceAndRank.ServiceBranch).FirstOrDefault();

            if (model != null)
            {
                result = model.Index;
            }

            return result;
        }
        /// <summary>
        /// Determines if the dropdown list should be enabled based on the role order priority.
        /// </summary>
        /// <param name="targetProfileTypeId"></param>
        /// <param name="targetSystemPriorityOrder"></param>
        /// <param name="userProfileTypeId"></param>
        /// <param name="userSystemPriorityOrder"></param>
        /// <returns></returns>
        internal virtual bool EnableDropDownListBasedOnSystemPriorityOrder(int? targetUserSystemPriorityOrder, int userSystemPriorityOrder)
        {
            //
            // One assumes that the default is true because the target user is being created.
            //
            return (targetUserSystemPriorityOrder.HasValue) ? userSystemPriorityOrder <= targetUserSystemPriorityOrder.Value : true;
        }
        /// <summary>
        /// Creates a view model populated with user profile data & dropdown lists.
        /// </summary>
        /// <param name="userInfoId">the users userInfoId</param>
        /// <returns>Populated ProfileViewModel with user data & dropdown data</returns>
        internal ProfileViewModel PopulateViewModelWithAUsersData(int targetUserInfoId)
        {
            ProfileViewModel viewModel = new ProfileViewModel();

            PopulateProfileViewModel(viewModel, targetUserInfoId, GetUserId());
            PopulateUserClients(viewModel);

            PopulateProfileDropdowns(viewModel, GetUserId());

            viewModel.IsBadgeNameEditable = HasPermission(Permissions.UserProfileManagement.ManageUserAccounts);
            viewModel.AreRolesDisplayed = AreRolesDisplayed();
            viewModel.CanManagePassword = CanManagePassword(viewModel.GeneralInfo.UserId, GetUserId());
            viewModel.PermanentCredentials = theProfileService.IsPermanentCredentials(viewModel.GeneralInfo.UserId);
            viewModel.IsVerify = theProfileService.IsProfileVerificationRequired(GetUserId());
            viewModel.IsUserProfileVerified = theProfileService.IsUserProfileVerified(GetUserId());

            return viewModel;
        }
        /// <summary>
        /// Populates the list of available clients
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void PopulateUserClients(ProfileViewModel viewModel)
        {
            viewModel.AvailableUserClients = theProfileService.GetAvailableUserProfileClient().ModelList.ToList();
        }
        /// <summary>
        /// Populated the view model for the Participation History page
        /// </summary>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>Participation History View Model populated from DB</returns>
        internal ParticipationHistoryViewModel PopulateViewModelWithParticipationHistory(int userInfoId)
        {
            ParticipationHistoryViewModel viewModel = new ParticipationHistoryViewModel
            {
                ParticipationHistory = theProfileService.GetParticipationHistory(userInfoId).ModelList.ToList()
            };
            int userId = theProfileService.GetUserId(userInfoId);
            viewModel.CanManagePassword = CanManagePassword(userId, GetUserId());
            viewModel.UserInfoId = userInfoId;
            viewModel.PermanentCredentials = theProfileService.IsPermanentCredentials(userId);
            return viewModel;
        }
        /// <summary>
        /// Populates the UserResume section of the view model
        /// </summary>
        /// <param name="viewModel"></param>
        private void PopulateUserResume(ProfileViewModel viewModel)
        {
            if (viewModel.GeneralInfo != null && viewModel.GeneralInfo.UserInfoId > 0)
                viewModel.UserResume = theProfileService.GetUserResume(viewModel.GeneralInfo.UserInfoId) as ResumeModel;
        }
        private void PopulateW9Address(ProfileViewModel viewModel)
        {
            if (viewModel.GeneralInfo.ProfileTypeId == ProfileViewModel.ReviewerProfileTypeId || viewModel.GeneralInfo.ProfileTypeId == ProfileViewModel.ProspectProfileTypeId)
            {
                viewModel.W9Addresses = theProfileService.GetW9Addresses(viewModel.GeneralInfo.UserInfoId) as W9AddressModel;
            }
        }
        /// <summary>
        /// Populates the view model with the search parameters
        /// </summary>
        /// <param name="viewModel">The view model to be populated</param>
        /// <param name="firstName">The user's first name</param>
        /// <param name="lastName">The user's last name</param>
        /// <param name="email">The user's email address</param>
        private void PopulateViewModelWithSearchParameters(ProfileViewModel viewModel, string firstName, string lastName, string email)
        {
            viewModel.GeneralInfo.FirstName = firstName;
            viewModel.GeneralInfo.LastName = lastName;
            viewModel.InstitutionEmailAddress.Address = email;
        }
        /// <summary>
        /// Ensures sufficient buffer space for Razor controls.  This method is intended to be used
        /// when creating a new user.
        /// </summary>
        /// <param name="viewModel">The view model</param>
        internal virtual void EnsureViewModelBufferSpace(ProfileViewModel viewModel)
        {
            viewModel.Websites = theProfileService.EnsureSuffientWebsiteModels(viewModel.Websites);
            viewModel.InstitutionEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.InstitutionEmailAddress, EmailAddressModel.InitializeModel);
            viewModel.PersonalEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.PersonalEmailAddress, EmailAddressModel.InitializeModel);
            viewModel.UserDegrees = theProfileService.EnsureSuffientModels<UserDegreeModel>(viewModel.UserDegrees, UserDegreeModel.MinimumEntries, UserDegreeModel.InitializeModel);
            viewModel.AlternateContactPersons = theProfileService.EnsureSuffientModels<UserAlternateContactPersonModel>(viewModel.AlternateContactPersons, UserAlternateContactPersonModel.MinimumEntries, UserAlternateContactPersonModel.InitializeModel);
            // Handles multiple phones for an alternate contact person
            for (var i = 0; i < viewModel.AlternateContactPersons.Count; i++)
            {
                viewModel.AlternateContactPersons[i].AlternateContactPhone = theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(viewModel.AlternateContactPersons[i].AlternateContactPhone, UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
            }
            viewModel.Addresses = theProfileService.EnsureSuffientModels<AddressInfoModel>(viewModel.Addresses, AddressInfoModel.MinimumEntries, AddressInfoModel.InitializeModel);
            viewModel.IsUpdateUser = false;
        }
        /// <summary>
        /// populates the users profile information
        /// </summary>
        /// <param name="viewModel">the profile view model</param>
        /// <param name="userInfoId">the users info id</param>
        /// <param name="currentUserId">User entity identifier of current user</param>
        /// <remarks>Candidate for re-factoring out the EnsureSuffient calls & replacing with EnsureViewModelBufferSpace()</remarks>
        /// <returns>the populated profile view model</returns>
        public ProfileViewModel PopulateProfileViewModel(ProfileViewModel viewModel, int userInfoId, int currentUserId)
        {
            viewModel.GeneralInfo = theProfileService.GetUserGeneralInfo(userInfoId) as GeneralInfoModel;
            //
            // If we have found a user then populate the view model with the user's data.  Otherwise
            // we are creating a user & they do not have any information to display.
            //
            if (viewModel.GeneralInfo != null)
            {
                viewModel.Websites = theProfileService.GetUserWebsite(userInfoId).ModelList.ToList();
                viewModel.Websites = theProfileService.EnsureSuffientWebsiteModels(viewModel.Websites);
                EnsureWebsiteFormat(viewModel);
                viewModel.InstitutionEmailAddress = theProfileService.GetInstitutionalUserEmailAddress(userInfoId) as EmailAddressModel;
                viewModel.InstitutionEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.InstitutionEmailAddress, EmailAddressModel.InitializeModel);
                viewModel.PersonalEmailAddress = theProfileService.GetPersonalUserEmailAddress(userInfoId) as EmailAddressModel;
                viewModel.PersonalEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.PersonalEmailAddress, EmailAddressModel.InitializeModel);
                EnsureEmailAddressPrimaryFlag(viewModel);
                viewModel.UserDegrees = theProfileService.GetUserDegree(userInfoId).ModelList.OfType<UserDegreeModel>().ToList();
                viewModel.UserDegrees = theProfileService.EnsureSuffientModels<UserDegreeModel>(viewModel.UserDegrees, UserDegreeModel.MinimumEntries, UserDegreeModel.InitializeModel);
                viewModel.MilitaryServiceAndRank = theProfileService.GetUserMilitaryRank(userInfoId);
                viewModel.MilitaryStatus = theProfileService.GetUserMilitaryStatus(userInfoId);
                viewModel.AlternateContactPersons = theProfileService.GetAlternativeContactPersons(userInfoId).ModelList.OfType<UserAlternateContactPersonModel>().ToList();
                viewModel.AlternateContactPersons = theProfileService.EnsureSuffientModels<UserAlternateContactPersonModel>(viewModel.AlternateContactPersons, UserAlternateContactPersonModel.MinimumEntries, UserAlternateContactPersonModel.InitializeModel);
                // Handles multiple phones for an alternate contact person
                for (var i = 0; i < viewModel.AlternateContactPersons.Count; i++)
                {
                    viewModel.AlternateContactPersons[i].AlternateContactPhone = theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(viewModel.AlternateContactPersons[i].AlternateContactPhone, UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
                }
                EnsureAlternatePrimaryFlag(viewModel);
                viewModel.Addresses = theProfileService.GetOrganizationalPersonalAddresses(userInfoId).ModelList.OfType<AddressInfoModel>().ToList();
                viewModel.Addresses = theProfileService.EnsureSuffientModels<AddressInfoModel>(viewModel.Addresses, AddressInfoModel.MinimumEntries, AddressInfoModel.InitializeModel);
                viewModel.CleanUpAddresses();
                EnsureAddressPrimaryFlag(viewModel);
                viewModel.ProfessionalAffiliation = theProfileService.GetUserProfessionalAffiliation(userInfoId) as ProfessionalAffiliationModel;
                viewModel.W9Addresses = theProfileService.GetW9Addresses(userInfoId) as W9AddressModel;
                viewModel.UserPhones = theProfileService.GetUserPhones(userInfoId).ModelList.OfType<PhoneNumberModel>().ToList();
                viewModel.UserResume = theProfileService.GetUserResume(userInfoId) as ResumeModel;
                viewModel.UserClients = theProfileService.GetAssignedUserProfileClient(viewModel.GeneralInfo.UserId).ModelList.ToList();
                viewModel.IsUpdateUser = true;
                viewModel.ActiveUserClients = viewModel.UserClients.Where(x => x.IsActive).ToList();
                viewModel.AccountDetails =
                    theProfileService.GetUserManageAccount(theLookupService, userInfoId);
                viewModel.MisconductType = LookupService.LookupSystemProfileTypeMisconductId;
                viewModel.VendorInfoIndividual = theProfileService.GetVendorId(userInfoId, true);
                viewModel.VendorInfoInstitutional = theProfileService.GetVendorId(userInfoId, false);
                //
                // If the user is displaying their own profile then we do not want to enable the role role drop down if it is displayed
                //
                viewModel.AreRolesDisabled = (currentUserId == viewModel.GeneralInfo.UserId);
                
                viewModel.CanUploadCv = true;
            }
            else
            {
                viewModel.IsUpdateUser = false;
                viewModel.CanUploadCv = false;
            }

            return viewModel;
        }
        /// <summary>
        /// Populates the users profile information
        /// </summary>
        /// <param name="viewModel">the profile view model</param>
        /// <param name="userInfoId">the users info id</param>
        /// <remarks>Candidate for refactoring out the EnsureSuffient calls & replacing with EnsureViewModelBufferSpace()</remarks>
        /// <returns>the populated profile view model</returns>
        public ProfileViewModel PopulateNewProfileViewModel(ProfileViewModel viewModel)
        {
            if (viewModel != null)
            {
                viewModel.Websites = theProfileService.EnsureSuffientWebsiteModels(viewModel.Websites);
                EnsureWebsiteFormat(viewModel);
                viewModel.InstitutionEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.InstitutionEmailAddress, EmailAddressModel.InitializeModel);
                viewModel.PersonalEmailAddress = theProfileService.EnsureInitializeModel<EmailAddressModel>(viewModel.PersonalEmailAddress, EmailAddressModel.InitializeModel);
                EnsureEmailAddressPrimaryFlag(viewModel);
                viewModel.UserDegrees = theProfileService.EnsureSuffientModels<UserDegreeModel>(viewModel.UserDegrees, UserDegreeModel.MinimumEntries, UserDegreeModel.InitializeModel);
                InitializeServiceAndRank(viewModel);
                viewModel.AlternateContactPersons = theProfileService.EnsureSuffientModels<UserAlternateContactPersonModel>(viewModel.AlternateContactPersons, UserAlternateContactPersonModel.MinimumEntries, UserAlternateContactPersonModel.InitializeModel);
                // Handles multiple phones for an alternate contact person
                for (var i = 0; i < viewModel.AlternateContactPersons.Count; i++)
                {
                    viewModel.AlternateContactPersons[i].AlternateContactPhone = theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(viewModel.AlternateContactPersons[i].AlternateContactPhone, UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
                }
                EnsureAlternatePrimaryFlag(viewModel);
                viewModel.ProfessionalAffiliation = new ProfessionalAffiliationModel();
                viewModel.Addresses = theProfileService.EnsureSuffientModels<AddressInfoModel>(viewModel.Addresses, AddressInfoModel.MinimumEntries, AddressInfoModel.InitializeModel);
                EnsureAddressPrimaryFlag(viewModel);
                viewModel.IsUpdateUser = false;
                viewModel.CanUploadCv = false;
                viewModel.AreRolesDisplayed = AreRolesDisplayed();
                viewModel.MisconductType = LookupService.LookupSystemProfileTypeMisconductId;
                //
                // Populate the list of clients
                //
                PopulateUserClients(viewModel);
            }
            return viewModel;
        }
        /// <summary>
        /// The military service & rank values are retrieved from the same table and are not independent tables.
        /// As a consequence of this a particular service will have multiple index values.  Retrieve the 
        /// specific index value used in the dropdowns based on the user's service.
        /// </summary>
        /// <param name="viewModel">The viewmodel for the user profile view</param>
        public void InitializeServiceAndRank(ProfileViewModel viewModel)
        {
            //
            // If the user has a service selected then set up the service id
            //
            var serviceEntity = viewModel.MilitaryServiceDropdown.Where(x => x.Index == viewModel.MilitaryServiceId).DefaultIfEmpty().First();
            var serivceBranch = serviceEntity != null ? serviceEntity.Value : string.Empty;
            if (serivceBranch != UserMilitaryRankModel.NoServiceBranch)
            {
                viewModel.MilitaryServiceId = viewModel.MilitaryServiceDropdown.FirstOrDefault(x => x.Value == serivceBranch).Index;
                //
                //  This may seem like a deviation from the norm but the Rank dropdown is populated based on the user's
                //  service value.
                //
                viewModel.MilitaryRankDropdown = this.theLookupService.ListMilitaryRanks(serivceBranch).ModelList;
            }
            else
            {
                viewModel.MilitaryServiceId = 0;
            }
        }
        /// <summary>
        /// Ensure email address primary flag. One of the email addresses is primary.
        /// </summary>
        /// <param name="viewModel">The viewmodel for the user profile view</param>
        private void EnsureEmailAddressPrimaryFlag(ProfileViewModel viewModel)
        {
            if (viewModel.InstitutionEmailAddress != null &&
                viewModel.PersonalEmailAddress != null)
            {
                if (viewModel.InstitutionEmailAddress.Primary != true && viewModel.PersonalEmailAddress.Primary != true)
                {
                    viewModel.InstitutionEmailAddress.Primary = true;
                }
            }
        }
        /// <summary>
        /// Ensure address primary flag. One of the alternate is primary.
        /// </summary>
        /// <param name="viewModel">The viewmodel for the user profile view</param>
        private void EnsureAlternatePrimaryFlag(ProfileViewModel viewModel)
        {
            if (viewModel.AlternateContactPersons != null && viewModel.AlternateContactPersons.Count > 0)
            {
                if (!viewModel.AlternateContactPersons.Any(x => x.PrimaryFlag == true))
                {
                    viewModel.AlternateContactPersons[0].PrimaryFlag = true;
                }
                foreach (UserAlternateContactPersonModel person in viewModel.AlternateContactPersons)
                {
                    if (person.AlternateContactPhone != null && person.AlternateContactPhone.Count > 0 &&
                            !person.AlternateContactPhone.Any(x => x.PrimaryFlag == true))
                    {
                        person.AlternateContactPhone[0].PrimaryFlag = true;
                    }
                }
            }
        }
        /// <summary>
        /// Ensure address primary flag. One of the addresses is primary.
        /// </summary>
        /// <param name="viewModel">The viewmodel for the user profile view</param>
        private void EnsureAddressPrimaryFlag(ProfileViewModel viewModel)
        {
            if (viewModel.Addresses != null && viewModel.Addresses.Count > 0)
            {
                if (!viewModel.Addresses.Any(x => x.PrimaryFlag == true))
                {
                    viewModel.Addresses[0].PrimaryFlag = true;
                }
            }
        }
        /// <summary>
        /// Ensure the website address starts with "http".
        /// </summary>
        /// <param name="viewModel">The viewmodel for the user profile view</param>
        private void EnsureWebsiteFormat(ProfileViewModel viewModel)
        {
            if (viewModel.Websites != null && viewModel.Websites.Count > 0)
            {
                for (var i = 0; i < viewModel.Websites.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Websites[i].WebsiteAddress) && 
                            !viewModel.Websites[i].WebsiteAddress.ToLower().StartsWith("http"))
                    {
                        viewModel.Websites[i].WebsiteAddress = "http://" + viewModel.Websites[i].WebsiteAddress;
                    }
                }
            }
        }
        /// <summary>
        /// Transfer error messages to ModelState
        /// </summary>
        /// <param name="viewModel">The User Profile view's view model</param>
        private void AddErrorMessagesToModelState(ProfileViewModel viewModel)
        {
            foreach (var error in viewModel.StatusMessages)
            {
                //
                // For now these errors from the BL are considered global and not associated with an input
                // If we want to associate with a property in the ViewModel a key would have to be passed back
                //
                ModelState.AddModelError(string.Empty, error);
            }
        }
        /// <summary>
        /// Uploads the w9 addresses.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        public ActionResult UploadW9Addresses(List<UserAddressUploadViewModel> addresses)
        {
            ICollection<string> errors = null;
            try
            {
                // Grab any error messages
                var models = addresses.ConvertAll(x => x.GetAddress());
                var getAddresses = theProfileService.UploadW9Addresses(models, GetUserId()).ToList();
                errors = MessageService.GetErrorMessages(getAddresses);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { results = errors });
        }
        /// <summary>
        /// returns manage block modal
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ManageBlock(int userInfoId)
        {
            ProfileViewModel viewModel = new ProfileViewModel();

            var targetUserInfoId = userInfoId;
            PopulateProfileViewModel(viewModel, targetUserInfoId, GetUserId());
            int loggedInUserId = GetUserId();
            viewModel.UserClients = theProfileService.GetAssignedUserProfileClient(loggedInUserId).ModelList.ToList();

            var clientIds = viewModel.UserClients.ConvertAll(x => x.ClientId);

            viewModel.ClientsBlocked = theProfileService.GetUserClientBlocks(userInfoId, clientIds);
            viewModel.HistoryTable = theProfileService.GetUserClientBlockLogs(userInfoId);

            return PartialView(ViewNames.ManageBlock, viewModel);
        }
        #endregion
    }
}