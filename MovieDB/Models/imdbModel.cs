using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDB.Models
{
    public class imdbModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Genre")]
        public string Genre { get; set; }
        [JsonProperty("Plot")]
        public string Plot { get; set; }
        [JsonProperty("Actors")]
        public string Actors { get; set; }
        [JsonProperty("Poster")]
        public string Poster { get; set; }
        [JsonProperty("imdbRating")]
        public string imdbRating { get; set; }
        [JsonProperty("imdbID")]
        public string imdbID { get; set; }
    }
}