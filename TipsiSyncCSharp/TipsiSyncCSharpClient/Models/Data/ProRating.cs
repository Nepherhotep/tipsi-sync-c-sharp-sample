// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProRating.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The pro rating.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// The pro rating.
    /// </summary>
    public class ProRating
    {
        /// <summary>
        /// Gets or sets the rating long name.
        /// </summary>
        [JsonProperty("name")]
        public string LongName { get; set; }

        /// <summary>
        /// Gets or sets the shortcut.
        /// </summary>
        [JsonProperty("shortcut")]
        public string Shortcut { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// Value has to be from 0 to 100.
        /// </summary>
        [JsonProperty("rating")]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the description of the given rating value.
        /// </summary>
        [JsonProperty("rating_description")]
        public string Description { get; set; }
    }
}
