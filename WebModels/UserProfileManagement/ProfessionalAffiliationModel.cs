

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public class ProfessionalAffiliationModel : IProfessionalAffiliationModel
    {
        #region constructors
        public ProfessionalAffiliationModel() { }

        public ProfessionalAffiliationModel(int? id, string name, string department, string position)
        {
            this.ProfessionalAffiliationId = id;
            this.Name = name;
            this.Department = department;
            this.Position = position;
        }
        #endregion

        /// <summary>
        /// Type of professional affiliation
        /// </summary>
        public int? ProfessionalAffiliationId { get; set; }
        /// <summary>
        /// institution name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }
    }
}
