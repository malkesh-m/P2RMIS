using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<FileStore> GetAllFiles();
        FileStore GetFileById(int id);
        void UploadFile(FileStore fileinfo);
        void Save();
    }
}