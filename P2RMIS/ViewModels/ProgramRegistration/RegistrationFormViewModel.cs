using Sra.P2rmis.WebModels.ProgramRegistration;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.CrossCuttingServices;
using System;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.ViewModels.ProgramRegistration
{
    /// <summary>
    /// The view model for registration forms (tabs)
    /// </summary>
    public class RegistrationFormViewModel
    {
        /// <summary>
        /// Minimum number of phone numbers to display in the alternate contact section.
        /// </summary>
        private const int MinimumNumberPhone = 1;
        /// <summary>
        /// Message to display when the pay rates are not uploaded.
        /// </summary>
        public const string PayRateNotUploadedMessage = "The Fee Schedule is not complete for this contract. This contract cannot be ratified at this time. Please contact help@p2rmis.com.";
        /// <summary>
        /// Data for the document form
        /// </summary>
        public IDocumentForm Form { get; set; }
        /// <summary>
        /// The business category lookup
        /// </summary>
        public BusinessCategoryLookup BusinessCategoryLookup { get; set; }
        /// <summary>
        /// Gets or sets the get alternate contact persons.
        /// </summary>
        /// <value>
        /// The get alternate contact persons.
        /// </value>
        public IUserAlternateContactPersonModel GetAlternateContactPersons { get; set; }
        /// <summary>
        /// Indicates if the Pay Rate warning message is displayed on the
        /// contract confirmation page.
        /// </summary>
        public bool DisplayPayRateMessageOnContractualConfirmation(bool isBypassed, bool isCustomized)
        {
                return (Routing.ProgramRegistration.WorkflowStepKey.Sign == this.Form.FormKey) && (!this.Form.ArePayRatesUploaded) && !isBypassed && !isCustomized;
        }
        /// <summary>
        /// Indicates if the Pay Rate warning message is displayed on the
        /// contractual agreement page.
        /// </summary>
        public bool DisplayPayRateMessageOnContractualAgreement
        {
            get
            {
                return (
                        (Routing.ProgramRegistration.WorkflowStepKey.Contract == this.Form.FormKey) ||
                        (Routing.ProgramRegistration.WorkflowStepKey.ContractCprit == this.Form.FormKey)
                        ) && (!this.Form.ArePayRatesUploaded && !this.Form.ContractData.IsContractBypassed && !this.Form.ContractData.IsContractCustomized);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrationFormViewModel() {
            this.PhoneTypeDropdown = new List<IListEntry>();
            this.UserPhones = new List<PhoneNumberModel>();
        }
        /// <summary>
        /// Gets the current date or signed date.
        /// </summary>
        /// <value>
        /// The current date or signed date.
        /// </value>
        public string CurrentDateOrSignedDate
        {
            get
            {
                return Form.DocumentSignedDateTime != null ? ViewHelpers.FormatDate(Form.DocumentSignedDateTime) : ViewHelpers.FormatDate(DateTime.UtcNow);
            }
        }
        /// <summary>
        /// Phone type dropdown list
        /// </summary>
        public List<IListEntry> PhoneTypeDropdown { get; set; }
        /// <summary>
        /// The user contact phone numbers
        /// </summary>
        public List<PhoneNumberModel> UserPhones { get; set; }
        /// <summary>
        /// Minimum phone numbers to display in the phone numbers section.
        /// </summary>
        private static IReadOnlyCollection<int> RequiredPhoneTypes =
            new ReadOnlyCollection<int>(new List<int> { 9 });
        /// <summary>
        /// Checks the PhoneNumberModel list of phone number models for required entries.  
        /// If the minimum number of phones is not present, then new PhoneNumberModel is added.
        /// </summary>
        /// <returns></returns>
        /// <remarks>TODO:Needs unit tests</remarks>
        public string PopulateMinimumPhones(List<PhoneNumberModel> model)
        {
            //
            // Create a new list to work with.  Then add one or more entries
            // to it to get to the minimum number.
            //
            List<PhoneNumberModel> list = model;
            foreach (int item in RequiredPhoneTypes.TakeWhile(x => list.Count() < RequiredPhoneTypes.Count()))
            {

                var entry = new PhoneNumberModel { PhoneTypeId = 9, International = false };
                if ((list.Any(x => x.PhoneTypeId == 9)) == false)
                {
                    list.Add(entry);
                }
            }
            //
            // And replace the original list with the new list
            //
            model = list;
        return string.Empty;
        }
    }
}