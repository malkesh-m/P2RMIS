using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class PersonnelViewModel
    {
        public PersonnelViewModel(IPersonnelModel model)
        {
            UserId = model.UserId;
            UserInfoId = model.UserInfoId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Organization = model.Organization;
            Email = model.Email;
            Assignments = model.Assignments.ToList().
                ConvertAll(x => new PersonnelAssignmentViewModel(x));
        }

        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User info identifier
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Organization
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Personnel assignments
        /// </summary>
        public IEnumerable<PersonnelAssignmentViewModel> Assignments { get; set; }
    }

    /// <summary>
    /// Personnel assignment model
    /// </summary>
    public class PersonnelAssignmentViewModel
    {
        public PersonnelAssignmentViewModel(IPersonnelAssignmentModel model)
        {
            FiscalYear = model.FiscalYear;
            MeetingAbbreviation = model.MeetingAbbreviation;
            ProgramAbbreviation = model.ProgramAbbreviation;
        }

        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Meeting abbreviation
        /// </summary>
        public string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
    }
}