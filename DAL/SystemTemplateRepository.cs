using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    public class SystemTemplateRepository : GenericRepository<SystemTemplate>
    {

        public SystemTemplateRepository(P2RMISNETEntities context)
            : base(context)
        {
            
        }

        public IEnumerable<SystemTemplate> GetSystemTemplatesPaged(int page, int rows, out int totalCount)
        {
            var tst = context.SystemTemplates;
            totalCount = tst.Count();
            
            return tst.OrderByDescending(t=>t.TemplateId).Skip(page * rows).Take(rows);
        }

        public List<SystemTemplate> GetSystemTemplates()
        {
            return context.SystemTemplates.ToList();
        }

        public List<SystemTemplate> GetSystemTemplatesByName(string name)
        {
            return context.SystemTemplates.Where(a=>a.Name == name).ToList();
        }

        public SystemTemplate GetSystemTemplateByID(int id)
        {
            return context.SystemTemplates.Find(id);
        }

        public SystemTemplateVersion GetSystemTemplateVersionByID(int id)
        {
            return context.SystemTemplateVersions.Find(id);
        }

        public void InsertSystemTemplate(SystemTemplate sysTemplate)
        {
            context.SystemTemplates.Add(sysTemplate);
        }

        public void UpdateSystemTemplate(SystemTemplate sysTemplate)
        {
            context.Entry(sysTemplate).State = EntityState.Modified;
        }

        public void AddSystemTemplateVersion(SystemTemplateVersion ver)
        {
            context.SystemTemplateVersions.Add(ver);
        }

        public void AddSystemTemplate(SystemTemplate sysTemplate)
        {
            context.SystemTemplates.Add(sysTemplate);
        }

        public void DeleteTemplate(SystemTemplate sysTemplate)
        {
            context.SystemTemplates.Remove(sysTemplate);
        }

        public void DeleteTemplateVersion(SystemTemplateVersion ver)
        {
            context.SystemTemplateVersions.Remove(ver);
        }

        public IEnumerable<LookupTemplateCategory> GetTemplateCategories()
        {
            return context.LookupTemplateCategories;
        }

        public IEnumerable<LookupTemplateType> GetTemplateTypes()
        {
            return context.LookupTemplateTypes;
        }
        public IEnumerable<LookupTemplateStage> GetTemplateStages()
        {
            return context.LookupTemplateStages;
        }

        public void Save()
        {
            context.SaveChanges();
        }


       
    }
}
