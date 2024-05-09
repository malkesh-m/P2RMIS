using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IApplicationPersonnelRepository : IGenericRepository<ApplicationPersonnel>
    {
        ApplicationPersonnel Add(int applicationId, int clientApplicationPersonnelTypeId,
                  string firstName, string lastName, string middleInitial, string organization, string state, string email, string phone, bool primaryFlag, string source, int userId);

        void DeleteCoiList(int applicationId, int userId);

        ApplicationPersonnel GetByApplicationPersonnelType(int applicationId, string applicationPersonnelType);

        void Update(ApplicationPersonnel applicationPersonnel, string firstName, string lastName, string middleInitial, string organization, string state, string email, string phone, bool primaryFlag, int userId);

        void UpdateName(ApplicationPersonnel applicationPersonnel, string firstName, string lastName, string middleInitial, int userId);
    }

    public class ApplicationPersonnelRepository : GenericRepository<ApplicationPersonnel>, IApplicationPersonnelRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationPersonnelRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ApplicationPersonnel Add(int applicationId, int clientApplicationPersonnelTypeId,
                  string firstName, string lastName, string middleInitial, string organization, string state, string email, string phone, bool primaryFlag, string source, int userId) {
            var o = new ApplicationPersonnel();
            o.ApplicationId = applicationId;
            o.ClientApplicationPersonnelTypeId = clientApplicationPersonnelTypeId;
            o.FirstName = firstName;
            o.LastName = lastName;
            o.MiddleInitial = middleInitial;
            o.OrganizationName = organization;
            o.StateAbbreviation = state;
            o.EmailAddress = email;
            o.PhoneNumber = phone;
            o.PrimaryFlag = primaryFlag;
            o.Source = source;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            Add(o);

            return o;
        }

        public void DeleteCoiList(int applicationId, int userId)
        {
            var os = Get(x => x.ApplicationId == applicationId && x.ClientApplicationPersonnelType.CoiFlag);
            foreach (var o in os)
            {
                Helper.UpdateDeletedFields(o, userId);
                Delete(o);
            }
        }

        public ApplicationPersonnel GetByApplicationPersonnelType(int applicationId, string applicationPersonnelType)
        {
            var o = Get(x => x.ApplicationId == applicationId && x.ClientApplicationPersonnelType.ApplicationPersonnelType == applicationPersonnelType).FirstOrDefault();
            return o;
        }

        public void Update(ApplicationPersonnel applicationPersonnel, string firstName, string lastName, string middleInitial, string organization, string state, string email, string phone, bool primaryFlag, int userId)
        {
            if (applicationPersonnel.FirstName != firstName || applicationPersonnel.LastName != lastName || applicationPersonnel.MiddleInitial != middleInitial ||
                   applicationPersonnel.OrganizationName != organization || applicationPersonnel.StateAbbreviation != state || applicationPersonnel.EmailAddress != email ||
                   applicationPersonnel.EmailAddress != email || applicationPersonnel.PhoneNumber != phone)
            {
                applicationPersonnel.FirstName = firstName;
                applicationPersonnel.LastName = lastName;
                applicationPersonnel.MiddleInitial = middleInitial;
                applicationPersonnel.OrganizationName = organization;
                applicationPersonnel.StateAbbreviation = state;
                applicationPersonnel.EmailAddress = email;
                applicationPersonnel.PhoneNumber = phone;
                applicationPersonnel.PrimaryFlag = primaryFlag;
                Helper.UpdateModifiedFields(applicationPersonnel, userId);
            }
        }

        public void UpdateName(ApplicationPersonnel applicationPersonnel, string firstName, string lastName, string middleInitial, int userId)
        {
            if (applicationPersonnel.FirstName != firstName || applicationPersonnel.LastName != lastName || applicationPersonnel.MiddleInitial != middleInitial)
            {
                applicationPersonnel.FirstName = firstName;
                applicationPersonnel.LastName = lastName;
                applicationPersonnel.MiddleInitial = middleInitial;
                Helper.UpdateModifiedFields(applicationPersonnel, userId);
            }
        }
    }
}
