using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BigBazarWebApplication.Models
{
    public class ProductModel
    {
        //public Product()
        //{
        //    Purchases = new HashSet<Purchase>();
        //}

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public int ChangedQuantity { get; set; }

        //public IEnumerable<SelectListItem> CategorySelectListItems { get; set; } 

        //public virtual Category Category { get; set; }
        //public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
