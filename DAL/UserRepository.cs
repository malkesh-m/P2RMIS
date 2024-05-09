using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sra.P2rmis.Dal.Common;

namespace Sra.P2rmis.Dal
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(P2RMISNETEntities context)
            : base(context)
        {
            
        }

        public List<User> GetUserAutocomplete()
        {
            return context.Users.ToList();
        }
        
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public IEnumerable<State> GetUSStates()
        {
            return context.States.OrderBy(z => z.StateName);
        }
        public IEnumerable<Client> GetClientDesc()
        {
            return context.Clients;
        }


        //public IEnumerable<LookupCountry> GetCountryNames()
        //{
        //    return context.LookupCountries.OrderBy(z => z.CountryName);
        //}
        
        public IEnumerable<Prefix> GetPrefixes()
        {
            return context.Prefixes;
        }


        //public IEnumerable<LookupRole> GetRolesRestrictedByUserRole(LookupRole userLookupRole)
        //{
        //    return context.LookupRoles.Where(r => r.RoleContext == Constants.LOOKUP_ROLE_CONTEXT_SYSTEM && r.PriorityOrder >= userLookupRole.PriorityOrder).OrderBy(r => r.RoleName); 
        //}

        public IEnumerable<SystemRole> GetRoles()
        {
            return context.SystemRoles.Where(r => r.SystemRoleContext == Constants.LOOKUP_ROLE_CONTEXT_SYSTEM).OrderBy(r => r.SystemRoleName);
        }

        public IEnumerable<SystemRole> GetRolesInit()
        {
            return context.SystemRoles.Where(r => r.SystemRoleContext == Constants.LOOKUP_ROLE_CONTEXT_SYSTEM && r.SystemRoleName != Constants.LOOKUP_ROLE_SYSTEM_WEBMASTER);
        }

        public List<string> GetUserEmails(int id)
        {
            return context.UserEmails.Where(u => u.UserInfo.UserID == id)
                .Select(u => u.Email)
                .ToList();
        }

        public User GetUserByID(int userId)
        {
            return context.Users.Find(userId);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(int userId)
        {
            User user = context.Users.Find(userId);
            context.Users.Remove(user);
        }
        
        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void ClearSystemRoles(int id)
        {
           User tmpUsr= GetUserByID(id);
           //List<UserSystemRole> allSysRoles = context.UserSystemRoles.Where(a => a.UserID == id) as List<UserSystemRole> ;
           UserSystemRole[] allSysRoles = new UserSystemRole[tmpUsr.UserSystemRoles.Count];
           tmpUsr.UserSystemRoles.CopyTo(allSysRoles, 0);
           
           foreach(UserSystemRole sysRole in allSysRoles){
               context.UserSystemRoles.Remove(sysRole);
           }
           
           context.SaveChanges(); 

        }

       
        public IEnumerable<Client> GetClients()
        {
            return context.Clients;
        }

        /// <summary>
        /// Gets the user instance by identifier with panel assignment info.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User entity with assignment information eager loaded</returns>
        public User GetByIDWithAssignments(int userId)
        {
            var result = GetEager(x => x.UserID == userId, null,
                x => x.PanelUserAssignments.Select(y => y.SessionPanel.ProgramPanels.Select(y2 => y2.ProgramYear.ClientProgram)),
                x => x.PanelUserAssignments.Select(y => y.ClientParticipantType),
                x => x.PanelUserAssignments.Select(y => y.SessionPanel.MeetingSession.ClientMeeting.MeetingType));
            return result.FirstOrDefault();
        }
    }
}