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
                    Type = viewModel.ProductType,
                    OrgId = viewModel.ProductOrgId,
                    CostPerUnitWeight = viewModel.CostPerUnitWeight,
                    CostPerUnit = viewModel.CostPerUnit,
                    WeightPerUnit = viewModel.WeightPerUnit,
                    CostPer100Sqft = viewModel.CostPer100Sqft,
                    WeightPer100Sqft = viewModel.WeightPer100Sqft
                    //CostPerUnitWeight = viewModel.CostPerUnitWeightHidden,
                    //CostPerUnit = viewModel.CostPerUnitHidden,
                    //WeightPerUnit = viewModel.WeightPerUnitHidden,
                    //CostPer100Sqft = viewModel.CostPer100SqftHidden,
                    //WeightPer100Sqft = viewModel.WeightPer100SqftHidden
                };

                _pcontext.Add(product);
                await _pcontext.SaveChangesAsync();
            }

            return View("CreateProduct", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts(string id)
        {
            var org = await _ocontext.Organisations.FindAsync(id);

            if (org == null)
            {
                return NotFound();
            }

            List<Product> products = _pcontext.Products.Where(b => b.OrgId == id).ToList();

            if(products == null)
            {
                return NotFound();
            }

            var viewModel = new ProductListViewModel
            {
                OrgName = org.Name,
                ProductList = products
            };

            // bind products to view
            return View("ViewProducts", viewModel);
        }
    }
}

