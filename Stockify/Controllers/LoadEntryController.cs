using System;
using Microsoft.AspNetCore.Mvc;
using Stockify.Models;

namespace Stockify.Controllers
{
	public class LoadEntryController : Controller
    {
        private readonly LoadDbContext _lcontext;
        private readonly LoadEntryDbContext _lecontext;
        private readonly ProductDbContext _pcontext;
        private readonly OrganisationDbContext _ocontext;

        public LoadEntryController(LoadDbContext context1, ProductDbContext context2, OrganisationDbContext context3, LoadEntryDbContext context4)
        {
            _lcontext = context1;
            _pcontext = context2;
            _ocontext = context3;
            _lecontext = context4;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: LoadEntry/Create
        [HttpGet]
        public async Task<IActionResult> CreateLoadEntry(string id)
        {
            var load = await _lcontext.Loads.FindAsync(id);

            if (load == null)
            {
                return NotFound();
            }

            var org = await _ocontext.Organisations.FindAsync(load.OrgId);

            if (org == null)
            {
                return NotFound();
            }

            List<Product> productlist = _pcontext.Products.Where(l => l.OrgId == org.OrgId).ToList();

            var viewModel = new LoadEntryViewModel
            {
                LoadId = load.LoadId,
                LoadName = load.Name,
                LoadGroup = load.LoadGroup,
                LoadOrgId = org.OrgId,
                LoadOrgName = org.Name,
                ProductList = productlist
            };

            ViewBag.NewLoadEntry = true;

            return View("NewLoadEntry", viewModel);
        }


        // POST: LoadEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoadEntry(LoadEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var loadentry = new LoadEntry
                {
                    LoadEntryId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    LoadId = viewModel.LoadId,
                    OrgId = viewModel.LoadOrgId,
                    ProductId = viewModel.LoadEntryProductId,
                    Weight = viewModel.Weight,
                    Quantity = viewModel.Quantity,
                    Width = viewModel.Width,
                    Height = viewModel.Height,
                    CreationDate = viewModel.CreationDate
                };

                _lecontext.Add(loadentry);
                await _lecontext.SaveChangesAsync();
            }

            return View("NewLoadEntry", viewModel);
        }
    }
}

