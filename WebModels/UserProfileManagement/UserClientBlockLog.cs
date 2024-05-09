using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public interface IUserClientBlockLog
    {
        /// <summary>
        /// Gets or sets the block identifier.
        /// </summary>
        /// <value>
        /// The block identifier.
        /// </value>
        int BlockId { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        string Comments { get; set; }
        /// <summary>
        /// Gets or sets the name of the entered by.
        /// </summary>
        /// <value>
        /// The name of the entered by.
        /// </value>
        string EnteredBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the client block flag.
        /// </summary>
        /// <value>
        /// The client block flag.
        /// </value>
        List<KeyValuePair<string, bool>> ClientBlockFlags { get; set; }
    }

    public class UserClientBlockLog : IUserClientBlockLog
    {
        public UserClientBlockLog()
        {
            ClientBlockFlags = new List<KeyValuePair<string, bool>>();
        }
        /// <summary>
        /// Gets or sets the block identifier.
        /// </summary>
        /// <value>
        /// The block identifier.
        /// </value>
        public int BlockId { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the name of the entered by.
        /// </summary>
        /// <value>
        /// The name of the entered by.
        /// </value>
        public string EnteredBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the client block flag.
        /// </summary>
        /// <value>
        /// The client block flag.
        /// </value>
        public List<KeyValuePair<string, bool>> ClientBlockFlags { get; set; }
    }
}
