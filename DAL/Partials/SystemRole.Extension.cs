
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's System Role object. Contains lookup values within SystemRole lookup table.
    /// </summary>
    public partial class SystemRole
    {
         /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            /// <summary>
            /// Identifier of the Client Role
            /// </summary>
            public const int Client = 4;
            /// <summary>
            /// Identifier of the Staff Role
            /// </summary>
            public const int Staff = 8;
            /// <summary>
            /// Identifier of the Webmaster Role
            /// </summary>
            public const int Webmaster = 10;
            /// <summary>
            /// Identifier of the SRO Role
            /// </summary>
            public const int SRO = 11;
            /// <summary>
            /// Identifier of the Reviewer Role
            /// </summary>
            public const int Reviewer = 12;
            /// <summary>
            /// Identifier of the Editor Role
            /// </summary>
            public const int Editor = 20;
            /// <summary>
            /// Identifier of the RTA Role
            /// </summary>
            public const int RTA = 22;
            /// <summary>
            /// Identifier of the Managing Editor Role
            /// </summary>
            public const int EditingManager = 23;
            /// <summary>
            /// Identifier of the SRM Role
            /// </summary>
            public const int SRM = 24;
            /// <summary>
            /// The cprit chair
            /// </summary>
            public const int CpritChair = 30;
        }
        //
        // Role to use when there is none
        //
        public const string NoRole = "N/A";

        public class RoleContext
        {
            public const string System = "system";
        }

        public class RoleName
        {
            /// <summary>
            /// Name of Client role
            /// </summary>
            public const string Client = "Client";
            /// <summary>
            /// Name of EditingManager role
            /// </summary>
            public const string EditingManager = "Editing Manager";
           /// <summary>
            /// Name of Editor role
            /// </summary>
            public const string Editor = "Editor";
           /// <summary>
            /// Name of Reviewer role
            /// </summary>
            public const string Reviewer = "Reviewer";
           /// <summary>
            /// Name of RTA role
            /// </summary>
            public const string RTA = "RTA";
           /// <summary>
            /// Name of SRM role
            /// </summary>
            public const string SRM = "SRM";
           /// <summary>
            /// Name of SRO role
            /// </summary>
            public const string SRO = "SRO";
           /// <summary>
            /// Name of Staff role
            /// </summary>
            public const string Staff = "Staff";
           /// <summary>
            /// Name of Webmaster role
            /// </summary>
            public const string Webmaster = "Webmaster";
            /// <summary>
            /// The cprit chair
            /// </summary>
            public const string CpritChair = "CpritChair";
        }

        public static string GetRoleName(int id)
        {
            string name = string.Empty;

            switch (id)
            {
                /// <summary>
                /// Client Role
                /// </summary>
                case Indexes.Client:
                    name = RoleName.Client;
                    break;
                /// <summary>
                /// Staff Role
                /// </summary>
                case Indexes.Staff:
                    name = RoleName.Staff;
                    break;
                /// <summary>
                /// IWebmaster Role
                /// </summary>
                case Indexes.Webmaster:
                    name = RoleName.Webmaster;
                    break;
                /// <summary>
                /// RO Role
                /// </summary>
                case Indexes.SRO:
                    name = RoleName.SRO;
                    break;
                /// <summary>
                /// Reviewer Role
                /// </summary>
                case Indexes.Reviewer:
                    name = RoleName.Reviewer;
                    break;
                /// <summary>
                /// Editor Role
                /// </summary>
                case Indexes.Editor:
                    name = RoleName.Editor;
                    break;
                /// <summary>
                /// IRTA Role
                /// </summary>
                case Indexes.RTA:
                    name = RoleName.RTA;
                    break;
                /// <summary>
                /// Editor Role
                /// </summary>
                case Indexes.EditingManager:
                    name = RoleName.EditingManager;
                    break;
                /// <summary>
                /// SRM Role
                /// </summary>
                case Indexes.SRM:
                    name = RoleName.SRM;
                    break;
                case Indexes.CpritChair:
                    name = RoleName.CpritChair;
                    break;
                default:
                    name = NoRole;
                    break;  
            }
            return name;
        }
        /// <summary>
        /// Checks if the SystemRole is an SRO
        /// </summary>
        /// <returns>True if the role is SRO; false otherwise</returns>
        public bool IsSro()
        {
            return (this.SystemRoleId == Indexes.SRO);
        }
        /// <summary>
        /// Checks if the SystemRole is an RTA
        /// </summary>
        /// <returns>True if the role is RTA; false otherwise</returns>
        public bool IsRta()
        {
            return (this.SystemRoleId == Indexes.RTA);
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is cprit chair.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cprit chair; otherwise, <c>false</c>.
        /// </value>
        public bool IsCpritChair()
        {
            return (this.SystemRoleId == Indexes.CpritChair);
        }
    }
}
