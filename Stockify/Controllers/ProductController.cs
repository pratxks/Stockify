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
        public IActionResult Create(string id)
        {
            var org = _ocontext.Organisations.Find(id);

            if (org == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                OrgId = org.OrgId,
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
            //string pname = formCollection["Name"];
            //string orgid = formCollection["OrgId"];
            //string orgname = formCollection["OrgName"];
            //decimal pcostperunit = Decimal.Parse(formCollection["CostPerUnit"]);
            //decimal pwperunit = Decimal.Parse(formCollection["WeightPerUnit"]);
            //decimal pcostperunit100 = Decimal.Parse(formCollection["CostPer100Sqft"]);
            //decimal pwperunit100 = Decimal.Parse(formCollection["WeightPer100Sqft"]);

            ProductViewModel viewModel1 = new ProductViewModel();

            if (ModelState.IsValid)
            {
                viewModel1.product = new Product
                {
                    Name = viewModel.Name,
                    OrgId = viewModel.OrgId,
                    CostPerUnit = viewModel.CostPerUnit,
                    WeightPerUnit = viewModel.WeightPerUnit,
                    CostPer100Sqft = viewModel.CostPer100Sqft,
                    WeightPer100Sqft = viewModel.WeightPer100Sqft
                };

                viewModel1.Name = viewModel.Name;
                viewModel1.OrgId = viewModel.OrgId;
                viewModel1.CostPerUnit = viewModel.CostPerUnit;
                viewModel1.WeightPerUnit = viewModel.WeightPerUnit;
                viewModel1.CostPer100Sqft = viewModel.CostPer100Sqft;
                viewModel1.WeightPer100Sqft = viewModel.WeightPer100Sqft;
                //viewModel1.ProductId = product1.ProductId;
                //viewModel1.product = product1;
                viewModel1.ProductList = new List<Product>();

                _pcontext.Add(viewModel1.product);

                await _pcontext.SaveChangesAsync();
            }

            return View("CreateProduct", viewModel);
        }

        //// POST: Product/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateProduct(IFormCollection formCollection)
        //{
        //    string pname = formCollection["Name"];
        //    string orgid = formCollection["OrgId"];
        //    string orgname = formCollection["OrgName"];
        //    decimal pcostperunit = Decimal.Parse(formCollection["CostPerUnit"]);
        //    decimal pwperunit = Decimal.Parse(formCollection["WeightPerUnit"]);
        //    decimal pcostperunit100 = Decimal.Parse(formCollection["CostPer100Sqft"]);
        //    decimal pwperunit100 = Decimal.Parse(formCollection["WeightPer100Sqft"]);

        //    ProductViewModel viewModel1 = new ProductViewModel();

        //    if (ModelState.IsValid)
        //    {
        //        var product1 = new Product
        //        {
        //            Name = pname,
        //            OrgId = orgid,
        //            CostPerUnit = pcostperunit,
        //            WeightPerUnit = pwperunit,
        //            CostPer100Sqft = pcostperunit100,
        //            WeightPer100Sqft = pwperunit100
        //        };

        //        viewModel1.OrgId = orgid;
        //        viewModel1.OrgName = orgname;
        //        viewModel1.product = product1;

        //        _pcontext.Add(product1);
        //        await _pcontext.SaveChangesAsync();
        //    }

        //    return View("CreateProduct", viewModel1);
        //}
    }
}

