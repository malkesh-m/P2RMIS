using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PiInformationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the PiInformationViewModel class.
        /// </summary>
        /// <param name="applicationPiInformation">The application PI information.</param>
        public PiInformationViewModel(IApplicationPIInformation applicationPiInformation)
        {
            ApplicationId = applicationPiInformation.applicationId;
            LogNumber = applicationPiInformation.LogNumber;
            ApplicationTitle = applicationPiInformation.ApplicationTitle;
            AwardMechanism = applicationPiInformation.AwardMechanism;
            ResearchArea = applicationPiInformation.ResearchArea;
            OrganizationName = applicationPiInformation.Blinded ? ViewHelpers.Constants.Blinded : applicationPiInformation.OrganizationName;
            PiName = applicationPiInformation.Blinded ? ViewHelpers.Constants.Blinded : ViewHelpers.ConstructNameWithComma(applicationPiInformation.FirstName, applicationPiInformation.LastName);
            var partnerPiNameList = new List<string>();
            if (!applicationPiInformation.Blinded)
            {
                foreach (var partnerPi in applicationPiInformation.PartnerPIInformation)
                {
                    partnerPiNameList.Add(ViewHelpers.ConstructNameAndOrganization(partnerPi.FirstName, partnerPi.LastName, partnerPi.OrganizationName));
                }
            }
            PartnerPiNames = string.Join(", ", partnerPiNameList.ToArray());            
            PartnerPiNamesCropped = string.Join(ViewHelpers.Constants.NameSeparator, partnerPiNameList.Take(ViewHelpers.Constants.TopNumberOfPartnerPis).ToArray());
        }
        /// <summary>
        /// Application identifier
        /// </summary>
        public int ApplicationId { get; private set; }
        /// <summary>
        /// Application Log Number
        /// </summary>
        public string LogNumber { get; private set; }
        /// <summary>
        /// Application's Title
        /// </summary>
        public string ApplicationTitle { get; private set; }
        /// <summary>
        /// Application's Award Mechanism
        /// </summary>
        public string AwardMechanism { get; private set; }
        /// <summary>
        /// Application's Research Area
        /// </summary>
        public string ResearchArea { get; private set; }
        /// <summary>
        /// PI's Organization
        /// </summary>
        public string OrganizationName { get; private set; }
        /// <summary>
        /// PI's name
        /// </summary>
        public string PiName { get; private set; }
        /// <summary>
        /// Partner PI Information
        /// </summary>
        public string PartnerPiNames { get; private set; }
        /// <summary>
        /// Partner PI Information cropped
        /// </summary>
        public string PartnerPiNamesCropped { get; private set; }
    }
}