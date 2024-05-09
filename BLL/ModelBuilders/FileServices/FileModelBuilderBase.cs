using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using System;
using System.IO;

namespace Sra.P2rmis.Bll.ModelBuilders.FileServices
{
    /// <summary>
    /// Base class for populating any type of FileModel
    /// </summary>
    internal class FileModelBuilderBase: ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="programEmailTemplateId">ProgramEmailTemplate entity identifier</param>
        public FileModelBuilderBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion
        #region Attributes
        /// <summary>
        /// Model to return
        /// </summary>
        protected IBuiltModel Model { get; set; }
        /// <summary>
        /// The file name from the URL.
        /// </summary>
        protected string FileName { get; set; }
        /// <summary>
        /// The file contents.
        /// </summary>
        protected byte[] Buffer { get; set; }
        /// <summary>
        /// File location
        /// </summary>
        protected string Location { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Construct the model to return
        /// </summary>
        /// <remarks>
        ///  Derived classes should override this method if a different model is used.
        /// </remarks>
        public virtual void ConstructModel()
        {
            //
            // Derived classes should override this method to construct the appropriate model.
            //
            throw new NotSupportedException("Derived class should override ConstructModel.  Please Contact a developer");
        }
        /// <summary>
        /// Populate the model to return
        /// </summary>
        public virtual void PopulateModel()
        {
            //
            // Derived classes should override this method to populate the appropriate model.
            //
            throw new NotSupportedException("Derived class should override PopulateModel.  Please Contact a developer");
        }
        /// <summary>
        /// Populate the model to return
        /// </summary>
        public virtual void RetrieveLLocation()
        {
            //
            // Derived classes should override this method to retrieve the file location.
            //
            throw new NotSupportedException("Derived class should override RetrieveLLocation.  Please Contact a developer");
        }
        /// <summary>
        /// Parses the file name from the directory location.
        /// </summary>
        public virtual void ParseFileNameFromLocation()
        {
            this.FileName = Path.GetFileName(this.Location);
        }
        /// <summary>
        /// Build the model which retrieves the file
        /// </summary>
        /// <returns>LegacyFileModel built</returns>
        public override IBuiltModel Build()
        {
            ConstructModel();
            RetrieveLLocation();
            //
            // If we don't have a Location then there is no reason to do try and retrieve a file
            //
            if (!string.IsNullOrWhiteSpace(Location))
            {
                ParseFileNameFromLocation();
                PopulateModel();
            }
            return Model;
        }
        #endregion
    }
}
