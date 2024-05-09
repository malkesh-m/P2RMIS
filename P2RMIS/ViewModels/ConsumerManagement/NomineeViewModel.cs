using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.ConsumerManagement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class NomineeViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="type">Nominee type</param>
        /// <param name="nominatingOrganization">Nominating organization</param>
        /// <param name="program">Program</param>
        /// <param name="fiscalYear">Fiscal year</param>
        /// <param name="score">Score</param>
        /// <param name="userId">User identifier</param>
        /// <param name="userInfoId">User info identifier</param>
        public NomineeViewModel(string firstName, string lastName, string type, 
            string nominatingOrganization, string program, string fiscalYear, 
            int? score, int? userId, int? userInfoId, int nomineeId)
        {
            Id = nomineeId;
            Name = ViewHelpers.ConstructName(lastName, firstName);
            Type = type;
            NominatingOrganization = nominatingOrganization;
            Program = program;
            FiscalYear = fiscalYear;
            Score = score != null ? Convert.ToString(score) : string.Empty;
            UserId = userId;
            UserInfoId = userInfoId;
        }
        /// <summary>
        /// Nominee Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Nominee name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Nominee type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Nominating organization
        /// </summary>
        [JsonProperty("nominatingOrganization")]
        public string NominatingOrganization { get; set; }
        /// <summary>
        /// Program
        /// </summary>
        [JsonProperty("program")]
        public string Program { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        [JsonProperty("fiscalYear")]
        public string FiscalYear { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        [JsonProperty("score")]
        public string Score { get; set; }
        /// <summary>
        /// User identifier
        /// </summary>
        [JsonProperty("userId")]
        public int? UserId { get; set; }
        /// <summary>
        /// User info identifier
        /// </summary>
        [JsonProperty("userinfoId")]
        public int? UserInfoId { get; set; }
    }
}