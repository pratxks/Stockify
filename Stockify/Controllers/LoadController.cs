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
    public class LoadController : Controller
    {
        private readonly LoadDbContext _lcontext;
        private readonly OrganisationDbContext _ocontext;

        public LoadController(LoadDbContext context1, OrganisationDbContext context2)
        {
            _lcontext = context1;
            _ocontext = context2;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: Load/Create
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var org = await _ocontext.Organisations.FindAsync(id);

            if (org == null)
            {
                return NotFound();
            }

            var viewModel = new LoadViewModel
            {
                LoadId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                LoadOrgId = org.OrgId,
                OrgName = org.Name
            };

            ViewBag.NewLoad = true;

            return View("NewLoad", viewModel);
        }

        // POST: Load/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewLoad(LoadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var load = new Load
                {
                    LoadId = viewModel.LoadId,
                    Name = viewModel.Name,
                    OrgId = viewModel.LoadOrgId
                };

                _lcontext.Add(load);
                await _lcontext.SaveChangesAsync();
            }

            return View("NewLoad", viewModel);
        }
    }
}

