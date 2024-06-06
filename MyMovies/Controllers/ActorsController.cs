//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MyMovies.Data;
//using MyMovies.Models;
//using MyMovies.ViewModels;

//namespace MyMovies.Controllers
//{
//    public class ActorsController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public ActorsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Actors
//        public async Task<IActionResult> Index(
//            string sortOrder,
//            string currentFilter,
//            string searchString,
//            int? pageNumber)
//        {

//            var actorsQuery = _context.Actors.AsQueryable();

            
            
//            ViewData["CurrentSort"] = actorsQuery = actorsQuery.OrderBy(a => a.Name);
//            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? actorsQuery.OrderByDescending(a => a.Name) : string.Empty;

//            if (!string.IsNullOrEmpty(searchString))
//            {
//                pageNumber = 1;
//            }
//            else
//            {
//                searchString = currentFilter;
//            }

//            ViewData["CurrentFilter"] = searchString;


//            if (!string.IsNullOrEmpty(searchString))
//            {
//                actorsQuery = actorsQuery.Where(a => a.Name.Contains(searchString));
//            }

//            int pageSize = 3;
//            return View(await PaginatedList<ActorVM>.CreateAsync(actorsQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
//        }

//        // GET: Actors/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            var actors = _context.Actors.Include(m => m.MovieActors).ThenInclude(ma => ma.Movie).FirstOrDefault(x => x.Id == id);

            

//            return View();
//        }

//        // GET: MoviesController/Create
//        public ActionResult Create()
//        {
//            var actor = _context.Actors.ToList();
//            var director = _context.Directors.ToList();
//            var movieInfo = new MovieVM //don't need the rest
//            {
//                Directors = director,
//                Actors = actor,
//            };

//            return View(movieInfo);
//        }
//        // POST: MoviesController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken] //protect protect against fraud
//        public async Task<ActionResult> Create([Bind("Id, Title, Year, Rated, Release, Runtime, Genre, Writer, Plot, Language, Country, Awards, Poster, Metascore,imdbRating, imdbVotes, Boxoffice, Directors, Actors, SelectedActors, SelectedDirectors")] MovieVM NewMovieVM)
//        {
//            var LastMovieId = _context.Movies.ToList().Last().Id;
//            var MAList = new List<MovieActor>();
//            var MDList = new List<MovieDirector>();

//            try
//            {
//                if (true)//check if entry is valid
//                {
//                    int i = LastMovieId + 1;
//                    NewMovieVM.Year = (NewMovieVM.Release).Year;

//                    var ASelect = NewMovieVM.SelectedActors.ToList();
//                    var DSelect = NewMovieVM.SelectedDirectors.ToList();

//                    ASelect.ForEach(x => MAList.Add(new MovieActor { ActorId = x, MovieId = i })); //go through all item in ASelect and make new MovieActor 
//                    DSelect.ForEach(x => MDList.Add(new MovieDirector { DirectorId = x, MovieId = i }));

//                    _context.MovieActors.AddRange(MAList);
//                    _context.MovieDirectors.AddRange(MDList);
//                    _context.Movies.Add(new Movie
//                    {
//                        Id = i,
//                        Title = NewMovieVM.Title,
//                        Year = NewMovieVM.Year,
//                        Rated = NewMovieVM.Rated,
//                        Release = NewMovieVM.Release,
//                        Runtime = NewMovieVM.Runtime,
//                        Genre = NewMovieVM.Genre,
//                        Writer = NewMovieVM.Writer,
//                        Plot = NewMovieVM.Plot,
//                        Language = NewMovieVM.Language,
//                        Country = NewMovieVM.Country,
//                        Awards = NewMovieVM.Awards,
//                        Poster = NewMovieVM.Poster,
//                        Metascore = NewMovieVM.Metascore,
//                        imdbRating = NewMovieVM.imdbRating,
//                        imdbVotes = NewMovieVM.imdbVotes,
//                        BoxOffice = NewMovieVM.BoxOffice
//                    });




//                    await _context.SaveChangesAsync();

//                    return RedirectToAction(nameof(Index));
//                }

//            }
//            catch (DbUpdateException) //ex when database cannot process entry
//            {
//                //Log the error (uncomment ex variable name and write a log.
//                ModelState.AddModelError("", "Unable to save changes. " +
//                    "Please try again");
//            }
//            return View();
//        }

//        // GET: MoviesController/Edit/5

//        public async Task<ActionResult> Edit(int id)
//        {
//            if (id == null) return NotFound();
//            //var actor = _context.Actors.ToList(); seperate actor from movie db.
//            var movie = await _context.Movies
//                        .FirstOrDefaultAsync(m => m.Id == id);

//            var actor = await _context.Actors.ToListAsync();
//            var director = await _context.Directors.ToListAsync();


//            if (movie == null)
//            {
//                return null;
//            }

//            var movieDetail = new MovieVM
//            {
//                Id = movie.Id,
//                Title = movie.Title,
//                Year = movie.Year,
//                Rated = movie.Rated,
//                Release = movie.Release,
//                Runtime = movie.Runtime,
//                Genre = movie.Genre,
//                Writer = movie.Writer,
//                Plot = movie.Plot,
//                Language = movie.Language,
//                Country = movie.Country,
//                Awards = movie.Awards,
//                Poster = movie.Poster,
//                imdbRating = movie.imdbRating,
//                imdbVotes = movie.imdbVotes,
//                BoxOffice = movie.BoxOffice,
//                Metascore = movie.Metascore,
//                Directors = director,
//                Actors = actor
//            };

//            return View(movieDetail);
//        }

//        // POST: MoviesController/Edit/5

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(int id, [Bind("Id, Title, Year, Rated, Release, Runtime, Genre, Writer, Plot, Language, Country, Awards, Poster, Metascore,imdbRating, imdbVotes, Boxoffice, Directors, Actors, SelectedActors, SelectedDirectors")] MovieVM MovieVM)
//        {
//            var MAList = new List<MovieActor>();
//            var MDList = new List<MovieDirector>();

//            var movieinfo = await _context.Movies
//                            .Include(m => m.MovieActors)
//                            .Include(m => m.MovieDirectors)
//                            .FirstOrDefaultAsync(m => m.Id == id);

//            try
//            {
//                if (true)//check if entry is valid
//                {
//                    MovieVM.Year = (MovieVM.Release).Year;

//                    var ASelect = MovieVM.SelectedActors.ToList();
//                    var DSelect = MovieVM.SelectedDirectors.ToList();

//                    ASelect.ForEach(x => MAList.Add(new MovieActor { ActorId = x, MovieId = id })); //go through all item in ASelect and make new MovieActor 
//                    DSelect.ForEach(x => MDList.Add(new MovieDirector { DirectorId = x, MovieId = id }));


//                    var movieUpdate = _context.Movies.FirstOrDefault(x => x.Id == id);
//                    movieUpdate.Title = MovieVM.Title;
//                    movieUpdate.Year = MovieVM.Year;
//                    movieUpdate.Rated = MovieVM.Rated;
//                    movieUpdate.Release = MovieVM.Release;
//                    movieUpdate.Runtime = MovieVM.Runtime;
//                    movieUpdate.Genre = MovieVM.Genre;
//                    movieUpdate.Writer = MovieVM.Writer;
//                    movieUpdate.Plot = MovieVM.Plot;
//                    movieUpdate.Language = MovieVM.Language;
//                    movieUpdate.Country = MovieVM.Country;
//                    movieUpdate.Awards = MovieVM.Awards;
//                    movieUpdate.Poster = MovieVM.Poster;
//                    movieUpdate.Metascore = MovieVM.Metascore;
//                    movieUpdate.imdbRating = MovieVM.imdbRating;
//                    movieUpdate.imdbVotes = MovieVM.imdbVotes;
//                    movieUpdate.BoxOffice = MovieVM.BoxOffice;
//                    movieUpdate.MovieActors = MAList;
//                    movieUpdate.MovieDirectors = MDList;


//                    _context.UpdateRange(movieUpdate);

//                    await _context.SaveChangesAsync();

//                    return RedirectToAction(nameof(Index));
//                }

//            }
//            catch (DbUpdateException) //ex when database cannot process entry
//            {
//                //Log the error (uncomment ex variable name and write a log.
//                ModelState.AddModelError("", "Unable to save changes. " +
//                    "Please try again");
//            }
//            return View();
//        }
//        // GET: MoviesController/Delete/5

//        public async Task<ActionResult> Delete(int id)
//        {
//            if (id == null) return NotFound();//error404 responce

//            var movie = await _context.Movies
//            .Include(m => m.MovieDirectors)
//                .ThenInclude(md => md.Director)
//            .Include(m => m.MovieActors)
//                .ThenInclude(ma => ma.Actor)
//            .FirstOrDefaultAsync(m => m.Id == id);

//            if (movie == null)
//            {
//                return null;
//            }

//            var movieDetail = new MovieVM
//            {
//                Id = movie.Id,
//                Title = movie.Title,
//                Year = movie.Year,
//                Rated = movie.Rated,
//                Release = movie.Release,
//                Runtime = movie.Runtime,
//                Genre = movie.Genre,
//                Writer = movie.Writer,
//                Plot = movie.Plot,
//                Language = movie.Language,
//                Country = movie.Country,
//                Awards = movie.Awards,
//                Poster = movie.Poster,
//                imdbRating = movie.imdbRating,
//                imdbVotes = movie.imdbVotes,
//                BoxOffice = movie.BoxOffice,
//                Metascore = movie.Metascore,
//                Directors = movie.MovieDirectors.ToList().Select(m => m.Director).ToList(),
//                Actors = movie.MovieActors.ToList().Select(m => m.Actor).ToList()
//            };

//            return View(movieDetail);
//        }

//        //POST: MoviesController/Delete/5

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                var test = id;
//                var Movie = _context.Movies;

//                var MAs = _context.MovieActors;

//                var MDs = _context.MovieDirectors;
//                var movieDel = await _context.Movies
//            .Include(m => m.MovieDirectors)
//            .Include(m => m.MovieActors)
//            .FirstOrDefaultAsync(m => m.Id == id);


//                _context.RemoveRange(movieDel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch (DbUpdateException /* ex */)
//            {
//                //Log the error (uncomment ex variable name and write a log.)
//                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
//            }

//        }

//        //        // GET: Actors/Create

//        //        public IActionResult Create()
//        //        {
//        //            return View();
//        //        }

//        //        // POST: Actors/Create
//        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //        [HttpPost]
//        //        [ValidateAntiForgeryToken]
//        //        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age")] Actor actor)
//        //        {
//        //            if (ModelState.IsValid)
//        //            {
//        //                _context.Add(actor);
//        //                await _context.SaveChangesAsync();
//        //                return RedirectToAction(nameof(Index));
//        //            }
//        //            return View(actor);
//        //        }

//        //        // GET: Actors/Edit/5
//        //        public async Task<IActionResult> Edit(int? id)
//        //        {
//        //            if (id == null)
//        //            {
//        //                return NotFound();
//        //            }

//        //            var actor = await _context.Actors.FindAsync(id);
//        //            if (actor == null)
//        //            {
//        //                return NotFound();
//        //            }
//        //            return View(actor);
//        //        }

//        //        // POST: Actors/Edit/5
//        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //        [HttpPost]
//        //        [ValidateAntiForgeryToken]
//        //        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age")] Actor actor)
//        //        {
//        //            if (id != actor.Id)
//        //            {
//        //                return NotFound();
//        //            }

//        //            if (ModelState.IsValid)
//        //            {
//        //                try
//        //                {
//        //                    _context.Update(actor);
//        //                    await _context.SaveChangesAsync();
//        //                }
//        //                catch (DbUpdateConcurrencyException)
//        //                {
//        //                    if (!ActorExists(actor.Id))
//        //                    {
//        //                        return NotFound();
//        //                    }
//        //                    else
//        //                    {
//        //                        throw;
//        //                    }
//        //                }
//        //                return RedirectToAction(nameof(Index));
//        //            }
//        //            return View(actor);
//        //        }

//        //        // GET: Actors/Delete/5
//        //        public async Task<IActionResult> Delete(int? id)
//        //        {
//        //            if (id == null)
//        //            {
//        //                return NotFound();
//        //            }

//        //            var actor = await _context.Actors
//        //                .FirstOrDefaultAsync(m => m.Id == id);
//        //            if (actor == null)
//        //            {
//        //                return NotFound();
//        //            }

//        //            return View(actor);
//        //        }

//        //        // POST: Actors/Delete/5
//        //        [HttpPost, ActionName("Delete")]
//        //        [ValidateAntiForgeryToken]
//        //        public async Task<IActionResult> DeleteConfirmed(int id)
//        //        {
//        //            var actor = await _context.Actors.FindAsync(id);
//        //            if (actor != null)
//        //            {
//        //                _context.Actors.Remove(actor);
//        //            }

//        //            await _context.SaveChangesAsync();
//        //            return RedirectToAction(nameof(Index));
//        //        }

//        //        private bool ActorExists(int id)
//        //        {
//        //            return _context.Actors.Any(e => e.Id == id);
//        //        }
//    }
//}
