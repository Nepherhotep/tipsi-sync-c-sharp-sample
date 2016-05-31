// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpUtilities.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   Defines the HttpUtilities type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Utilities
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// The http utilities and extensions.
    /// </summary>
    public static class HttpUtilities
    {
        /// <summary>
        /// Sends http request with PATCH method.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task<HttpResponseMessage> PatchAsync(
            this HttpClient client,
            string requestUri,
            HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
                                             {
                                                 Content = content
                                             };

            return await client.SendAsync(request);
        }
    }
}
