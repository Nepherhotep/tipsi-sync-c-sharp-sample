// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCredentials.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The user credentials.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharpClient.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The user credentials.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// Gets the login.
        /// </summary>
        [JsonProperty("username")]
        public string Login { get; private set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; private set; }

        /// <summary>
        /// Converts the object to JSON string.
        /// </summary>
        /// <returns>The JSON string.</returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentials"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        public UserCredentials(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
