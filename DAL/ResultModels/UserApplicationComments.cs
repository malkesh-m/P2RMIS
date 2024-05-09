namespace Sra.P2rmis.Dal.ResultModels
{
    public class UserApplicationComments : IUserApplicationComments
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserApplicationComments() { }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int CommentID { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationID { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string UserPrefix { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string UserFirstName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string UserLastName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? CreatedBy { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public System.DateTime? CreatedDate { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public System.DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Comment lookup ID
        /// </summary>
        public int CommentLkpId { get; set; }
        /// <summary>
        /// Comment lookup description
        /// </summary>
        public string CommentLkpDescription { get; set; }
        #endregion
    }
}