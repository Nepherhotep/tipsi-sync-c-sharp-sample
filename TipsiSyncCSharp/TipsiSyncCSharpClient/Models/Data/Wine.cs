// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wine.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The wine.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using TipsiSyncCSharpClient.Models.Data.Abstract;

    /// <summary>
    /// The wine.
    /// </summary>
    public class Wine : NameID
    {
        /// <summary>
        /// The wine type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            /// <summary>
            /// The regular.
            /// </summary>
            [EnumMember(Value = "regular")]
            Regular,

            /// <summary>
            /// The fortified.
            /// </summary>
            [EnumMember(Value = "fortified")]
            Fortified,

            /// <summary>
            /// The sparkling.
            /// </summary>
            [EnumMember(Value = "sparkling")]
            Sparkling,

            /// <summary>
            /// The dessert.
            /// </summary>
            [EnumMember(Value = "dessert")]
            Dessert,

            /// <summary>
            /// The off dry.
            /// </summary>
            [EnumMember(Value = "offdry")]
            Offdry
        }

        /// <summary>
        /// Gets or sets the year of vintage or "NV".
        /// </summary>
        [JsonProperty("vintage")]
        public string Vintage { get; set; }

        /// <summary>
        /// Gets or sets the winery.
        /// </summary>
        [JsonProperty("winery")]
        public Winery Winery { get; set; }

        /// <summary>
        /// Gets or sets the vineyard.
        /// </summary>
        [JsonProperty("vineyard")]
        public Vineyard Vineyard { get; set; }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        [JsonProperty("designation")]
        public Designation Designation { get; set; }

        /// <summary>
        /// Gets or sets the varietals.
        /// </summary>
        [JsonProperty("varietals")]
        public List<Varietal> Varietals { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonProperty("country")]
        public Country Country { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [JsonProperty("region")]
        public Region Region { get; set; }

        /// <summary>
        /// Gets or sets the sub regions.
        /// </summary>
        [JsonProperty("sub_regions")]
        public List<SubRegion> SubRegions { get; set; }

        /// <summary>
        /// Gets or sets the wine type.
        /// </summary>
        [JsonProperty("type")]
        public Type WineType { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// Values from 1 to 3 - White, 4 - Rose, from 5 to 7 - Red.
        /// </summary>
        [JsonProperty("color")]
        public int Color { get; set; }

        /// <summary>
        /// Gets or sets the URL of wine label image.
        /// </summary>
        [JsonProperty("label_url")]
        public string LabelImageURL { get; set; }

        /// <summary>
        /// Gets or sets the URL to wine producer or re-seller.
        /// </summary>
        [JsonProperty("wine_url")]
        public string WineURL { get; set; }

        /// <summary>
        /// Gets or sets the pro ratings.
        /// </summary>
        [JsonProperty("pro_rating")]
        public List<ProRating> ProRatings { get; set; }
    }
}
