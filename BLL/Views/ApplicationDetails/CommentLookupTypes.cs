using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public class CommentLookupTypes : ICommentLookupTypes
    {
        #region Constructor

        internal CommentLookupTypes(ICommentTypes commentTypes)
        {
            this.CommentTypeId = commentTypes.CommentTypeId;
            this.CommentTypeDescription = commentTypes.CommentTypeDescription;
        }

        #endregion

        #region Properties

        public int CommentTypeId { get; set; }
        public string CommentTypeDescription { get; set; }

        #endregion
    }
}
