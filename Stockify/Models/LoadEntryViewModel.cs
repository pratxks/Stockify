using System;
namespace Stockify.Models
{
	public class LoadEntryViewModel
	{
        public string LoadId { get; set; }
        public string LoadName { get; set; }
        public string LoadOrgId { get; set; }
        public string OrgName { get; set; }
        public string LoadEntryProduct { get; set; }
        public decimal Dimension { get; set; }
        public int Quantity { get; set; }
        public List<Product> ProductList { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

