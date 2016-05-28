// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseInventory.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   Defines the BaseInventory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The base inventory.
    /// </summary>
    public class BaseInventory
    {
        /// <summary>
        /// Gets or sets the inventory id.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [JsonProperty("price")]
        public float Price { get; set; }

        /// <summary>
        /// Gets or sets the special price.
        /// </summary>
        [JsonProperty("special_price")]
        public float SpecialPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the special price is enabled.
        /// </summary>
        [JsonProperty("special_price_on")]
        public bool IsSpecialPriceEnabled { get; set; }

        /// <summary>
        /// Gets or sets the count of items with special price.
        /// </summary>
        [JsonProperty("special_price_amount")]
        public int SpecialPriceCount { get; set; }

        /// <summary>
        /// Gets or sets the count of all items in stock.
        /// </summary>
        [JsonProperty("in_stock")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the alcohol proof.
        /// </summary>
        [JsonProperty("proof")]
        public float AlcoholProof { get; set; }

        /// <summary>
        /// Gets or sets the alcohol by value.
        /// </summary>
        [JsonProperty("abv")]
        public float AlcoholByValue { get; set; }

        /// <summary>
        /// Gets or sets the bottle size in ml.
        /// </summary>
        [JsonProperty("bottle_size")]
        public int BottleSize { get; set; }

        /// <summary>
        /// Gets or sets the list of barcodes.
        /// Each barcode has to be unique per retail store, otherwise backend will report the error.
        /// </summary>
        [JsonProperty("barcodes")]
        public List<string> Barcodes { get; set; }
    }
}
