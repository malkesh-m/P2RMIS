using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sra.P2rmis.CrossCuttingServices.HttpServices
{
    /// <summary>
    /// A wrapped HTTP client for generic/core web service calls
    /// </summary>
    /// <seealso cref="System.Net.Http.HttpClient" />
    public class CoreHttpClient : HttpClient
    {
        /// <summary>
        /// The basic scheme
        /// </summary>
        public const string BasicScheme = "Basic";
        /// <summary>
        /// The timeout seconds
        /// </summary>
        public const int TimeoutSeconds = 60;

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreHttpClient"/> class.
        /// </summary>
        /// <param name="credentialKey">The credential key.</param>
        public CoreHttpClient(string credentialKey) : base()
        {
            Init(credentialKey);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreHttpClient"/> class.
        /// </summary>
        /// <param name="credentialKey">The credential key.</param>
        /// <param name="handler">The http client handler</param>
        public CoreHttpClient(string credentialKey, HttpClientHandler handler) : base(handler)
        {
            Init(credentialKey);
        }
        /// <summary>
        /// Initialize the HTTP client.
        /// </summary>
        /// <param name="credentialKey">The credential key.</param>
        public void Init(string credentialKey)
        {
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BasicScheme, credentialKey);
            this.Timeout = new TimeSpan(0, 0, TimeoutSeconds);
        }
        #endregion
        #region Methods                  
        /// <summary>
        /// Gets the byte array asynchronous task.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public async Task<byte[]> GetByteArrayAsyncTask(string uri)
        {
            var response = await this.GetAsync(uri);

            // Will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
        /// <summary>
        /// Gets the string asynchronous task.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public async Task<string> GetStringAsyncTask(string uri)
        {
            var response = await this.GetAsync(uri);

            // Will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Gets the byte array asynchronous result.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public byte[] GetByteArrayAsyncResult(string url)
        {
            var returnTask = Task.Run(() => GetByteArrayAsyncTask(url));
            returnTask.Wait();
            var byteArray = returnTask.Result;
            return byteArray;
        }
        /// <summary>
        /// Gets the string asynchronous result.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string GetStringAsyncResult(string url)
        {
            try
            {
                var returnTask = Task.Run(() => GetStringAsyncTask(url));
                returnTask.Wait();
                var result = returnTask.Result;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  // used to avoid compiler warnings
                return null;
            }
        }
        /// <summary>
        /// Gets the byte array asynchronous result.
        /// </summary>
        /// <param name="urls">The urls.</param>
        /// <returns></returns>
        /// <remarks>Handles the scenario when we need to process multiple urls asynchronous</remarks>
        public List<byte[]> GetByteArrayAsyncResult(List<string> urls)
        {
            var byteArrays = new List<byte[]>();
            var returnTasks = new List<Task<byte[]>>();
            for (var i = 0; i < urls.Count; i++)
            {
                returnTasks.Add(Task.Run(() => GetByteArrayAsyncTask(urls[i])));
            }
            for (var i = 0; i < urls.Count; i++)
            {
                returnTasks[0].Wait();
                byteArrays.Add(returnTasks[0].Result);
            }
            return byteArrays;
        }
        /// <summary>
        /// Gets the string asynchronous result.
        /// </summary>
        /// <param name="urls">The urls.</param>
        /// <returns></returns>
        public List<string> GetStringAsyncResult(List<string> urls)
        {
            var results = new List<string>();
            var returnTasks = new List<Task<string>>();
            for (var i = 0; i < urls.Count; i++)
            {
                returnTasks.Add(Task.Run(() => GetStringAsyncTask(urls[i])));
            }
            for (var i = 0; i < urls.Count; i++)
            {
                returnTasks[0].Wait();
                results.Add(returnTasks[0].Result);
            }
            return results;
        }
        #endregion
    }
}
