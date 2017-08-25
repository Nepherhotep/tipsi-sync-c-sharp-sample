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

    using Newtonsoft.Json.Linq;

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
            string login = "USERNAME";
            string password = "PASSWORD";
            string apiVersion = "v001";
            string storeID = "STORE_ID";
            string baseAddress = "https://integration-test.gettipsi.com";

            List<Dictionary<string, object>> syncData = new List<Dictionary<string, object>>
                                                            {
                                                                new Dictionary<string, object>
                                                                    {
                                                                        { TipsiClient.ExternalId, 229445 },
                                                                        { "unit_size", "750ML" },
                                                                        { "price", 34.4 },
                                                                        { "in_stock", 12 },
                                                                        { "barcodes", new [] {12332323232323} }
                                                                    },
                                                                new Dictionary<string, object>
                                                                    {
                                                                        { TipsiClient.ExternalId, 227985 },
                                                                        { "unit_size", "750ML" },
                                                                        { "price", 34.4 },
                                                                        { "in_stock", 12 },
                                                                        { "barcodes", new [] {1233232323232323} }
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

            string externalId = "EXTERNAL_ID";

            // Get Product By externalId
            JObject jObject =
                tipsiClient.ProductMatchAsync(
                    storeID,
                    externalId,
                    new Dictionary<string, string>
                        {
                            { "wine_fields", "id,winery,region" },
                            { "inventory_fields", "id,wine" },
                            { "winery_fields", "id,name" },
                            { "region_fields", "id,name,description,image_url" }
                        }).Result;
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
