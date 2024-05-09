using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.ConsumerManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ConsumerViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConsumerViewModel() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nomineeTypes"></param>
        public ConsumerViewModel(List<KeyValuePair<int, string>> nomineeTypes, 
            List<IListEntry> prefixes, List<IListEntry> genders,
            List<IListEntry> ethnicities, List<IListEntry> phoneTypes, 
            List<IListEntry> states, List<IListEntry> countries,
            List<IListEntry> affected, 
            List<IClientProgramModel> programs, 
            int defaultCountryId,
            int defaultPhoneType1Id,
            int defaultPhoneType2Id)
        {
            NomineeUpdateModel = new NomineeUpdateModel();
            NomineeProgramUpdateModel = new NomineeProgramUpdateModel();
            NomineeSponsorUpdateModel = new NomineeSponsorUpdateModel();

            NomineeTypes = nomineeTypes;
            Prefixes = prefixes.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Genders = genders.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Ethnicities = ethnicities.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            PhoneTypes = phoneTypes.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            States = states.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Countries = countries.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Affected = affected.ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            Programs = programs;

            DefaultCountryId = defaultCountryId;
            DefaultPhoneType1Id = defaultPhoneType1Id;
            DefaultPhoneType2Id = defaultPhoneType2Id;
        }
        /// <summary>
        /// The nominee update view model
        /// </summary>
        public NomineeUpdateModel NomineeUpdateModel { get; set; }
        /// <summary>
        /// The nominee program update view model
        /// </summary>
        public NomineeProgramUpdateModel NomineeProgramUpdateModel { get; set; }
        /// <summary>
        /// The nominee sponsor update view model
        /// </summary>
        public NomineeSponsorUpdateModel NomineeSponsorUpdateModel { get; set; }
        /// <summary>
        /// Nominee types
        /// </summary>
        public List<KeyValuePair<int, string>> NomineeTypes { get; set; }
        /// <summary>
        /// Prefix list
        /// </summary>
        public List<KeyValuePair<int, string>> Prefixes { get; set; }
        /// <summary>
        /// Gender list
        /// </summary>
        public List<KeyValuePair<int, string>> Genders { get; set; }
        /// <summary>
        /// Ethnicity list
        /// </summary>
        public List<KeyValuePair<int, string>> Ethnicities { get; set; }
        /// <summary>
        /// Phone types
        /// </summary>
        public List<KeyValuePair<int, string>> PhoneTypes { get; set; }
        /// <summary>
        /// States
        /// </summary>
        public List<KeyValuePair<int, string>> States { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public List<KeyValuePair<int, string>> Countries { get; set; }
        /// <summary>
        /// Affected entities
        /// </summary>
        public List<KeyValuePair<int, string>> Affected { get; set; }
        /// <summary>
        /// Programs
        /// </summary>
        public List<IClientProgramModel> Programs { get; set; }
        /// <summary>
        /// Default country identifier
        /// </summary>
        public int DefaultCountryId { get; set; }
        /// <summary>
        /// Default phone type 1 identifier
        /// </summary>
        public int DefaultPhoneType1Id { get; set; }
        /// <summary>
        /// Default phone type 2 identifier
        /// </summary>
        public int DefaultPhoneType2Id { get; set; }
    }
}