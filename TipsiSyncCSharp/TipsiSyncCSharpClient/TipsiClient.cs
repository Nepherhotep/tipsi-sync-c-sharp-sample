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
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using TipsiSyncCSharpClient.Models;
    using TipsiSyncCSharpClient.Settings;

    /// <summary>
    /// The Tipsi http client.
    /// </summary>
    public class TipsiClient
    {
        /// <summary>
        /// The application/json media type.
        /// </summary>
        public const string ApplicationJSONMediaType = "application/json";

        /// <summary>
        /// The standart http client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The user credentials.
        /// </summary>
        private readonly UserCredentials _userCredentials;

        /// <summary>
        /// Logins at the service.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task LoginAsync()
        {
            HttpResponseMessage result = await _httpClient.PostAsync(
                Address.Default.LoginURL,
                new StringContent(
                    _userCredentials.ToJSON(),
                    Encoding.UTF8,
                    ApplicationJSONMediaType));

            result.EnsureSuccessStatusCode();
        }
        
        public void Sync(string batchJson)
        {
        }

        public void SyncClear(string batchJson)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TipsiClient"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        public TipsiClient(string login, string password)
        {
            // Create and configure standart http client.
            _httpClient = new HttpClient { BaseAddress = new Uri(Address.Default.DomainURL) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJSONMediaType));

            // Keep credentials.
            _userCredentials = new UserCredentials(login, password);
        }
    }
}
