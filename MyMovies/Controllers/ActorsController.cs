using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMovies.Data;
using MyMovies.Models;
using MyMovies.ViewModels;

namespace MyMovies.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            var actorsQuery = _context.Actors.Include(a => a.MovieActors).ThenInclude(ma => ma.Movie).AsQueryable();
            
            


            ViewData["CurrentSort"] = actorsQuery = actorsQuery.OrderBy(a => a.Name);
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? actorsQuery.OrderByDescending(a => a.Name) : string.Empty;

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            if (!string.IsNullOrEmpty(searchString))
            {
                actorsQuery = actorsQuery.Where(a => a.Name.Contains(searchString));
            }

            int pageSize = 10;
            return View(await PaginatedList<Actor>.CreateAsync(actorsQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();//error 404

            var actors = _context.Actors.Include(m => m.MovieActors).ThenInclude(ma => ma.Movie).FirstOrDefault(x => x.Id == id);

            if(actors == null) return NotFound();

            var actorDetail = new ActorVM
            {
                Id = actors.Id,
                Name = actors.Name,
                Movies = actors.MovieActors.ToList().Select(a => a.Movie).ToList()
            };
           

            return View(actorDetail);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {
            var movies = _context.Movies.ToList(); //eager loading (sending data to html so this is needed)
            var actorInfo = new ActorVM //don't need the rest
            {
                Movies = movies
            };

            return View(actorInfo);
        }
        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //protect protect against fraud
        public async Task<ActionResult> Create([Bind("Id, Name, movies, SelectedMovies")] ActorVM NewActorVM)
        {
            var LastActorId = _context.Actors.ToList().Max(x => x.Id);
            var MAList = new List<MovieActor>();
        

            try
            {
                if (true)//check if entry is valid
                {
                    int i = LastActorId + 1;


                    var ASelect = NewActorVM.SelectedMovies.ToList();

                    ASelect.ForEach(x => MAList.Add(new MovieActor { ActorId = i, MovieId = x })); //go through all item in ASelect and make new MovieActor 

                   _context.MovieActors.AddRange(MAList);
                    _context.Actors.Add(new Actor
                    {
                        Id = i,
                        Name = NewActorVM.Name
                    });


                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException) //ex when database cannot process entry
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Please try again");
            }
            return View();
        }

        // GET: MoviesController/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            
            var actor = await _context.Actors
                        .FirstOrDefaultAsync(m => m.Id == id);

            var movie = await _context.Movies.ToListAsync();


            if (actor == null)
            {
                return null;
            }

            var actorDetail = new ActorVM
            {
                Id = id,
                Name = actor.Name,
                Movies = movie
            };

            return View(actorDetail);
        }

        // POST: MoviesController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id, Name, Movies, SelectedMovies")] ActorVM ActorVM)
        {
            var MAList = new List<MovieActor>();

            var actorInfo = await _context.Actors
                            .Include(m => m.MovieActors)
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (ActorVM.SelectedMovies == null || ActorVM.Name == null)
            {
                return RedirectToAction(nameof(Edit));
            }
            try
            {
                if (true)//check if entry is valid
                {

                    var ASelect = ActorVM.SelectedMovies.ToList();

                    ASelect.ForEach(x => MAList.Add(new MovieActor { ActorId = id, MovieId = x })); //go through all item in ASelect and make new MovieActor 


                    var actorUpdate = _context.Actors.FirstOrDefault(x => x.Id == id);
                    actorUpdate.Name = ActorVM.Name;
                    actorUpdate.MovieActors = MAList;


                    _context.UpdateRange(actorUpdate);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException) //ex when database cannot process entry
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Please try again");
            }
            return View();
        }
        // GET: MoviesController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null) return NotFound();//error404 responce

            var actor = await _context.Actors
            .Include(m => m.MovieActors)
            .ThenInclude(ma => ma.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (actor == null)
            {
                return null;
            }

            var ActorDetail = new ActorVM
            {
                Name = actor.Name,
                Movies = actor.MovieActors.ToList().Select(a => a.Movie).ToList()
            };

            return View(ActorDetail);
        }

        //POST: MoviesController/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var actorDel = await _context.Actors
                                    .Include(m => m.MovieActors)
                                    .FirstOrDefaultAsync(m => m.Id == id);


                _context.RemoveRange(actorDel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }

        }

    }
}
