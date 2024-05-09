using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// System Templates Service.  Services provided are:
    ///      - Return a system template by name
    ///      - Return the active version of a system template.
    ///      - Return a system template version by ID
    /// </summary>
    public class ManageSystemTemplates: IDisposable
    {
        #region Attributes
        private UnitOfWork unitOfWork;
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        private bool _disposed;
        #endregion
        #region Constructors & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        public ManageSystemTemplates()
        {
            ///
            /// TODO:: refactor for DI
            /// 
            unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Dispose of the service
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose of this service non-controlled objects.
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    _disposed = true;
                }
            }
        }
        #endregion
        #region Services
        ///
        /// TODO: move services methods here
        /// 
        #endregion

        public SystemTemplate GetSystemTemplateByName(String name)
        {
            List<SystemTemplate> retList = unitOfWork.UofwSysTemplateRepository.GetSystemTemplates();

            SystemTemplate emailTemplate = retList.Find
            (
                delegate(SystemTemplate tmplate)
                {
                    return tmplate.Name == name;
                }
            );
            return emailTemplate;
        }

        public SystemTemplateVersion GetActiveVersion(SystemTemplate sysTemp)
        {
            return sysTemp.SystemTemplateVersions.Single(x => x.VersionNumber == sysTemp.VersionId);
        }

        /// <summary>
        /// Gets the system template version by identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>SystemTemplateVersion object</returns>
        public SystemTemplateVersion GetSystemTemplateVersionById(int templateId)
        {
            var template = unitOfWork.UofwSysTemplateRepository.GetByID(templateId);
            return template.SystemTemplateVersions.OrderByDescending(x => x.VersionNumber).First();
        }
        /// <summary>
        /// Retrieves the system template version for the specified template name
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <returns>SystemTemplateVersion identified</returns>
        public static SystemTemplateVersion GetSystemTemplateVersion(string templateName)
        {
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            SystemTemplate templateToSend = sysTemplateMgr.GetSystemTemplateByName(templateName);
            return sysTemplateMgr.GetActiveVersion(templateToSend);
        }
#region Lookup Helpers

#endregion

    }
}
