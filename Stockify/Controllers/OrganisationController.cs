using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockify.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stockify.Controllers
{
    public class OrganisationController : Controller
    {
        private readonly OrganisationDbContext _context;

        public OrganisationController(OrganisationDbContext context1)
        {
            _context = context1;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: Budget/Create
        public IActionResult Create()
        {
            ViewBag.NewOrganisation = true;

            return View("NewOrganisation");
        }


        // POST: Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrganisation(Organisation model)
        {
            if (ModelState.IsValid)
            {
                var organisation = new Organisation
                {
                    Name = model.Name,
                    Location = model.Location,
                    Email = model.Email,
                    Phone = model.Phone,
                };

                _context.Add(organisation);
                await _context.SaveChangesAsync();
            }

            return View("NewOrganisation", model);
        }
    }
}

