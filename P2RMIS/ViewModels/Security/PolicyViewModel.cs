using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PolicyViewModel
    {
        public PolicyWebModel Policy { get; set; }

        public List<KeyValuePair<int, string>> ClientList { get; set; }
        public List<KeyValuePair<int, string>> PolicyTypeList { get; set; }

        public List<KeyValuePair<int, string>> PolicyRestrictionTypeList { get; set; }

        public List<KeyValuePair<int, string>> WeekDayList { get; set; }

        /// <summary>
        /// Configure the lists
        /// </summary>
        /// <param name="thePolicyTypeList">List of PolicyTypes & their indexes</param>        
        internal void ConfigureLists(List<KeyValuePair<int, string>> theClientList, List<KeyValuePair<int, string>> thePolicyTypeList, List<KeyValuePair<int, string>> thePolicyRestrictionTypeList, List<KeyValuePair<int, string>> theWeekDayList)
        {
            ClientList = theClientList;
            PolicyTypeList = thePolicyTypeList;
            PolicyRestrictionTypeList = thePolicyRestrictionTypeList;
            WeekDayList = theWeekDayList;
        }

    }
}