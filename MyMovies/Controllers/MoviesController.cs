using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Controllers;

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
                    moviesQuery = moviesQuery.OrderBy(m => m.ReleaseDate);
                    break;
                case "Date_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.ReleaseDate);
                    break;
                case "Rating":
                    moviesQuery = moviesQuery.OrderBy(m => m.Rating);
                    break;
                case "Rating_desc":
                    moviesQuery = moviesQuery.OrderByDescending(m => m.Rating);
                    break;
                default:
                    moviesQuery = moviesQuery.OrderBy(m => m.Title);
                    break;
            }

            const int pageSize = 3;
            return View(await PaginatedList<Movie>.CreateAsync(moviesQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
            // GET: MoviesController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();//error404 responce

            var movie = await _context.Movies
                .Include(x => x.Actors)
                .FirstOrDefaultAsync(x => x.Id == id);//search db for id to select

            return View(movie);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {  
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //protect protect against fraud
        public async Task<ActionResult> Create([Bind("Id, Title, ReleaseDate, Genre, Rating")] Movie Movie)
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
        
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: MoviesController/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id, Title, ReleaseDate, Genre, Rating ")] Movie Movie)
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
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return View(movie);
        }

        // POST: MoviesController/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Movie MovieToDelete = new() { Id = id }; //create new movie type reference
                _context.Entry(MovieToDelete).State = EntityState.Deleted;
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
