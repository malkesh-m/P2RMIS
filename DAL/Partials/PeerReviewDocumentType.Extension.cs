using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    public partial class PeerReviewDocumentType
    {
        #region Constants/Lookup Values
        public class Lookups
        {
            /// <summary>
            /// The training document type
            /// </summary>
            public const int Training = 1;
            /// <summary>
            /// The meeting information
            /// </summary>
            public const int MeetingInformation = 2;
            /// <summary>
            /// The email template
            /// </summary>
            public const int EmailTemplate = 3;
        }
        #endregion
    }
}
