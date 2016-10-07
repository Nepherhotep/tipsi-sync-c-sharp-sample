// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncResult.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The food ids responce result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The food ids responce result.
    /// </summary>
    public class FoodIdsResponceModel
    {
        /// <summary>
        /// The result items count.
        /// </summary>
        [JsonProperty("count")]
        public int ResultsCount { get; set; }

        /// <summary>
        /// The next.
        /// </summary>
        [JsonProperty("next")]
        public object Next { get; set; }

        /// <summary>
        /// The previous.
        /// </summary>
        [JsonProperty("previous")]
        public object Previous { get; set; }

        /// <summary>
        /// The list of food ID.
        /// </summary>
        [JsonProperty("results")]
        public List<Dictionary<string, object>> Results { get; set; }
    }
}
