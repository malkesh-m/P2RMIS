using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sra.P2rmis.Dal;
using System.Data.Entity;
using System.Data;

using System.Web;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.SqlTypes;

//Source from - http://asadyousufi.blogspot.com/2010/11/filestream-storage-in-sql-2008-with_26.html

namespace Sra.P2rmis.Bll
{
    public class ManageFiles : IDisposable
    {
        private UnitOfWork unitOfWork;

        public ManageFiles()
        {
            unitOfWork = new UnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        //Gets the File based on its Name
        public int GetFileStoreByName(String filename)
        {
            List<FileStore> filelList = unitOfWork.UofwFileRepository.GetFileStores();

            int tempId = filelList.Single(a => a.FileStoreName == filename).FileID;

            //return filelList.Find(a => a.FileID == tempId);
            return tempId;
        }

        public IEnumerable<FileStore> GetFilesList()
        {
           return unitOfWork.UofwFileRepository.GetAllFiles();
        }

        public IEnumerable<FileStoreMain> GetFileMainsList()
        {
            return unitOfWork.UofwFileRepository.GetAllFileMains();
        }


        /// <summary>
        /// DownloadFileStream - retrieves a file from the dbo.FileStore table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Response"></param>
        public void DownloadFileStream(int id, HttpResponseBase Response)
        {

            SqlConnection connection = null;
            

            using (TransactionScope ts = new TransactionScope())
            {
                FileStore selectedFile =  unitOfWork.UofwFileRepository.GetFileById(id);

                connection = new SqlConnection(this.getConnectionString());
                connection.Open();

                //Get the Filepath
               string pathName = unitOfWork.UofwFileRepository.GetFilepathByFileId(id, connection);
                
                //Get the Content type
               string contentType = unitOfWork.UofwFileRepository.GetContentTypeByFileId(id, connection);

                //Get the transaction context
               byte[] txContext = unitOfWork.UofwFileRepository.GetTransactionContext(connection);               


                SqlFileStream sqlFile = new SqlFileStream(pathName, txContext, System.IO.FileAccess.Read);


                if (selectedFile != null)
                {

                    // Buffer to read 10K bytes in chunk:
                    byte[] buffer = new Byte[1024 * 512]; //512 KB

                    // Length of the file:
                    int length;

                    // Total bytes to read:
                    long dataToRead;

                    try
                    {
                        // Total bytes to read:
                        dataToRead = sqlFile.Length;

                        Response.ContentType = contentType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + selectedFile.FileStoreName);

                        // Read the bytes.
                        while (dataToRead > 0)
                        {
                            // Verify that the client is connected.
                            if (Response.IsClientConnected)
                            {
                                // Read the data in buffer.
                                length = sqlFile.Read(buffer, 0, 1024 * 512);

                                // Write the data to the current output stream.
                                Response.OutputStream.Write(buffer, 0, length);

                                // Flush the data to the HTML output.
                                Response.Flush();

                                buffer = new Byte[1024 * 512];
                                dataToRead = dataToRead - length;
                            }
                            else
                            {
                                //prevent infinite loop if user disconnects
                                dataToRead = -1;
                            }
                        }
                        ts.Complete();
                    }
                    catch (Exception ex)
                    {
                        // Trap the error, if any.
                        Response.Write("Error : " + ex.Message);
                        ts.Dispose();
                    }
                    finally
                    {
                        if (sqlFile != null)
                        {
                            //Close the file.
                            sqlFile.Close();
                            Response.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// UploadStream - inserts each File in the sent Request into FileStore table using direct Transact-SQL commands.
        /// </summary>
        /// <param name="Request"></param>
        /// <returns>Primary Id of the inserted filestream in the FileStore -  If failure happens return value will be -1</returns>
        public int UploadStream(HttpRequestBase Request, int ident)
        {
            int retVal = -1;

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    SqlFileStream sqlFile = null;
                    SqlConnection connection = null;
                    SqlParameter parameter = null;
                    FileStore objFile = null;
                    FileStoreMain objFileMain = null; 

                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            objFileMain = new FileStoreMain();
                            objFileMain.rowguid = Guid.NewGuid();
                            objFileMain.ActiveVersion = 1;

                            connection = new SqlConnection(this.getConnectionString());
                            connection.Open();

                            SqlCommand insertCommandMain = connection.CreateCommand();
                            insertCommandMain.CommandText = "INSERT INTO dbo.FileStoreMain ([rowguid], [ActiveVersion]) VALUES (@Id, @ActiveVersion)";
                            insertCommandMain.CommandType = CommandType.Text;

                            parameter = new SqlParameter("@Id", objFileMain.rowguid);
                            insertCommandMain.Parameters.Add(parameter);
                            parameter = new SqlParameter("@ActiveVersion", objFileMain.ActiveVersion);
                            insertCommandMain.Parameters.Add(parameter);
                            insertCommandMain.ExecuteScalar();

                            SqlCommand fileStoreMainIdCommand = connection.CreateCommand();
                            fileStoreMainIdCommand.CommandText = "SELECT FileStoreMainId FROM dbo.FileStoreMain where [rowguid] = @Id";
                            parameter = new SqlParameter("@Id", objFileMain.rowguid);
                            fileStoreMainIdCommand.Parameters.Add(parameter);
                            int fileStoreMainId = (int)fileStoreMainIdCommand.ExecuteScalar();

                            objFile = new FileStore();
                            objFile.FileStoreName = file.FileName.Split('\\')[file.FileName.Split('\\').Length - 1]; ;
                            objFile.FileContentType = file.ContentType;
                            objFile.FileStoreSize = Convert.ToInt64(file.ContentLength);
                            objFile.FileStoreID = Guid.NewGuid();

                            SqlCommand insertCommand = connection.CreateCommand();
                            insertCommand.CommandText = "INSERT INTO dbo.FileStore ([FileStoreID], [FileStoreName], [FileStoreSize], [FileContentType], [FileStoreContent], [VersionNumber], [FileStoreMainId], [CreatedDate], [CreatedBy]) VALUES (@Id, @Name, @len, @type, (0x), @VersionNumber, @FileStoreMainId, @CreatedDate, @CreatedBy)";
                            insertCommand.CommandType = CommandType.Text;

                            parameter = new SqlParameter("@Id", objFile.FileStoreID);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@Name", objFile.FileStoreName);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@type", objFile.FileContentType);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@len", objFile.FileStoreSize);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@VersionNumber", objFileMain.ActiveVersion);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@FileStoreMainId", fileStoreMainId);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@CreatedDate", DateTime.Now);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@CreatedBy", ident);
                            insertCommand.Parameters.Add(parameter);
                            insertCommand.ExecuteScalar();

                            //Get pathName 
                            string computedPathName = unitOfWork.UofwFileRepository.GetFilepathByGuid(objFile.FileStoreID, connection);
                            
                            //Get the Content type
                            string contentType = unitOfWork.UofwFileRepository.GetContentTypeByGuid(objFile.FileStoreID, connection);
                            
                            //Get Transaction context
                            byte[] transactionContext = unitOfWork.UofwFileRepository.GetTransactionContext(connection);      

                            //Save file on FILESTREAM                         
                            sqlFile = new SqlFileStream(computedPathName, transactionContext, System.IO.FileAccess.Write);
                            byte[] buffer = new byte[1024 * 1024]; // 512Kb
                            int bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                            while (bytesRead > 0)
                            {
                                sqlFile.Write(buffer, 0, bytesRead);
                                bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                            }

                            ts.Complete();
                            SqlCommand fileIdCommand = connection.CreateCommand();
                            fileIdCommand.CommandText = "SELECT FileId FROM dbo.FileStore where [FileStoreID] = @Id";
                            parameter = new SqlParameter("@Id", objFile.FileStoreID);
                            fileIdCommand.Parameters.Add(parameter);
                            retVal = (int)fileIdCommand.ExecuteScalar();

                            return retVal;
                        }
                        catch (Exception)
                        {
                            ts.Dispose();
                            connection.Close();
                            return retVal;
                        }
                        finally
                        {
                            if (sqlFile != null)
                                sqlFile.Close();
                            if (connection != null)
                                connection.Close();
                        }
                    }
                }
            }// For Each Loop
            return retVal; // If this LOC is reached that means there is no valid file to upload.
        }//Method uploadStream()


         /// <summary>
        /// UpdateFileStream - Updates an already existing file in the FileStore table based on the id.
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="id">Key of the file to update - will not create a new version</param>
        /// <returns>True on success and False on failure</returns>
        public bool UpdateFileStream(HttpRequestBase Request, int id, int ident)
        {
            SqlConnection connection = null;
            SqlFileStream sqlFile = null;
            HttpPostedFileBase file = Request.Files[0];
            SqlParameter parameter = null;

            if (file.ContentLength > 0)
            {
           
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        FileStore selectedFile = unitOfWork.UofwFileRepository.GetFileById(id);

                        selectedFile.FileStoreSize = Convert.ToInt64(file.ContentLength);
                        selectedFile.FileContentType = file.ContentType;
                        unitOfWork.UofwFileRepository.Save();
                        connection = new SqlConnection(this.getConnectionString());
                        connection.Open();

                        SqlCommand updateCommand = connection.CreateCommand();
                        updateCommand.CommandText = "UPDATE dbo.FileStore SET ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE FileId = @FileId";
                        updateCommand.CommandType = CommandType.Text;

                        parameter = new SqlParameter("@FileId", selectedFile.FileID);
                        updateCommand.Parameters.Add(parameter);
                        parameter = new SqlParameter("@ModifiedDate", DateTime.Now);
                        updateCommand.Parameters.Add(parameter);
                        parameter = new SqlParameter("@ModifiedBy", ident);
                        updateCommand.Parameters.Add(parameter);
                        updateCommand.ExecuteScalar();

                        //Get the Filepath
                        string pathName = unitOfWork.UofwFileRepository.GetFilepathByFileId(id, connection);

                        //Get the transaction context
                        byte[] txContext = unitOfWork.UofwFileRepository.GetTransactionContext(connection);            

                        sqlFile = new SqlFileStream(pathName, txContext, System.IO.FileAccess.Write);

                        byte[] buffer = new byte[1024 * 1024]; // 512Kb
                        int bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            sqlFile.Write(buffer, 0, bytesRead);
                            bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                        }

                        ts.Complete();
                        return true;

                }
                catch (Exception)
                {
                    ts.Dispose();
                    connection.Close();
                    return false;
                }
                finally
                {
                    if (sqlFile != null)
                        sqlFile.Close();
                    if (connection != null)
                        connection.Close();
                }
            }
            }
            return false;
        }

        /// <summary>
        /// UpdateFileStreamWithRevision - Adds a new version to an already existing file in the FileStore table based on the id.
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="id">Key of the file to update with next version</param>
        /// <returns>True on success and False on failure</returns>
        public int UpdateFileStreamWithRevision(HttpRequestBase Request, int id, int ident)
        {
            int retVal = -1;

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    SqlFileStream sqlFile = null;
                    SqlConnection connection = null;
                    SqlParameter parameter = null;
                    FileStore objFileOld = null;
                    FileStore objFileNew = null;

                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            objFileOld = unitOfWork.UofwFileRepository.GetFileById(id);

                            connection = new SqlConnection(this.getConnectionString());
                            connection.Open();

                            //Get FileStoreMain record
                            SqlCommand fsmCommand = connection.CreateCommand();
                            fsmCommand.CommandText = "SELECT ActiveVersion FROM dbo.FileStoreMain where [FileStoreMainID] = @Id";
                            parameter = new SqlParameter("@Id", objFileOld.FileStoreMainId);
                            fsmCommand.Parameters.Add(parameter);
                            int activeVersion = (int)fsmCommand.ExecuteScalar();
                            activeVersion = ++activeVersion;

                            SqlCommand updateCommandMain = connection.CreateCommand();
                            updateCommandMain.CommandText = "UPDATE dbo.FileStoreMain SET ActiveVersion = @ActiveVersion WHERE FileStoreMainId = @fsmId";
                            updateCommandMain.CommandType = CommandType.Text;

                            parameter = new SqlParameter("@fsmId", objFileOld.FileStoreMainId);
                            updateCommandMain.Parameters.Add(parameter);
                            parameter = new SqlParameter("@ActiveVersion", activeVersion);
                            updateCommandMain.Parameters.Add(parameter);
                            updateCommandMain.ExecuteScalar();

                            objFileNew = new FileStore();
                            objFileNew.FileStoreName = file.FileName.Split('\\')[file.FileName.Split('\\').Length - 1]; ;
                            objFileNew.FileContentType = file.ContentType;
                            objFileNew.FileStoreSize = Convert.ToInt64(file.ContentLength);
                            objFileNew.FileStoreID = Guid.NewGuid();

                            SqlCommand insertCommand = connection.CreateCommand();
                            insertCommand.CommandText = "INSERT INTO dbo.FileStore ([FileStoreID], [FileStoreName], [FileStoreSize], [FileContentType], [FileStoreContent], [VersionNumber], [FileStoreMainId], [CreatedDate], [CreatedBy]) VALUES (@Id, @Name, @len, @type, (0x), @VersionNumber, @FileStoreMainId, @CreatedDate, @CreatedBy)";
                            insertCommand.CommandType = CommandType.Text;

                            parameter = new SqlParameter("@Id", objFileNew.FileStoreID);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@Name", objFileOld.FileStoreName);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@type", objFileNew.FileContentType);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@len", objFileNew.FileStoreSize);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@VersionNumber", activeVersion);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@FileStoreMainId", objFileOld.FileStoreMainId);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@CreatedDate", DateTime.Now);
                            insertCommand.Parameters.Add(parameter);
                            parameter = new SqlParameter("@CreatedBy", ident);
                            insertCommand.Parameters.Add(parameter);
                            insertCommand.ExecuteScalar();

                            //Get pathName 
                            string computedPathName = unitOfWork.UofwFileRepository.GetFilepathByGuid(objFileNew.FileStoreID, connection);

                            //Get the Content type
                            string contentType = unitOfWork.UofwFileRepository.GetContentTypeByGuid(objFileNew.FileStoreID, connection);

                            //Get Transaction context
                            byte[] transactionContext = unitOfWork.UofwFileRepository.GetTransactionContext(connection); 

                            //Save file on FILESTREAM                         
                            sqlFile = new SqlFileStream(computedPathName, transactionContext, System.IO.FileAccess.Write);
                            byte[] buffer = new byte[1024 * 1024]; // 512Kb
                            int bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                            while (bytesRead > 0)
                            {
                                sqlFile.Write(buffer, 0, bytesRead);
                                bytesRead = file.InputStream.Read(buffer, 0, buffer.Length);
                            }

                            ts.Complete();
                            SqlCommand fileIdCommand = connection.CreateCommand();
                            fileIdCommand.CommandText = "SELECT FileId FROM dbo.FileStore where [FileStoreID] = @Id";
                            parameter = new SqlParameter("@Id", objFileNew.FileStoreID);
                            fileIdCommand.Parameters.Add(parameter);
                            retVal = (int)fileIdCommand.ExecuteScalar();

                            return retVal;
                        }
                        catch (Exception)
                        {
                            ts.Dispose();
                            connection.Close();
                            return retVal;
                        }
                        finally
                        {
                            if (sqlFile != null)
                                sqlFile.Close();
                            if (connection != null)
                                connection.Close();
                        }
                    }
                }
            }// For Each Loop
            return retVal; // If this LOC is reached that means there is no valid file to upload.
        }

        public bool RemoveFile(int id)
        {
            bool retVal = true;

            FileStoreMain fsMain = null;

            fsMain = unitOfWork.UofwFileRepository.GetFileStoreMainByID(unitOfWork.UofwFileRepository.GetFileById(id).FileStoreMainId);
            List<FileStore> fileList = fsMain.FileStores.ToList();
            if (fileList == null)
            {
                retVal = false;
            }

            if (retVal == true)
            {
                for (int i = 0; i < fileList.Count; i++)
                {
                    unitOfWork.UofwFileRepository.DeleteFileStore(fileList[i]);
                }
                if (fsMain.FileStores.Count() == 0)
                {
                    unitOfWork.UofwFileRepository.DeleteFileStoreMain(fsMain);
                }
                unitOfWork.Save();
            }

            return retVal;
        }

        private string getConnectionString()
        {
            IDbConnection tempConnection = unitOfWork.UofwFileRepository.getConnectionString();
            return tempConnection.ConnectionString;
        }

    }
}
