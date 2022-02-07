using System;
using System.Collections.Generic;

#nullable disable

namespace BigBazarEntities.Entity
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int CustomerId { get; set; }

        //public virtual Customer Customer { get; set; }
        //public virtual Product Product { get; set; }
    }
}
