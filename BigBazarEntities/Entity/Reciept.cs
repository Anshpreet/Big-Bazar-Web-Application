using System;
using System.Collections.Generic;

#nullable disable

namespace BigBazarEntities.Entity
{
    public partial class Reciept
    {
        public int RecieptId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int TotalBill { get; set; }
        public int Quantity { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
