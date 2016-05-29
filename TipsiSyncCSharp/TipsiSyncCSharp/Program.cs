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
    using System.Collections.Generic;

    using TipsiSyncCSharpClient;
    using TipsiSyncCSharpClient.Models;

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
            // Prepare data
            string login = "bottles-cases-api";
            string password = "3bYtXvmTBZXX";
            string apiVersion = "v001";
            string storeID = "19351";
            string baseAddress = "https://test.gettipsi.com";
            List<Dictionary<string, object>> syncData = new List<Dictionary<string, object>>
                                                            {
                                                                new Dictionary<string, object>
                                                                    {
                                                                        { "barcode", "bar-525" },
                                                                        { "bottle_size", 750 },
                                                                        { "price", 34.4 },
                                                                        { "in_stock", 12 }
                                                                    },
                                                                new Dictionary<string, object>
                                                                    {
                                                                        { "barcode", "bar-504" },
                                                                        { "bottle_size", 750 },
                                                                        { "price", 34.4 },
                                                                        { "in_stock", 12 }
                                                                    }
                                                            };
            
            TipsiClient tipsiClient = new TipsiClient(baseAddress, apiVersion, login, password);
            tipsiClient.LoginAsync().Wait();
            Console.WriteLine("Login is successful!!!");

            // now you can sync
            SyncResult syncResult = tipsiClient.SyncAsync(storeID, syncData).Result;
            PrintSyncResult(syncResult);

            // or
            syncResult = tipsiClient.SyncClearAsync(storeID, syncData).Result;
            PrintSyncResult(syncResult);
        }

        /// <summary>
        /// Print sync result at console.
        /// </summary>
        /// <param name="syncResult">The sync result.</param>
        private static void PrintSyncResult(SyncResult syncResult)
        {
            Console.WriteLine();
            if (syncResult.CreatedItemsCount != null)
            {
                Console.WriteLine("Created items count: {0}", syncResult.CreatedItemsCount);
            }

            if (syncResult.ClearedItemsCount != null)
            {
                Console.WriteLine("Cleared items count: {0}", syncResult.ClearedItemsCount);
            }

            if (syncResult.UpdatedItemsCount != null)
            {
                Console.WriteLine("Updated items count: {0}", syncResult.UpdatedItemsCount);
            }
        }
    }
}
