using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Controllers;
using MyMovies.ViewModels;

namespace MyMovies
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //instance of db to access. 
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;//constructor for controller to get data from db
        }
        // GET: MoviesController
        public async Task<IActionResult> Index(
                                                 string sortOrder,
                                                 string currentFilter,
                                                 string searchString,
                                                 int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            ViewData["RatingSortParam"] = sortOrder == "Rating" ? "Rating_desc" : "Rating";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["RuntimeSortParam"] = sortOrder == "Runtime" ? "Runtime_desc" : "Runtime";

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var moviesQuery = _context.Movies.AsQueryable(); //clearer name

            if (!string.IsNullOrEmpty(searchString))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Title);
                    break;
                case "Date":
                    moviesQuery = moviesQuery.OrderBy(m => m.Release);
                    break;
                case "Date_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Release);
                    break;
                case "Rating":
                    moviesQuery = moviesQuery.OrderBy(m => m.imdbRating);
                    break;
                case "Rating_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.imdbRating);
                    break;
                case "Runtime":
                    moviesQuery = moviesQuery.OrderBy(m => m.Runtime);
                    break;
                case "Runtime_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Runtime);
                    break;
                default:
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
            }

            const int pageSize = 10;
            return View(await PaginatedList<Movie>.CreateAsync(moviesQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
            // GET: MoviesController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();//error404 responce

            var movie250 = await _context.Movies250
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(movie250);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {  
            //var actor = _context.Actors.ToList();
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //protect protect against fraud
        public async Task<ActionResult> Create([Bind("Id, Title, Year, Rated, Release, Runtime, Genre, Director, Writer, Actor, Plot, Language, Country, Awards, Poster, Metascore,imdbRating, imdbVotes, Boxoffice")] Movie250 Movie)
        {
            try
            {
                if (true)//check if entry is valid
                {
                    _context.Add(Movie);//add new entry to move
                    await _context.SaveChangesAsync();//save chagnes in background
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
        
        //public ActionResult Edit(int id) 
        //{
        //    //var actor = _context.Actors.ToList(); seperate actor from movie db.
        //    var movie = _context.Movies250.FirstOrDefault(x => x.Id == id);
        //    var model = new MovieActorVM()
        //    {
        //        Actors = actor,
        //        Id = movie.Id,
        //        Title = movie.Title,
        //        ReleaseDate = movie.ReleaseDate,
        //        Genre = movie.Genre,
        //        Rating = movie.Rating
        //    };
        //    return View(model);
        //}

        // POST: MoviesController/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id, Title, Year, Rated, Release, Runtime, Genre, Director, Writer, Actor, Plot, Language, Country, Awards, Poster, Metascore,imdbRating, imdbVotes, Boxoffice")] Movie250 Movie)
        {
            if (id != Movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            return View(Movie);
        }
        // GET: MoviesController/Delete/5
        
        public ActionResult Delete(int id) //need to display information thus need var movie to retreive information
        {
           // var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return View();
        }

        // POST: MoviesController/Delete/5
        
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        Movie MovieToDelete = new() { Id = id }; //create new movie type reference
        //        _context.Entry(MovieToDelete).State = EntityState.Deleted;
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (DbUpdateException /* ex */)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.)
        //        return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
        //    }

        //}
    }
}
