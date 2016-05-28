// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameID.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the NameID type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data.Abstract
{
    using Newtonsoft.Json;

    /// <summary>
    /// The class contains mapped Name and ID properties.
    /// </summary>
    public abstract class NameID
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
