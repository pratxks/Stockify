using System;
using Microsoft.AspNetCore.Mvc;
using Stockify.Models;

namespace Stockify.Controllers
{
	public class JobWorkEntryController : Controller
    {
        private readonly LoadDbContext _lcontext;
        private readonly LoadEntryDbContext _lecontext;
        private readonly ProductDbContext _pcontext;
        private readonly OrganisationDbContext _ocontext;
        private readonly JobWorkDbContext _jwcontext;
        private readonly JobWorkEntryDbContext _jwecontext;

        public JobWorkEntryController(LoadDbContext context1, ProductDbContext context2, OrganisationDbContext context3, LoadEntryDbContext context4, JobWorkDbContext context5, JobWorkEntryDbContext context6)
        {
            _lcontext = context1;
            _pcontext = context2;
            _ocontext = context3;
            _lecontext = context4;
            _jwcontext = context5;
            _jwecontext = context6;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: JobWorkEntry/Create
        [HttpGet]
        public async Task<IActionResult> CreateJobWorkEntry(string id)
        {
            var jobWork = await _jwcontext.JobWorks.FindAsync(id);

            if (jobWork == null)
            {
                return NotFound();
            }

            var org = await _ocontext.Organisations.FindAsync(jobWork.OrgId);

            if (org == null)
            {
                return NotFound();
            }

            List<Product> productlist = _pcontext.Products.Where(jw => jw.OrgId == org.OrgId).ToList();

            var viewModel = new JobWorkEntryViewModel
            {
                JobWorkEntryId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                JobWorkName = jobWork.Name,
                JobWorkId = jobWork.JobWorkId,
                JobWorkEntryOrgId = org.OrgId,
                OrgName = org.Name,
                ProductList = productlist
            };

            ViewBag.NewJobWorkEntry = true;

            return View("NewJobWorkEntry", viewModel);
        }

        // POST: JobWorkEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobWorkEntry(JobWorkEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobworkentry = new JobWorkEntry
                {
                    JobWorkEntryId = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    JobWorkId = viewModel.JobWorkId,
                    OrgId = viewModel.JobWorkEntryOrgId,
                    ProductId = viewModel.JobWorkEntryProductId,
                    Weight = viewModel.Weight,
                    Quantity = viewModel.Quantity,
                    Width = viewModel.Width,
                    Height = viewModel.Height,
                };

                viewModel.CreationDate = jobworkentry.CreationDate;

                _jwecontext.Add(jobworkentry);
                await _jwecontext.SaveChangesAsync();
            }

            return View("NewJobWorkEntry", viewModel);
        }

        public async Task<IActionResult> ListJobWorkEntries(string id)
        {
            var jobwork = await _jwcontext.JobWorks.FindAsync(id);

            if (jobwork == null)
            {
                return NotFound();
            }

            var org = await _ocontext.Organisations.FindAsync(jobwork.OrgId);

            if (org == null)
            {
                return NotFound();
            }

            List<JobWorkEntry> jobworkentries = _jwecontext.JobWorkEntries.Where(l => l.JobWorkId == id).ToList();

            var productNames = new Dictionary<string, string>();
            var productTypes = new Dictionary<string, string>();

            foreach (var jobworkentry in jobworkentries)
            {
                var product = await _pcontext.Products.FindAsync(jobworkentry.ProductId);

                if (product == null)
                {
                    productNames[jobworkentry.JobWorkEntryId] = "N/A";
                    productTypes[jobworkentry.JobWorkEntryId] = "N/A";
                }
                else
                {
                    productNames[jobworkentry.JobWorkEntryId] = product.Name;
                    productTypes[jobworkentry.JobWorkEntryId] = product.Type;
                }
            }

            var model = new JobWorkEntryListViewModel
            {
                OrgName = org.Name,
                JobWorkName = jobwork.Name,
                JobWorkEntryList = jobworkentries,
                JobWorkEntryProductNames = productNames,
                JobWorkEntryProductTypes = productTypes,
            };

            // bind products to view
            return View("ViewJobWorkEntries", model);
        }

    }
}

