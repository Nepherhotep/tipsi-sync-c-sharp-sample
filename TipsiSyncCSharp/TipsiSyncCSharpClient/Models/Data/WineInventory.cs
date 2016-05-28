// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WineInventory.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The wine inventory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The wine inventory.
    /// </summary>
    public class WineInventory : BaseInventory
    {
        /// <summary>
        /// The matching status.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum MatchingStatus
        {
            /// <summary>
            /// The match pending.
            /// </summary>
            [EnumMember(Value = "match_pending")]
            MatchPending,

            /// <summary>
            /// The in progress.
            /// </summary>
            [EnumMember(Value = "in_progress")]
            InProgress,

            /// <summary>
            /// The match complete.
            /// </summary>
            [EnumMember(Value = "match_complete")]
            MatchComplete
        }

        /// <summary>
        /// Gets or sets the related wine.
        /// </summary>
        [JsonProperty("wine")]
        public Wine RelatedWine { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("status")]
        public MatchingStatus Status { get; set; }
    }
}
