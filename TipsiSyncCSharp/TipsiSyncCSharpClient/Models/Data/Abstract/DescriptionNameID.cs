// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescriptionNameID.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   Defines the DescriptionNameID type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models.Data.Abstract
{
    using Newtonsoft.Json;

    /// <summary>
    /// The description name id.
    /// </summary>
    public abstract class DescriptionNameID : NameID
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
