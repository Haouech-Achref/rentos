using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rentos.Data;
using Rentos.Models;
using Rentos.Models.ApplicationUserViewModels;

namespace Rentos.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;


        public ApplicationUsersController(ApplicationDbContext context,
            UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            var appusers = new List<ApplicationUserView>();
            var users = await _context.ApplicationUser.ToListAsync();

            foreach (var item in users)
            {
                var roles = await _usermanager.GetRolesAsync(item);
                appusers.Add(new ApplicationUserView() { Email = item.Email,
                    FirstName = item.FirstName,
                    Id = item.Id,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Roles = roles
                    
                });
            }

            return View(appusers);
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            var user = new ApplicationUserView(applicationUser)
            {
                Roles = await _usermanager.GetRolesAsync(applicationUser)
            };
            return View(user);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(new ApplicationUserView(applicationUser));
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Roles")] ApplicationUserView model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.ApplicationUser.Find(model.Id);

                    var email = user.Email;
                    if (model.Email != email)
                    {
                        var setEmailResult = await _usermanager.SetEmailAsync(user, model.Email);
                        if (!setEmailResult.Succeeded)
                        {
                            throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                        }
                    }

                    var phoneNumber = user.PhoneNumber;
                    if (model.PhoneNumber != phoneNumber)
                    {
                        var setPhoneResult = await _usermanager.SetPhoneNumberAsync(user, model.PhoneNumber);
                        if (!setPhoneResult.Succeeded)
                        {
                            throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                        }
                    }
                    var firstname = user.FirstName;
                    if (model.FirstName != firstname)
                    {
                        user.FirstName = model.FirstName;
                        var setFirstNameResult = await _usermanager.UpdateAsync(user);
                        if (!setFirstNameResult.Succeeded)
                        {
                            throw new ApplicationException($"Unexpected error occurred setting first name for user with ID '{user.Id}'.");
                        }
                    }

                    var lastname = user.LastName;
                    if (model.LastName != lastname)
                    {
                        user.LastName = model.LastName;
                        var setLastNameResult = await _usermanager.UpdateAsync(user);
                        if (!setLastNameResult.Succeeded)
                        {
                            throw new ApplicationException($"Unexpected error occurred setting last" +
                                $" name for user with ID '{user.Id}'.");
                        }
                    }
                    var currentuserroles = await _usermanager.GetRolesAsync(user);
                    await _usermanager.RemoveFromRolesAsync(user, currentuserroles );
                    List<string> modifiedUserRoles =(List<string>) model.Roles;
                    modifiedUserRoles.Add("user");
                    await _usermanager.AddToRolesAsync(user, modifiedUserRoles);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(model.Id))
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
            return View(model);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            var user = new ApplicationUserView(applicationUser);
            var roles = await _usermanager.GetRolesAsync(applicationUser);
            user.Roles = roles;
            return View(user);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
