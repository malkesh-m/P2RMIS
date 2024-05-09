using System;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.EntityChanges;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserResume object. 
    /// </summary>
    public partial class UserResume : IStandardDateFields, ILogEntityChanges
    {
        #region Static Attributes
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            {nameof(FileName), new PropertyChange(typeof(string), UserInfoChangeType.Indexes.CV) } 
        };
        #endregion
        public class Constants
        {
            /// <summary>
            /// Starting version number of resumes
            /// </summary>
            public static int ResumeStartingVersion { get { return 1; } }
            /// <summary>
            /// Resume extension for P2RMIS (only one excepted)
            /// </summary>
            public static string ResumeExtension { get { return "pdf"; } }
        }
        /// <summary>
        /// Populates a new UserResume in preparation for addition to the repository.
        /// </summary>
        /// <param name="fileName">Resume File name</param>
        /// <param name="version">Version number</param>
        /// <param name="fileByteArray">File in byte array</param>
        /// <param name="now">Received date/time</param>
        public void Populate(string fileName, int version, byte[] fileByteArray, DateTime now)
        {
            Populate(fileName, version, fileByteArray);
            this.ReceivedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Populates a new UserResume in preparation for addition to the repository.
        /// </summary>
        /// <param name="fileName">Resume File name</param>
        /// <param name="version">Version number</param>
        /// <param name="fileByteArray">File in byte array</param>
        public void Populate(string fileName, int version, byte[] fileByteArray)
        {
            this.FileName = fileName;
            this.Version = version;
            this.DocType = UserResume.Constants.ResumeExtension;
            this.ResumeFile = fileByteArray;
        }
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static UserResume _default;
        public static UserResume Default
        {
            get 
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new UserResume();
                }
                return _default; 
            }
        }
        /// <summary>
        /// Increments the version number
        /// </summary>
        public int NextVersion { get { return this.Version + 1; } }
        /// <summary>
        /// Retrieves the collection of properties names & there attributes that 
        /// should have changes logged.
        /// </summary>
        /// <returns>Collection of properties</returns>
        public Dictionary<string, PropertyChange> PropertiesToLog()
        {
            return UserResume.ChangeLogRequired;
        }
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        public string KeyPropertyName
        {
            get { return nameof(UserResumeId); }
        }

    }
}
