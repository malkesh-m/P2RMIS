using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Files;
using System;
using System.Linq;
using System.Net;

namespace Sra.P2rmis.Bll.ModelBuilders.FileServices
{
    /// <summary>
    /// Construct a LegacyFileModel (which contains a file retrieved from
    /// the PRMIS Legacy system)
    /// </summary>
    internal class LegacyFileModelBuilder: FileModelBuilderBase//ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="programEmailTemplateId">ProgramEmailTemplate entity identifier</param>
        public LegacyFileModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion
        #region Attributes
        /// <summary>
        /// The ProgramEmailTemplate entity identifier
        /// </summary>
        private int ProgramEmailTemplateId { get; set; }
        /// <summary>
        /// The URL of the file resource to retrieve;
        /// </summary>
        protected string Url { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Build the model which retrieves the file
        /// </summary>
        /// <returns>LegacyFileModel built</returns>
        public override IBuiltModel Build()
        {
            ConstructModel();
            RetrieveLegacyUrl();
            //
            // If we don't have a URL then there is no reason to do try and retrieve a file
            //
            if (!string.IsNullOrWhiteSpace(Url))
            {
                //
                //  Create a WebClient then download the file.  While we are at it parse the
                //  file name from the URL & return it in case it needs to be used when returning the 
                //  file to the client.  Finally populate the model.
                //
                WebClient webClient = new WebClient();
                Buffer = webClient.DownloadData(Url);
                ParseFileNameFromLegacyEmailTemplateUri();
                PopulateModel();
            }
            return Model;
        }
        /// <summary>
        /// Retrieve the URL for the file resource.
        /// </summary>
        protected virtual void RetrieveLegacyUrl()
        {
            //
            // Derived classes should override this method to retrieve the URL.  One assumes it is
            // set from a database entity.
            //
        }
        /// <summary>
        /// Retrieve the file name from a legacy email template.  We know (or expect) the file name to be
        /// the last segment in the Uri (segment count - 1).
        /// </summary>
        /// <param name="legacyUri">Legacy ProgramEmailTemplate Uri</param>
        public virtual void ParseFileNameFromLegacyEmailTemplateUri()
        {
            Uri uri = new Uri(Url);
            int numberSegments = uri.Segments.Count();

            FileName = uri.Segments[numberSegments - 1];
        }
        /// <summary>
        /// Construct the model to return
        /// </summary>
        /// <remarks\>
        ///  Derived classes should override this method if a different model is used.
        /// </remarks>
        public override void ConstructModel()
        {
            Model = new LegacyFileModel();
        }
        /// <summary>
        /// Populate the model to return
        /// </summary>
        /// <remarks>
        /// Derived classes should override this method if necessary.
        /// </remarks>
        public override void PopulateModel()
        {
            LegacyFileModel model = Model as LegacyFileModel;
            model.Populate(Buffer, FileName);
        }
        #endregion
    }

    /// <summary>
    /// Retrieve an EmailTemplate file.
    /// </summary>
    internal class EmailTemplateFileModelBuilder: LegacyFileModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="programEmailTemplateId">ProgramEmailTemplate entity identifier</param>
        public EmailTemplateFileModelBuilder(IUnitOfWork unitOfWork, int programEmailTemplateId)
            : base(unitOfWork)
        {
            this.ProgramEmailTemplateId = programEmailTemplateId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The ProgramEmailTemplate entity identifier
        /// </summary>
        private int ProgramEmailTemplateId { get; set; }
        #endregion
        #region Overrides
        /// <summary>
        /// Retrieve the URL for the file resource.
        /// </summary>
        protected override void RetrieveLegacyUrl()
        {
            ProgramEmailTemplate programEmailTemplateEntity = UnitOfWork.ProgramEmailTemplateRepository.GetByID(ProgramEmailTemplateId);
            //
            // Build the URL from the P2RMIS 1 server & the template location from the entity.  The managed hosting servers are configured 
            // in such a manner that they cannot access any other server outside of their machine.
            //
            UriBuilder builder = new UriBuilder(ConfigManager.P2RMISv1Server);
            builder.Path = programEmailTemplateEntity.FileLocation;

            this.Url = builder.Uri.ToString();
        }
        #endregion
    }
}
