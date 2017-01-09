# Endpoints
## Login Endpoint
It's possible to pass params both as JSON or POST params


| URL | https://DOMAIN/api/rest/v001/login |
| --- | --- |
| Method | POST |
| Params | username, password |


## Sync Inventory
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/barcode/sync |
| --- | --- |
| Method | PATCH |
| Request Content | JSON encoded array list of [inventory structs](#base-inventory-struct). Each struct has to contain "barcode" param, which will be used to lookup related item in inventory. Unnecessary params can be omitted, in that case they will not be updated. By the way, as barcode is used to lookup inventory, inventory "id" param is prohibited here due to the request will be ambiguous, thus trying to use it will cause error with HTTP respone 400. |


## Sync Inventory With Clearing
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/barcode/sync_clear
| --- | --- |
| Method | PATCH |

Same, as [Sync Inventory Endpoint](#sync-inventory), except will clear inventory not listed in the batch. It's a safe method as it will just mark in_stock parameter to 0. For real inventory deletion DELETE method should be used upon each inventory item.

## List Wine Inventory
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/wine |
| --- | --- |
| Method | GET |
| GET Params | List of fields for each struct [inventory structs](#base-inventory-struct). If not specified which fields to fetch for a given struct, it will contain only "id" parameter. Param name, related to a given struct, listed below struct tables |

```
https://DOMAIN.gettipsi.com/api/rest/v001/store/STORE ID/wine?wine_fields=id,winery,region&inventory_fields=id,wine&winery_fields=id,name&region_fields=id,name,description,image_url
```

## List Drink Inventory
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/drink |
| --- | --- |
| Method | GET |
| GET Params | List of fields for each struct [inventory structs](#base-inventory-struct), similar to wine list. |


## Create Wine Inventory
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/wine |
| --- | --- |
| Method | POST |
| POST Params | JSON without nested fields [inventory structs](#base-inventory-struct).|

Minimal parameters for wine: `barcodes`, `external_id`, `wine_id`

```javascript
// POST /api/rest/v001/store/38/wine
// Data: {"external_id": 3001, "barcodes": ["wine-123"], "wine_id": 311}
// Response code: 201
({
     "abv": null,
     "barcodes": [
         "wine-123"
     ],
     "external_id": 3001,
     "id": 421,
     "in_stock": null,
     "price": null,
     "proof": null,
     "special_price": null,
     "special_price_amount": 0,
     "special_price_on": false,
     "status": "match_complete",
     "unit_size": null
 })
```

## Create Drink Inventory
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/drink |
| --- | --- |
| Method | POST |
| POST Params | JSON without nested fields [inventory structs](#base-inventory-struct), similar to wine list. |

Minimal parameters for wine: `barcodes`, `external_id`, `drink_id`


## List Food
| URL | https://DOMAIN/api/rest/v001/food |
| --- | --- |
| Method | GET |
| GET Params | "food_fields" - list of fields for food struct [food structs](#food-struct). |


## List Wine Styles
| URL | https://DOMAIN/api/rest/v001/wine_style |
| --- | --- |
| Method | GET |
| GET Params | "style_fields" - list of fields for wine style struct [wine style structs](#wine-style-struct). |


## Get Product By Barcode
| URL | https://DOMAIN/api/rest/v001/store/STORE ID/barcode/BARCODE |
| --- | --- |
| Method | GET |
| GET Params | List of fields for each struct [inventory structs](#base-inventory-struct). If not specified which fields to fetch for a given struct, it will contain only "id" parameter. Param name, related to a given struct, listed below struct tables. |

### Example:
```
https://DOMAIN.gettipsi.com/api/rest/v001/store/STORE ID/barcode/BARCODE?wine_fields=id,winery,region&inventory_fields=id,wine&winery_fields=id,name&region_fields=id,name,description,image_url
```

## Full Text Search
| URL | https://DOMAIN/api/rest/v001/fts/ |
| --- | --- |
| Method | GET |
| GET Params | query - search query, [fts struct fields](#fts-struct) |

### Example

```javascript
// Search drinks and wines
// GET /api/rest/v001/fts/?pro_rating_fields=shortcut%2Crating&amp;wine_fields=id%2Cpro_rating%2Cwinery&amp;fts_fields=rank%2Cwine%2Cdrink&amp;query=validated&amp;winery_fields=id%2Cname
// Data: {"pro_rating_fields": "shortcut,rating", "wine_fields": "id,pro_rating,winery", "fts_fields": "rank,wine,drink", "query": "validated", "winery_fields": "id,name"}
// Response code: 200
({
     "count": 2,
     "next": null,
     "previous": null,
     "results": [
         {
             "drink": null,
             "rank": "0.638323",
             "wine": {
                 "id": 19,
                 "pro_rating": [],
                 "winery": {
                     "id": 18,
                     "name": "Caymus"
                 }
             }
         },
         {
             "drink": {
                 "id": 20
             },
             "rank": "0.0607927",
             "wine": null
         }
     ]
 }

// Only wines
// /api/rest/v001/fts/?pro_rating_fields=shortcut%2Crating&amp;wine_fields=id%2Cpro_rating%2Cwinery&amp;query=validated&amp;fts_fields=rank%2Cwine&amp;winery_fields=id%2Cname
// Data: {"pro_rating_fields": "shortcut,rating", "wine_fields": "id,pro_rating,winery", "query": "validated", "fts_fields": "rank,wine", "winery_fields": "id,name"}
// Response code: 200

 {
     "count": 1,
     "next": null,
     "previous": null,
     "results": [
         {
             "rank": "0.638323",
             "wine": {
                 "id": 19,
                 "pro_rating": [],
                 "winery": {
                     "id": 18,
                     "name": "Caymus"
                 }
             }
         }
     ]
 }

// Only drinks
// /api/rest/v001/fts/?pro_rating_fields=shortcut%2Crating&amp;wine_fields=id%2Cpro_rating%2Cwinery&amp;query=validated&amp;fts_fields=rank%2Cdrink&amp;winery_fields=id%2Cname
// Data: {"pro_rating_fields": "shortcut,rating", "wine_fields": "id,pro_rating,winery", "query": "validated", "fts_fields": "rank,drink", "winery_fields": "id,name"}
// Response code: 200

({
     "count": 1,
     "next": null,
     "previous": null,
     "results": [
         {
             "drink": {
                 "id": 20
             },
             "rank": "0.0607927"
         }
     ]
 }
```

# Structs
## Base Inventory Struct
| Field Name | Description |
| --- | --- |
| id | Inventory id, int |
| price | Item price, float |
| special_price | Special price, float |
| special_price_on | Special price enabled/disable, bool |
| special_price_amount | Amount of items required to purchase to get special price, int |
| barcodes | List of barcodes (list of strings) - each barcode has to be unique per retail store, otherwise backend will report validation error |
| proof | Alcohol proof, float |
| abv | Alcohol by value, float |
| bottle_size | Bottle size in ml, int |
| in_stock | Amount of items in stock (regular price), int |
| updated | Last inventory update date and time |

Param name: **inventory_fields**


## FTS Struct

| Field Name | Description |
| --- | --- |
| rank | Matched item search rank, int |
| drink | Related drink, [struct](#drink-struct) |
| wine | Related wine, [struct](#wine-struct) |


## Wine Inventory Struct
Same as [Base Inventory Struct](#base-inventory-struct), +additional params:

| Field Name | Description |
| --- | --- |
| wine | Related wine, [struct](#wine-struct) |
| status | Matching status, str - it can be match_pending, in_progress or match_complete |

Param name: **inventory_fields**

## Drink Inventory Struct
Same as [Base Inventory Struct](#base-inventory-struct), +additional params:

| Field Name | Description |
| --- | --- |
| drink | Related drink, [struct](#drink-struct) |
| status | Matching status, str - it can be match_pending, in_progress or match_complete |

Param name: **inventory_fields**


## Wine Struct
| Field Name | Description |
| --- | --- |
| id | Tipsi wine id, int |
| name | Wine name, str |
| vintage | Wine vintage, str. Year (.e.g. "2003", "2005") or "NV" for non vintage |
| winery | [struct](#winery-struct) |
| vineyard | [struct](#vineyard-struct) |
| designation | [struct](#designation-struct) |
| varietals | list of [structs](#varietal-struct) |
| country | [struct](#country-struct) |
| region | [struct](#region-struct) |
| sub_regions | list of [structs](#sub-region-struct) |
| type | regular, fortified, sparkling, dessert or offdry |
| color | Color density, int. Values from 1 to 3 - White, 4 - Rose, from 5 to 7 - Red |
| label_url | Url of wine label image, str |
| wine_url | URL to wine producer or re-seller |
| pro_rating | list of [structs](#pro-rating-struct) |
| style_scoring | list of [structs](#wine-style-score-struct) |
| food_scoring | list of [structs](#food-score-struct) |
| winemakers_note | A note from wine producer |


Param name: **wine_fields**

## Winery Struct
| Field Name | Description |
| --- | --- |
| id | Winery id, int |
| name | Winery name, str |

Param name: **winery_fields**

## Vineyard Struct
| Field Name | Description |
| --- | --- |
| id | Vineyard id, int |
| name | name, str |
| description | description, str |

Param name: **vineyard_fields**

## Designation Struct
Field Name | Description |
| --- | --- |
| id | Designation id, int |
| name | Designation name, str |
| description | Designation description, str |

Param name: **designation_fields**

## Varietal Struct
| Field Name | Description |
| --- | --- |
| id | Varietal id, int |
| name | Varietal name, str |
| description | Varietal description, str |

Param name: **varietal_fields**

## Country Struct
| Field Name | Description |
| --- | --- |
| id | Country id, int |
| name | Country name, str |
| description | Country description, str |
| image_url | Map image url |

Param name: **country_fields**

## Region Struct
| Field Name | Description |
| --- | --- |
| id | Region id, int |
| name | Region name, str |
| description | Region description, str |
| image_url | Map image url |

Param name: **region_fields**

## Sub Region Struct
| Field Name | Description |
| --- | --- |
| id | Sub Region id, int |
| name | Sub Region name, str |
| description | Sub Region description, str |
| image_url | Map image url |

Param name: **sub_region_fields**

## Pro Rating Struct
| Field Name | Description |
| --- | --- |
| name | Rating long name, str |
| shortcut | shortcut, str |
| rating | Rating value, int: 0..100 |
| rating_description | Description of the given rating value |

Param name: **pro_rating_fields**

## Wine Style Score Struct
| Field Name | Description |
| --- | --- |
| id | Wine style id, int |
| name | Wine style name, str |
| score | Wine style score, int |

Param name: **style_fields**

## Food Score Struct
| Field Name | Description |
| --- | --- |
| id | Food id, int |
| score | Food score, int |

Param name: **food_fields**

## Drink Struct

| Field Name | Description |
| --- | --- |
| id | Drink id, int |
| name | Drink name, str |
| description | Drink description, str |
| drink_type | beer, spirits, cocktail or other |
| producer | Drink producer, [struct](#drink-producer-struct) |
| country | Country, [struct](#country-struct) |
| label_url | Drink label url |

Param name: **drink_fields**

## Drink Producer Struct

| Field Name | Description |
| --- | --- |
| id | Drink producer id, int |
| name | Drink name, str |
| description | Producer description, str |

Param name: **producer_fields**

## Food Struct

| Field Name | Description |
| --- | --- |
| id | Food id, int |
| meal | Meal name, str |
| preparation | Preparation name, str (or None) |
| image_url | Food image (unselected) |
| image_selected_url | Food image (selected) |

## Wine Style Struct

| Field Name | Description |
| --- | --- |
| id | Wine style id, int |
| name | Style name, str |

Param name: **style_fields**
