# tipsi-sync-c-sharp-sample
Sample library to sync with Tipsi

```cs
string login = "USERNAME";
string password = "PASSWORD";
string apiVersion = "v001";
string storeID = "STORE_ID";
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
```
