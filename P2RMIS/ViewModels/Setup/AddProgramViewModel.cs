using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AddProgramViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProgramViewModel"/> class.
        /// </summary>
        public AddProgramViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProgramViewModel"/> class.
        /// </summary>
        /// <param name="fyList">The fy list.</param>
        public AddProgramViewModel(List<IGenericListEntry<int, string>> fyList)
        {
            FiscalYears = fyList.ConvertAll(x => new KeyValuePair<string, string>(x.Value, x.Value));
        }
        /// <summary>
        /// Gets the fiscal years.
        /// </summary>
        /// <value>
        /// The fiscal years.
        /// </value>
        public List<KeyValuePair<string, string>> FiscalYears { get; private set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; set; }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public int ClientMeetingId { get; private set; }
    }
}