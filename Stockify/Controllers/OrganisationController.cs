﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockify.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stockify.Controllers
{
    public class OrganisationController : Controller
    {
        private readonly OrganisationDbContext _ocontext;
        private readonly JobWorkDbContext _jwcontext;

        public OrganisationController(OrganisationDbContext context1, JobWorkDbContext context2)
        {
            _ocontext = context1;
            _jwcontext = context2;
        }

        // GET: Budget/Create
        public IActionResult InitDashboard(string id)
        {
            ViewBag.Dashboard = true;

            var org = _ocontext.Organisations.Find(id);

            if (org == null)
            {
                return NotFound();
            }

            List<JobWork> jobworklist = _jwcontext.JobWorks.Where(l => l.OrgId == org.OrgId).ToList();

            var viewModel = new DashboardViewModel
            {
                OrgId = org.OrgId,
                Name = org.Name,
                Type = org.Type,    
                Location = org.Location,    
                Phone = org.Phone,    
                Email = org.Email,
                JobWorkList = jobworklist
            };

            return View("Dashboard", viewModel);
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
                    Type = model.Type,
                    Location = model.Location,
                    Email = model.Email,
                    Phone = model.Phone
                };

                model.CreationDate = organisation.CreationDate;

                _ocontext.Add(organisation);
                await _ocontext.SaveChangesAsync();
            }

            return View("NewOrganisation", model);
        }

        public IActionResult ListOrganisations()
        {
            List<Organisation> organisations = _ocontext.Organisations.OrderBy(b => b.OrgId).ToList(); ;

            var model = new OrganisationViewModel
            {
                OrganisationList = organisations
            };

            // bind products to view
            return View("ViewOrganisation", model);
        }

        // GET: Budget/Create
        public IActionResult ListJobWorks(string id)
        {
            //ViewBag.Dashboard = true;

            var org = _ocontext.Organisations.Find(id);

            if (org == null)
            {
                return NotFound();
            }

            List<JobWork> jobworklist = _jwcontext.JobWorks.Where(l => l.OrgId == org.OrgId).ToList();

            var viewModel = new DashboardViewModel
            {
                OrgId = org.OrgId,
                Name = org.Name,
                Type = org.Type,
                Location = org.Location,
                Phone = org.Phone,
                Email = org.Email,
                JobWorkList = jobworklist
            };

            return View("~/Views/JobWork/ViewJobWorks.cshtml", viewModel);
        }
    }
}

