using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockify.Models;

namespace Stockify.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _pcontext;
        private readonly OrganisationDbContext _ocontext;

        public ProductController(ProductDbContext context1, OrganisationDbContext context2)
        {
            _pcontext = context1;
            _ocontext = context2;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var org = await _ocontext.Organisations.FindAsync(id);

            if (org == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductOrgId = org.OrgId,
                OrgName = org.Name
            };

            ViewBag.NewProduct = true;

            return View("CreateProduct", viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    OrgId = viewModel.ProductOrgId,
                    CostPerUnit = viewModel.CostPerUnit,
                    WeightPerUnit = viewModel.WeightPerUnit,
                    CostPer100Sqft = viewModel.CostPer100Sqft,
                    WeightPer100Sqft = viewModel.WeightPer100Sqft
                };

                _pcontext.Add(product);
                await _pcontext.SaveChangesAsync();
            }

            return View("CreateProduct", viewModel);
        }
    }
}

