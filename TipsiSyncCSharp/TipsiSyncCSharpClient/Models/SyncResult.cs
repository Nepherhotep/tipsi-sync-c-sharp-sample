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

        /// <summary>
        /// The overraided '+' operator.
        /// </summary>
        /// <param name="first">The first object.</param>
        /// <param name="second">The second object</param>
        /// <returns></returns>
        public static SyncResult operator +(SyncResult first, SyncResult second)
        {
            return new SyncResult
            {
                ClearedItemsCount = first.ClearedItemsCount + second.ClearedItemsCount,
                CreatedItemsCount = first.CreatedItemsCount + second.CreatedItemsCount,
                UpdatedItemsCount = first.UpdatedItemsCount + second.UpdatedItemsCount
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncResult"/> class.
        /// </summary>
        public SyncResult()
        {
            ClearedItemsCount = 0;
            CreatedItemsCount = 0;
            UpdatedItemsCount = 0;
        }
    }
}
