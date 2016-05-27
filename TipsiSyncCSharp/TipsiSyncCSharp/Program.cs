// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nepherhotep">
//   Nepherhotep
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TipsiSyncCSharp
{
    using System;

    using TipsiSyncCSharpClient;

    /// <summary>
    /// The program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        static void Main(string[] args)
        {
            TipsiClient tipsiClient = new TipsiClient("YOUR_LOGIN", "YOUR_PASSWORD");
            tipsiClient.LoginAsync().Wait();
            Console.WriteLine("Login is successful!!!");

            // now you can sync
            //client.Sync(batchJson).Wait();

            // or
            //client.SyncClear(batchJson).Wait();

            Console.ReadKey();
        }
    }
}
