using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// comment type for online discussion, as starting a new online discussion or adding a comment
    /// </summary>
    public class CommentTypeModel
    {
        public int CommentId { get; set; }
        public bool CommentType { get; set; }
    }
}
