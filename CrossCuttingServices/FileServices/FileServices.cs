using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Sra.P2rmis.CrossCuttingServices.FileServices
{
    /// <summary>
    /// File services
    /// </summary>
    public static class FileServices
    {
        /// <summary>
        /// Gets the binary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static byte[] GetBinary(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Gets the MD5 has of the file for integrity checking purposes
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Base-64 represenations of MD5 file hash</returns>
        public static string GetMd5Hash(byte[] file)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(file);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Gets a string of a datetime in a file system friendly format
        /// </summary>
        /// <param name="dateTime">date time to convert</param>
        /// <returns>string representation of date in yyyy-MM-dd-HH-mm-ssfff format</returns>
        public static string GetTimestampForFileName(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd-HH-mm-ssfff");
        }
    }
}
