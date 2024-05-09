
namespace Sra.P2rmis.Dal
{
	/// <summary>
    /// Custom methods for Entity Framework's CommentType
    /// </summary>	
    public partial class CommentType
    {
        /// <summary>
        /// Specific index values.
        /// </summary> 
        public class Indexes
        {
            /// <summary>
            /// Discussion note
            /// </summary>
            public static int DiscussionNote = 1;
            /// <summary>
            /// Admin note
            /// </summary>
            public static int AdminNote = 2;
            /// <summary>
            /// Admin note
            /// </summary>
            public static int GeneralNote = 3;
            /// <summary>
            /// Summary note
            /// </summary>
            public static int SummaryNote = 4;
            /// <summary>
            /// Reviewer note
            /// </summary>
            public static int ReviewerNote = 5;
        }
    }
}
