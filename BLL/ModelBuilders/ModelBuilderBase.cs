using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using System.IO;

namespace Sra.P2rmis.Bll.ModelBuilders
{
    /// <summary>
    /// Base Service layer model builder.  Collection of common functionality.
    /// </summary>
    internal class ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="userId">User entity identifier</param>
        public ModelBuilderBase(IUnitOfWork unitOfWork, int userId)
        {
            this.UnitOfWork = unitOfWork;
            this.UserId = userId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        public ModelBuilderBase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Unit of work object
        /// </summary>
        internal IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// User entity identifier
        /// </summary>
        internal int UserId { get; private set; }
        #endregion
        #region Repository wrappers (because I got tired of writing UnitOfWork.....
        protected ProgramYear GetThisProgramYearEntity(int id) { return this.UnitOfWork.ProgramYearRepository.GetByID(id); }
        protected User GetThisUser(int id) { return this.UnitOfWork.UserRepository.GetByID(id); }
        protected Client GetThisClient(int id) { return this.UnitOfWork.ClientRepository.GetByID(id); }
        protected UserInfo GetThisUserInfoEntity(int id) {
            return this.UnitOfWork.UserInfoRepository.GetByID(id);
        }
        protected SessionPanel GetThisSessionPanel(int id) { return this.UnitOfWork.SessionPanelRepository.GetByIDWithPanelApplicationStatus(id); }
        protected AcademicRank GetThisAcademicRank(int? academicRankId)  { return (academicRankId.HasValue) ? UnitOfWork.AcademicRankRepository.GetByID(academicRankId) : null; }
        protected ClientProgram GetThisClientProgram(int id) { return this.UnitOfWork.ClientProgramRepository.GetByID(id); }
        #endregion
        #region Model builder services
        /// <summary>
        /// Build the model.  Default implementation.
        /// </summary>
        /// <returns>Null</returns>
        public virtual IBuiltModel Build()
        {
            return null;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Helper method to get parse the file extension from a path string. 
        /// </summary>
        /// <param name="path">Path string</param>
        /// <returns>File extension if path is not null or empty; empty string otherwise</returns>
        public static string GetFileExtension(string path)
        {
            return (string.IsNullOrEmpty(path))? string.Empty: Path.GetExtension(path); 
        }
        #endregion
    }
 }
