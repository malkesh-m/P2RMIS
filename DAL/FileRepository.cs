using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Data.SqlTypes;
using System.Data.Common;

using System.Data.SqlClient;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
   public class FileRepository : GenericRepository<FileStore>, IFileRepository
   {
       private UnitOfWork unitOfWork = new UnitOfWork();

       public FileRepository(P2RMISNETEntities context)
            : base(context)
        {
            
        }
           public IEnumerable<FileStore> GetAllFiles()
           {
               return context.FileStores;
           }

           public List<FileStore> GetFileStores()
           {
               return context.FileStores.ToList();
           }

           public IEnumerable<FileStoreMain> GetAllFileMains()
           {
               return context.FileStoreMains;
           }
       
            public void UploadFile(FileStore file)
           {
         

                context.FileStores.Add(file); 
           }

            public FileStore GetFileById(int fileId)
           {
               return context.FileStores.Find(fileId);
           }

            public FileStoreMain GetFileStoreMainByID(int fsMainId)
           {
               return context.FileStoreMains.Find(fsMainId);
           }       

            public string GetFilepathByFileId(int id, SqlConnection connection)
            {
                SqlCommand filePathCommand = connection.CreateCommand();
                filePathCommand.CommandText = "SELECT FileStoreContent.PathName() from dbo.FileStore where FileID = '" + id + "'";
                filePathCommand.CommandType = CommandType.Text;
                string filePath = (string)filePathCommand.ExecuteScalar();

                return filePath;
            }

            public string GetFilepathByGuid(System.Guid guid, SqlConnection connection)
            {
                SqlParameter parameter = null;
                SqlCommand filePathCommand = connection.CreateCommand();
                filePathCommand.CommandText = "SELECT FileStoreContent.PathName() from dbo.FileStore where [FileStoreID] = @Id";
                parameter = new SqlParameter("@Id", guid);
                filePathCommand.Parameters.Add(parameter);
                string filePath = (string)filePathCommand.ExecuteScalar();

                return filePath;
            }

            public string GetContentTypeByFileId(int id, SqlConnection connection)
            {
                SqlCommand fileContentCommand = connection.CreateCommand();
                fileContentCommand.CommandText = "SELECT FileContentType from dbo.FileStore where FileID = '" + id + "'";
                fileContentCommand.CommandType = CommandType.Text;
                string contentType = (string)fileContentCommand.ExecuteScalar();

                return contentType;
            }

            public string GetContentTypeByGuid(System.Guid guid, SqlConnection connection)
            {
                SqlParameter parameter = null;
                SqlCommand fileContentCommand = connection.CreateCommand();
                fileContentCommand.CommandText = "SELECT FileContentType from dbo.FileStore where [FileStoreID] = @Id";
                parameter = new SqlParameter("@Id", guid);
                fileContentCommand.Parameters.Add(parameter);
                string contentType = (string)fileContentCommand.ExecuteScalar();

                return contentType;
            }

            public byte[] GetTransactionContext(SqlConnection connection)
            {
                SqlCommand transactionCommand = connection.CreateCommand();
                transactionCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                transactionCommand.CommandType = CommandType.Text;
                byte[] txContext = (byte[])transactionCommand.ExecuteScalar();

                return txContext;
            }

            public void DeleteFileStore(FileStore fs)
            {
                context.FileStores.Remove(fs);
            }

            public void DeleteFileStoreMain(FileStoreMain fsm)
            {
                context.FileStoreMains.Remove(fsm);
            }

            public DbConnection getConnectionString()
            {
                return context.Database.Connection;
            }
           
       
       public void Save()
            {
                context.SaveChanges();
            }

       }

    }
