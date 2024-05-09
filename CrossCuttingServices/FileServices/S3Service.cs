using System;
using System.Collections.Generic;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using Amazon.S3.Util;

namespace Sra.P2rmis.CrossCuttingServices.FileServices
{
    /// <summary>
    /// Collection of helpers for interacting with the configured S3 bucket
    /// </summary>
    public static class S3Service
    {
        static string bucketName => ConfigurationServices.ConfigManager.S3BucketName;
        static IAmazonS3 client;

        /// <summary>
        /// Lists the associated files.
        /// </summary>
        /// <param name="prefix">The prefix to find.</param>
        /// <param name="folderName">Name of the folder in which the object resides.</param>
        /// <returns>
        /// List of s3 object keys with file size
        /// </returns>
        public static List<Tuple<string, long>> ListAssociatedFiles(string prefix, string folderName)
        {
            List<Tuple<string, long>> fileList = new List<Tuple<string, long>>();
            using (client = new AmazonS3Client(RegionEndpoint.USGovCloudWest1))
            {
                ListObjectsV2Request request = new ListObjectsV2Request()
                {
                    BucketName = bucketName,
                    Prefix = BuildQualifiedLookup(folderName, prefix)
                };
                ListObjectsV2Response response = new ListObjectsV2Response();
                response = client.ListObjectsV2(request);
                foreach (S3Object entry in response.S3Objects)
                {
                    fileList.Add(new Tuple<string, long>(entry.Key, entry.Size));
                }
            }
            return fileList;
        }

        /// <summary>
        /// Gets the pre signed URL.
        /// </summary>
        /// <param name="key">The object key.</param>
        /// <param name="folderName">Name of the folder in which the object resides.</param>
        /// <returns>
        /// Pre signed URL for downloading the requested object
        /// </returns>
        public static string GetPreSignedUrl(string key, string folderName)
        {
            string urlString = string.Empty;
            using (client = new AmazonS3Client(RegionEndpoint.USGovCloudWest1))
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = BuildQualifiedLookup(folderName, key),
                    Expires = DateTime.Now.AddMinutes(5)
                };
                urlString = client.GetPreSignedURL(request1);
            }
            return urlString;
        }

        /// <summary>
        /// Gets the file contents of an S3 object as byte array.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>byte array of object contents</returns>
        public static byte[] GetFileContents(string key, string folderName)
        {
            using (client = new AmazonS3Client(RegionEndpoint.USGovCloudWest1))
            {
                GetObjectRequest request = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = BuildQualifiedLookup(folderName, key)
                };
                using (GetObjectResponse response = client.GetObject(request))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        response.ResponseStream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Writes a files contents to S3
        /// </summary>
        /// <param name="fileContent">Byte representation of file to upload</param>
        /// <param name="key">Path where file resides within the bucket and folder</param>
        /// <param name="folder">Base S3 folder path containing the key</param>
        public static void WriteFileContents(byte[] fileContent, string key, string folderName)
        {
            using (Stream fileStream = new MemoryStream(fileContent))
            {
                using (client = new AmazonS3Client(RegionEndpoint.USGovCloudWest1))
                {
                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = BuildQualifiedLookup(folderName, key),
                        InputStream = fileStream
                    };
                    PutObjectResponse response = client.PutObject(request);
                }
            }
        }
        /// <summary>
        /// Builds a fully qualified S3 key/prefix lookup.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="prefix">The prefix or key of the object.</param>
        /// <returns>Qualified lookup used by S3</returns>
        internal static string BuildQualifiedLookup(string folderName, string prefix)
        {
            return $"{folderName}/{prefix}";
        }
    }
}
