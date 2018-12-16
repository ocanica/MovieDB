using MovieDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieDB.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewUserMovies()
        {
            return View(GetUserMovies());
        }

        public ActionResult ViewAllMovies(string searchQuery)
        {
            using (WebClient webClient = new WebClient())
            {
                var jsonData = webClient.DownloadString($"http://www.omdbapi.com/?apikey=75f14f13&s={searchQuery}");
                var jsonResponse = JObject.Parse(jsonData).SelectToken("Search").ToObject<IEnumerable<omdbModel>>();
                return View(jsonResponse);
            }
        }

        public ActionResult ViewDetails(string imdbID)
        {
            using (WebClient webClient = new WebClient())
            {
                var jsonData = webClient.DownloadString($"http://www.omdbapi.com/?apikey=75f14f13&i={imdbID}");
                var jsonResponse = JsonConvert.DeserializeObject<imdbModel>(jsonData);
                return PartialView(jsonResponse);
            }
        }

        IEnumerable<Movie> GetUserMovies()
        {
            using (DBModel db = new DBModel())
            {
                return db.Movies.ToList<Movie>();
            }
        }

        /*
        IEnumerable<omdbModel> GetAllMovies()
        {
            using (WebClient webClient = new WebClient())
            {
                var jsonData = webClient.DownloadString("http://www.omdbapi.com/?apikey=75f14f13&s=batman");
                // var objResponse = JObject.Parse(jsonData).SelectToken("Search").ToString();
                var jsonResponse = JObject.Parse(jsonData).SelectToken("Search").ToObject<IEnumerable<omdbModel>>();
                return jsonResponse;
            }
        }
        */
    }
}