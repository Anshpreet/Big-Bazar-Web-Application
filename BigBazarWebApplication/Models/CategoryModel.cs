using System;
using System.Collections.Generic;



namespace BigBazarWebApplication.Models
{
    public class CategoryModel
    {
        //public Category()
        //{
        //    Products = new HashSet<Product>();
        //}

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

       // public virtual ICollection<Product> Products { get; set; }
    }
}
