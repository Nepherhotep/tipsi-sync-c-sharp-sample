// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncResult.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The sync result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The sync result.
    /// </summary>
    public class SyncResult
    {
        /// <summary>
        /// Gets or sets the created items count.
        /// </summary>
        [JsonProperty("created_items")]
        public int? CreatedItemsCount { get; set; }

        /// <summary>
        /// Gets or sets the cleared items count.
        /// </summary>
        [JsonProperty("cleared_items")]
        public int? ClearedItemsCount { get; set; }

        /// <summary>
        /// Gets or sets the updated items count.
        /// </summary>
        [JsonProperty("updated_items")]
        public int? UpdatedItemsCount { get; set; }
    }
}
