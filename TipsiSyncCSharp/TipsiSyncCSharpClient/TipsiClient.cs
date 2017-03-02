// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TipsiClient.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   Defines the TipsiClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using TipsiSyncCSharpClient.Models;
    using TipsiSyncCSharpClient.Utilities;

    /// <summary>
    /// The Tipsi http client.
    /// </summary>
    public class TipsiClient
    {
        /// <summary>
        /// The external_id constant.
        /// </summary>
        public const string ExternalId = "external_id";

        /// <summary>
        /// The application/json media type.
        /// </summary>
        private const string ApplicationJSONMediaType = "application/json";

        /// <summary>
        /// The login route pattern.
        /// {0} - is version.
        /// </summary>
        private const string LoginRoutePattern = "/api/rest/{0}/login";

        /// <summary>
        /// The sync route pattern.
        /// {0} - is version.
        /// {1} - is store id.
        /// </summary>
        private const string SyncRoutePattern = "/api/rest/{0}/store/{1}/ext/sync";
        
        /// <summary>
        /// The sync clear route pattern.
        /// {0} - is version.
        /// {1} - is store id.
        /// </summary>
        private const string SyncClearRoutePattern = "api/rest/{0}/store/{1}/ext/sync_clear";
                
        /// <summary>
        /// The barcode route pattern.
        /// {0} - is version.
        /// {1} - is store id.
        /// {2} - is external_id.
        /// </summary>
        private const string BarcodeRoutePattern = "api/rest/{0}/store/{1}/ext/{2}";

        /// <summary>
        /// The count of items per chunk.
        /// </summary>
        private const int ChunkSize = 300;

        /// <summary>
        /// The timeout in seconds.
        /// </summary>
        private const int TimeOut = 300;

        /// <summary>
        /// The version.
        /// </summary>
        private readonly string _version;

        /// <summary>
        /// The login route.
        /// </summary>
        private readonly string _loginRoute;

        /// <summary>
        /// The standart http client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The user credentials.
        /// </summary>
        private readonly UserCredentials _userCredentials;

        /// <summary>
        /// Checks sync data.
        /// </summary>
        /// <param name="syncData">The sync data.</param>
        private void CheckSyncData(List<Dictionary<string, object>> syncData)
        {
            foreach (Dictionary<string, object> dictionary in syncData)
            {
                if (!dictionary.Keys.Contains(ExternalId))
                {
                    throw new ArgumentException("Each syncData dictionary must contain 'external_id' key. Please add '{external_id: EXTERNAL_ID_VALUE}' key-value pair to the each dictionary in the list.");
                }
            }
        }

        /// <summary>
        /// Checks resopnse for errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The <see cref="Task"/>. The responce content.</returns>
        /// <exception cref="Exception"></exception>
        private async Task<string> CheckResopnse(HttpResponseMessage response)
        {
            string responceContent = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(responceContent, ex);
            }

            return responceContent;
        }

        /// <summary>
        /// Clears all items in the given store.
        /// </summary>
        /// <param name="storeId">The store ID.</param>
        /// <returns>The result of operation.</returns>
        private async Task<SyncResult> ClearAsync(string storeId)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync(
                string.Format(SyncClearRoutePattern, _version, storeId),
                new StringContent(JsonConvert.SerializeObject(new List<Dictionary<string, object>>()), Encoding.UTF8,
                    ApplicationJSONMediaType));

            string responceContent = await CheckResopnse(response);
            return JsonConvert.DeserializeObject<SyncResult>(responceContent);
        }

        /// <summary>
        /// Synchronizes the chunk of data asynchronously.
        /// </summary>
        /// <param name="storeId">The store ID.</param>
        /// <param name="syncData">The wine Inventory For Syncs.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task<SyncResult> SyncChunckAsync(string storeId, List<Dictionary<string, object>> syncData)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync(
                string.Format(SyncRoutePattern, _version, storeId),
                new StringContent(JsonConvert.SerializeObject(syncData), Encoding.UTF8, ApplicationJSONMediaType));

            string responceContent = await CheckResopnse(response);
            return JsonConvert.DeserializeObject<SyncResult>(responceContent);
        }

        /// <summary>
        /// Logins at the service.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task LoginAsync()
        {
            HttpResponseMessage responce = await _httpClient.PostAsync(
                    _loginRoute,
                    new StringContent(_userCredentials.ToJSON(), Encoding.UTF8, ApplicationJSONMediaType));

            await CheckResopnse(responce);
        }

        /// <summary>
        /// Synchronizes the data asynchronously.
        /// </summary>
        /// <param name="storeId">The store ID.</param>
        /// <param name="syncData">The wine Inventory For Syncs.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<SyncResult> SyncAsync(string storeId, List<Dictionary<string, object>> syncData)
        {
            List<Dictionary<string, object>> chuckData = new List<Dictionary<string, object>>();
            SyncResult syncResult = new SyncResult();

            for (int i = 0; i < syncData.Count; i++)
            {
                chuckData.Add(syncData[i]);
                if (i % ChunkSize == 0)
                {
                    SyncResult chunkSyncResult = await SyncChunckAsync(storeId, chuckData);
                    syncResult += chunkSyncResult;
                    chuckData.Clear();
                }
            }

            if (chuckData.Count > 0)
            {
                SyncResult chunkSyncResult = await SyncChunckAsync(storeId, chuckData);
                syncResult += chunkSyncResult;
            }

            return syncResult;
        }

        /// <summary>
        /// Clears items, except the given.
        /// </summary>
        /// <param name="storeId">The store ID.</param>
        /// <param name="syncData">The sync data.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<SyncResult> SyncClearAsync(string storeId, List<Dictionary<string, object>> syncData)
        {
            SyncResult syncResult = await ClearAsync(storeId);
            syncResult += await SyncAsync(storeId, syncData);
            return syncResult;
        }

        /// <summary>
        /// Gets the external_id matches async.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="externalId">The external ID.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<JObject> BarcodeMatchAsync(string storeId, string externalId, Dictionary<string, string> parameters)
        {
            string requestUri = parameters != null && parameters.Count > 0
                ? string.Format(
                    "{0}?{1}",
                    string.Format(BarcodeRoutePattern, _version, storeId, externalId),
                    parameters.ToGetParameters())
                : string.Format(BarcodeRoutePattern, _version, storeId, externalId);

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            string responceContent = await CheckResopnse(response);
            return JsonConvert.DeserializeObject(responceContent) as JObject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TipsiClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The protocol and domain name (f.e. https://integration.gettipsi.com)</param>
        /// <param name="version">The API version. (f.e. "v001").</param>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        public TipsiClient(string baseAddress, string version, string login, string password)
        {
            _version = version;
            _loginRoute = string.Format(LoginRoutePattern, version);

            // Create and configure standart http client.
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress.TrimEnd('/')) };
            _httpClient.Timeout = new TimeSpan(0, 0, 0, TimeOut);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJSONMediaType));

            // Keep credentials.
            _userCredentials = new UserCredentials(login, password);
        }
    }
}
