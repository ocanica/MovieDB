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
            return View(GetAllMovies(searchQuery));

        }

        public ActionResult ViewDetails(string imdbID)
        {
            return PartialView(GetMovieDetails(imdbID));
        }

        IEnumerable<UserMovy> GetUserMovies()
        {
            using (DBModel db = new DBModel())
            {
                return db.UserMovies.ToList<UserMovy>();
            }
        }

        IEnumerable<omdbModel> GetAllMovies(string searchQuery)
        {
            using (WebClient webClient = new WebClient())
            {
                var jsonData = webClient.DownloadString($"http://www.omdbapi.com/?apikey=75f14f13&s={searchQuery}&type=movie");
                var jsonResponse = JObject.Parse(jsonData).SelectToken("Search").ToObject<IEnumerable<omdbModel>>();
                return jsonResponse;
            }
        }

        public imdbModel GetMovieDetails(string imdbID)
        {
            using (WebClient webClient = new WebClient())
            {
                var jsonData = webClient.DownloadString($"http://www.omdbapi.com/?apikey=75f14f13&i={imdbID}");
                var jsonResponse = JsonConvert.DeserializeObject<imdbModel>(jsonData);
                return jsonResponse;
            }
        }

        public void AddMovie(string imdbID)
        {
            UserMovy userMovie = new UserMovy();
            imdbModel imdbMovie = new imdbModel();

            imdbMovie = GetMovieDetails(imdbID);

            userMovie.imdbID = imdbMovie.imdbID;
            userMovie.Title = imdbMovie.Title;
            userMovie.Genre = imdbMovie.Genre;

            using (DBModel db = new DBModel())
            {
                db.UserMovies.Add(userMovie);
                db.SaveChanges();
            }
        }

    }
}