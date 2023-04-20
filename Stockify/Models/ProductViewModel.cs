using System;
using System.Xml.Linq;

namespace Stockify.Models
{
	public class ProductViewModel
	{
        public string ProductOrgId { get; set; }
        public string OrgName { get; set; }
        public string Name { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal WeightPerUnit { get; set; }
        public decimal CostPer100Sqft { get; set; }
        public decimal WeightPer100Sqft { get; set; }
        public DateTime CreationDate { get; set; }

        //public string ProductId { get; set; }
        //public Product product { get; set; }
        //public List<Product> ProductList { get; set; }
    }
}

