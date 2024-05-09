using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.Library;
namespace Sra.P2rmis.Dal.Repository.LibraryManagement
{
    public class LibraryManagementRepository : ILibraryManagementRepository
    {
        public IEnumerable<ITrainingDocumentModel> GetTrainingDocumentsForUser(int programYearId, int userId, bool canViewAllDocuments)
        {
            //first get the user's assignments for the supplied program year
            return null;
        }
    }
}
