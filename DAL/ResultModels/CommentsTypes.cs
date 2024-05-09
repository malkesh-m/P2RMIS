namespace Sra.P2rmis.Dal.ResultModels
{
    public class CommentTypes : ICommentTypes
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommentTypes() { }
        #endregion
        #region Properties
        /// <summary>
        /// Comment lookup ID
        /// </summary>
        public int CommentTypeId { get; set; }
        /// <summary>
        /// Comment lookup description
        /// </summary>
        public string CommentTypeDescription { get; set; }
        #endregion
    }
}