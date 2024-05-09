using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class PeerReviewDocument : IStandardDateFields
    {
        public class Indexes
        {
            /// <summary>
            /// The file or image
            /// </summary>
            public const int FileOrImage = 1;
            /// <summary>
            /// The document link
            /// </summary>
            public const int DocumentLink = 2;
            /// <summary>
            /// The video link
            /// </summary>
            public const int VideoLink = 3;
        }
    }
}
