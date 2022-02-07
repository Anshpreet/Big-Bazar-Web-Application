using System;
using System.Collections.Generic;

#nullable disable

namespace BigBazarEntities.Entity
{
    public partial class Product
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

        //public virtual Category Category { get; set; }
        //public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
