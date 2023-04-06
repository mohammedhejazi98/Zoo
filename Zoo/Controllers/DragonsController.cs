using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers
{
    public class DragonsController : Controller
    {
        #region Private Fields

        private readonly ApplicationDbContext _context;

        #endregion Private Fields

        #region Public Constructors

        public DragonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        // GET: Dragons/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }

        // POST: Dragons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Id,Name,RoomId,FavouriteFood")] Dragon dragon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dragon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", dragon.RoomId);
            return View(dragon);
        }

        // GET: Dragons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dragons == null)
            {
                return NotFound();
            }

            var dragon = await _context.Dragons
                .Include(d => d.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dragon == null)
            {
                return NotFound();
            }

            return View(dragon);
        }

        // POST: Dragons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dragons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Dragons'  is null.");
            }
            var dragon = await _context.Dragons.FindAsync(id);
            if (dragon != null)
            {
                _context.Dragons.Remove(dragon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Dragons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dragons == null)
            {
                return NotFound();
            }

            var dragon = await _context.Dragons
                .Include(d => d.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dragon == null)
            {
                return NotFound();
            }

            return View(dragon);
        }

        // GET: Dragons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dragons == null)
            {
                return NotFound();
            }

            var dragon = await _context.Dragons.FindAsync(id);
            if (dragon == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", dragon.RoomId);
            return View(dragon);
        }

        // POST: Dragons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Age,Id,Name,RoomId,FavouriteFood")] Dragon dragon)
        {
            if (id != dragon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dragon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DragonExists(dragon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", dragon.RoomId);
            return View(dragon);
        }

        // GET: Dragons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dragons.Include(d => d.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        #endregion Public Methods

        #region Private Methods

        private bool DragonExists(int id)
        {
            return (_context.Dragons?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion Private Methods
    }
}