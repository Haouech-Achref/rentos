using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rentos.Data;
using Rentos.Models;
using Rentos.Models.RentViewModels;

namespace Rentos.Controllers
{
    public class RentsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RentosContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private static Rent _rent;
        public RentsController(RentosContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _rent = new Rent();
        }

        // GET: Rents
        public IActionResult Index()
        {
            var rentList = new List<RentListViewModel>();
            foreach(var item in _context.Rent)
            {
                rentList.Add(new RentListViewModel()
                {
                    Car = _context.Car.Find(item.Car_CarId),
                    User = _applicationDbContext.ApplicationUser.Find(item.User_UserId),
                    Rent = item
                });
            }
            return View(rentList);
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .SingleOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Cars
        public IActionResult FindCars()
        {
            var manufacturers = _context.Car.Select(c => c.Manufacturer).Distinct().ToList();
            manufacturers.Insert(0, "");

            var colors = _context.Car.Select(c => c.Color).Distinct().ToList();
            colors.Insert(0, "");

            ViewBag.Manufacturers = new SelectList(manufacturers);
            ViewBag.Colors = new SelectList(colors);

            return View("Create");
        }

        //Action result for ajax call
        //Used to dynamically show car models based on selected manufacturer
        [HttpPost]
        public ActionResult GetModelByManufacturer(string manufacturer)
        {
            var models = _context.Car.Where(c => c.Manufacturer == manufacturer).Select( m => m.Model).Distinct().ToList();
            SelectList modelsList = new SelectList(models);
            return Json(modelsList);
        }
        // POST: Rents/FindCARS
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult SearchResult(RentSearchViewModel rentSearch)
        {
            if (ModelState.IsValid)
            {
                var currentRents = _context.Rent.Where(r => !(r.DropoffDate <= rentSearch.PickupDate && r.PickupDate >= rentSearch.DropoffDate));
                var unavailableCars = new List<Car>();
                foreach (var item in currentRents)
                {
                    unavailableCars.Add(_context.Car.Find(item.Car_CarId));
                }

                var availableCars = _context.Car.ToList().Except(unavailableCars);

                #region Filters
                if (!String.IsNullOrEmpty(rentSearch.ManufacturerFilter))
                {
                    availableCars = availableCars.Where(c => c.Manufacturer == rentSearch.ManufacturerFilter);
                }

                if (!String.IsNullOrEmpty(rentSearch.MinPowerFilter))
                {
                    var power = Int32.Parse(rentSearch.MinPowerFilter);
                    availableCars = availableCars.Where(c => c.Power >= power);
                }
                if (!String.IsNullOrEmpty(rentSearch.MaxPriceFilter))
                {
                    var price = Int32.Parse(rentSearch.MaxPriceFilter);
                    availableCars = availableCars.Where(c => c.Price <= price);
                }
                if (!String.IsNullOrEmpty(rentSearch.ColorFilter))
                {
                    availableCars = availableCars.Where(c => c.Color == rentSearch.ColorFilter);
                }
                if (!String.IsNullOrEmpty(rentSearch.ModelFilter))
                {
                    availableCars = availableCars.Where(c => c.Model == rentSearch.ModelFilter);
                }
                #endregion

                var rentStr = JsonConvert.SerializeObject(new Rent() { DropoffDate = rentSearch.DropoffDate, PickupDate = rentSearch.PickupDate });
                HttpContext.Session.SetString("rent", rentStr);
                ViewBag.pickup = rentSearch.PickupDate;
                ViewBag.dropoff = rentSearch.DropoffDate;
                return View("FindCars", availableCars); 
            }

            return View(rentSearch);
        }


        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent.SingleOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["Car_CarId"] = new SelectList(_context.Car, "CarId", "CarId", rent.Car_CarId);
            ViewData["User_UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", rent.User_UserId);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Car_CarId,User_UserId,PickupDate,DropoffDate")] Rent rent)
        {
            if (id != rent.Car_CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.Car_CarId))
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
            ViewData["Car_CarId"] = new SelectList(_context.Car, "CarId", "CarId", rent.Car_CarId);
            ViewData["User_UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", rent.User_UserId);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .SingleOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int RentId)
        {
            var rent = await _context.Rent.SingleOrDefaultAsync(m => m.RentId == RentId);
            _context.Rent.Remove(rent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rent.Any(e => e.Car_CarId == id);
        }

        [HttpPost]
        public async Task<ActionResult> RentOperation(int CarId)
        {
            var rentStr = HttpContext.Session.GetString("rent");
            var rent = JsonConvert.DeserializeObject<Rent>(rentStr);
            rent.Car_CarId = CarId;
            if (_signInManager.IsSignedIn(User))
            {
                rent.User_UserId = _userManager.GetUserId(User);
                _context.Add(rent);
                await _context.SaveChangesAsync();
                ViewBag.rentId = rent.RentId;
                return View("Success");
            }
            else
            {
                return Redirect("/Account/Login");
            }



        }
    }
}
