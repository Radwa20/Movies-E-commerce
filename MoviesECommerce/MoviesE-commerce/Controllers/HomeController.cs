﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesE_commerce.DBContect;
using MoviesE_commerce.Models;
using System.Diagnostics;

namespace MoviesE_commerce.Controllers
{
    public class HomeController : Controller
    {


        private readonly MovieE_CommerceDbContext _db;

        public HomeController(MovieE_CommerceDbContext db)
        {
            _db = db;
        }


        public IActionResult MoviesType(MovieCategory category)
        {

            var movies = _db.Movies.Where(m => m.MovieCategory == category).ToList();
            ViewData["Category"] = category;

            return View(movies);
        }
        public IActionResult Index()
        {

            var movies = _db.Movies.ToList();
            return View(movies);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Movies()
        {
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            //ViewBag.UserId = userId;
            var movies = _db.Movies.ToList();
            return View(movies);
        }
        public IActionResult OneMovies(int id)
        {
            var movie = _db.Movies
                .Include(m => m.Producer)
                .Include(m => m.ActorMovies)
                    .ThenInclude(am => am.Actor)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult Producer(int id)
        {
            var producer = _db.Producers
                .Include(am => am.Movies)
                .FirstOrDefault(p => p.Id == id);

            if (producer == null)
            {
                return NotFound(); // Return a 404 Not Found if the producer with the specified ID is not found
            }
            return View(producer);
        }

        public IActionResult Actor(int id)
        {

            var actor = _db.Actors
                .Include(am => am.ActorMovies)
                    .ThenInclude(am => am.Movie)
                .FirstOrDefault(a => a.Id == id);

            if (actor == null)
            {
                return NotFound(); // Return a 404 Not Found if the actor with the specified ID is not found
            }

            return View(actor);
        }
    }
    


    

}