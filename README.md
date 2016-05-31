# Sync inventory using Tipsi integration API

## Sample Code

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
              // all fields described here https://github.com/Nepherhotep/tipsi-sync-c-sharp-sample/wiki/API-Description#base-inventory-struct
              { "barcode", "22-0010" },  // barcode, which will be used to lookup inventory item
              { "barcodes": ["22-0010", "22-0020"] },  // you can specify alternate barcodes here, will override existing barcodes
              { "bottle_size", 750 },  // bottle size in ml (standard bottle - 750ml)
              { "price", 34.4 },  // bottle price
              { "in_stock", 12 }  // number of items in stock
          },
      new Dictionary<string, object>
          {
              { "barcode", "000-234-000" },
              { "barcodes": ["000-234-000"] },
              { "bottle_size", 750 },
              { "price", 34.4 },
              { "in_stock", 12 }
          }
  };

TipsiClient tipsiClient = new TipsiClient(baseAddress, apiVersion, login, password);
tipsiClient.LoginAsync().Wait();
Console.WriteLine("Login is successful!!!");

// sync clear will automatically set in_stock param to "0" for unlisted items in the given batch
syncResult = tipsiClient.SyncClearAsync(storeID, syncData).Result;
```

## Video
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/gCw0NtvPm_o/0.jpg)](https://www.youtube.com/watch?v=gCw0NtvPm_o)
