// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Area.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The area.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data.Abstract
{
    using Newtonsoft.Json;

    /// <summary>
    /// The area.
    /// </summary>
    public abstract class Area : DescriptionNameID
    {
        /// <summary>
        /// Gets or sets the URL of the map image.
        /// </summary>
        [JsonProperty("image_url")]
        public string MapImageURL { get; set; }
    }
}
