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
            return PartialView(GetUserMovies());
        }

        public ActionResult ViewAllMovies(string searchQuery)
        {
            return View(GetAllMovies(searchQuery));
        }

        public ActionResult ViewDetails(string imdbID)
        {
            return PartialView(GetMovieDetails(imdbID));
        }

        public void AddMovie(string imdbID)
        {
            UserMovy userMovie = new UserMovy();
            imdbModel imdbMovie = new imdbModel();

            imdbMovie = GetMovieDetails(imdbID);

            userMovie.imdbID = imdbMovie.imdbID;
            userMovie.Title = imdbMovie.Title;
            userMovie.Genre = imdbMovie.Genre;
            userMovie.Poster = imdbMovie.Poster;

            using (DBModel db = new DBModel())
            {
                db.UserMovies.Add(userMovie);
                db.SaveChanges();
            }
        }

        public void RemoveMovie(string imdbID)
        {
            using (DBModel db = new DBModel())
            {
                db.UserMovies.Remove(db.UserMovies.SingleOrDefault(x => x.imdbID == imdbID));
                db.SaveChanges();
            }
        }

        public bool ToggleFavourite(string imdbID, string favouriteBool)
        {
            bool boolean = bool.Parse(favouriteBool);

            using (DBModel db = new DBModel())
            {
                var root = db.UserMovies.SingleOrDefault(x => x.imdbID == imdbID);
                var query = db.UserMovies.SingleOrDefault(x => x.imdbID == imdbID).Favourite;

                if (boolean == query)
                {
                    root.Favourite = !boolean;
                } else if (boolean != query)
                {
                    root.Favourite = !query;
                }

                db.SaveChanges();
                return (bool)root.Favourite;
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

        IEnumerable<UserMovy> GetUserMovies()
        {
            using (DBModel db = new DBModel())
            {
                return db.UserMovies.ToList<UserMovy>();
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

    }
}