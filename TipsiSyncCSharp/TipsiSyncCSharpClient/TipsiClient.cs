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
        private const string SyncRoutePattern = "/api/rest/{0}/store/{1}/barcode/sync";
        
        /// <summary>
        /// The sync clear route pattern.
        /// {0} - is version.
        /// {1} - is store id.
        /// </summary>
        private const string SyncClearRoutePattern = "api/rest/{0}/store/{1}/barcode/sync_clear";

        /// <summary>
        /// The food IDs route pattern.
        /// {0} - is version.
        /// </summary>
        private const string FoodIdsRoutePattern = "api/rest/{0}/food";

        /// <summary>
        /// The food scoring struct route pattern.
        /// {0} - is version.
        /// {1} - is store id.
        /// </summary>
        private const string WineRoutePattern = "api/rest/{0}/store/{1}/wine";

        /// <summary>
        /// The food_fields constant.
        /// </summary>
        public const string FoodFields = "food_fields";

        /// <summary>
        /// The ID key.
        /// </summary>
        public const string IdKey = "id";

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
        /// The cached food data.
        /// </summary>
        private Dictionary<long, Dictionary<string, object>> _foodDictionary;

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

        private string BuildGetParametersString(Dictionary<string, string> parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                stringBuilder.Append("&" + parameter.Key + "=" + parameter.Value);
            }

            stringBuilder[0] = '?';
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Fetchs food ids.
        /// </summary>
        /// <returns></returns>
        private async Task FetchFoodIdsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                string.Format(FoodIdsRoutePattern, _version) +
                "?food_fields=id,meal,preparation,image_url,image_selected_url");

            string responceContent = await CheckResopnse(response);
            FoodIdsResponceModel foodIdsResponceModel = JsonConvert.DeserializeObject<FoodIdsResponceModel>(responceContent);
            _foodDictionary = new Dictionary<long, Dictionary<string, object>>();
            foreach (Dictionary<string, object> result in foodIdsResponceModel.Results)
            {
                _foodDictionary.Add((long)result[IdKey], result);
            }
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
            HttpResponseMessage response = await _httpClient.PatchAsync(
                    string.Format(SyncRoutePattern, _version, storeId),
                    new StringContent(JsonConvert.SerializeObject(syncData), Encoding.UTF8, ApplicationJSONMediaType));

            string responceContent = await CheckResopnse(response);
            return JsonConvert.DeserializeObject<SyncResult>(responceContent);
        }

        /// <summary>
        /// Clears items, except the given.
        /// </summary>
        /// <param name="storeId">The store ID.</param>
        /// <param name="syncData">The sync data.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<SyncResult> SyncClearAsync(string storeId, List<Dictionary<string, object>> syncData)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync(
                    string.Format(SyncClearRoutePattern, _version, storeId),
                    new StringContent(JsonConvert.SerializeObject(syncData), Encoding.UTF8, ApplicationJSONMediaType));

            string responceContent = await CheckResopnse(response);
            return JsonConvert.DeserializeObject<SyncResult>(responceContent);
        }

        public async Task<object> BarcodeMatchAsync(string storeID, Dictionary<string, string> parameters)
        {
            string foodFieldsParameter;
            if (parameters.TryGetValue(FoodFields, out foodFieldsParameter))
            {
                if (_foodDictionary == null)
                {
                    await FetchFoodIdsAsync();
                }

                if (!foodFieldsParameter.Contains(IdKey))
                {
                    parameters[FoodFields] = IdKey + "," + foodFieldsParameter;
                }
            }

            string parametersString = BuildGetParametersString(parameters);
            HttpResponseMessage response = await _httpClient.GetAsync(string.Format(WineRoutePattern, _version, storeID) + parametersString);
            string responceContent = await CheckResopnse(response);
            JObject jObject = JObject.Parse(responceContent);
            if (foodFieldsParameter != null)
            {
                JToken resultsToken = jObject["results"];
                if (resultsToken != null)
                {
                    int resultsCount = resultsToken.Count();
                    for (int i = 0; i < resultsCount; i++)
                    {
                        JToken jToken = resultsToken[i];
                        if (jToken != null)
                        {
                            jToken = jToken["wine"];
                            if (jToken != null)
                            {
                                jToken = jToken["food_scoring"];
                                int scoresCount = jToken.Count();
                                for (int j = 0; j < scoresCount; j++)
                                {
                                    long id = jToken[j]["id"].Value<long>();
                                    Dictionary<string, object> foodData = _foodDictionary[id];
                                    foreach (var keyValue in foodData)
                                    {
                                        // Add requested fields.
                                        if (foodFieldsParameter.Contains(keyValue.Key))
                                        {
                                            jToken[j][keyValue.Key] = new JValue(keyValue.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return jObject;
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
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJSONMediaType));

            // Keep credentials.
            _userCredentials = new UserCredentials(login, password);
        }
    }
}
