//c# dynamic JSON deserialization
/*
Input
{
  "page": 1,
  "total_pages": 8,
  "total_entries": 74,
  "q": "muse",
  "albums": [
    {
      "name": "Muse",
      "permalink": "Muse",
      "cover_image_url": "http://image.kazaa.com/images/69/01672812 1569/Yaron_Herman_Trio/Muse/Yaron_Herman_Trio-Muse_1.jpg",
      "id": 93098,
      "artist_name": "Yaron Herman Trio"
    },
    {
      "name": "Muse",
      "permalink": "Muse",
      "cover_image_url": "htt p://image.kazaa.com/images/54/888880301154/Candy_Lo/Muse/Candy_Lo-Muse_1.jpg",
      "i d": 102702,
      "artist_name": "\u76e7\u5de7\u97f3"
    }
   ]
  }
*/
dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
var page = x.page;
var total_pages = x.total_pages
var albums = x.albums;
foreach(var album in albums)
{
    var albumName = album.name;

    // Access album data;
}
