using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<ActionResult> Index() 
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return NotFound();//error404 responce

            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);//search db for id to select

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
        public async Task<ActionResult> Create([Bind("Id, Title, ReleaseDate, Genre, Rating ")] Movie Movie)
        {
            try
            {
                if (ModelState.IsValid)//check if entry is valid
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
        public ActionResult Edit(int id, [Bind("Id, Title, ReleaseDate, Genre, Rating ")] Movie Movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(Movie);

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
