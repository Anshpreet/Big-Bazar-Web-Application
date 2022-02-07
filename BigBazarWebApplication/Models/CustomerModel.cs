using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBazarWebApplication.Models 
{
    public  class CustomerModel 
    {
        //public Customer()
        //{
        //    Purchases = new HashSet<Purchase>();
        //    Reciepts = new HashSet<Reciept>();
        //}

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmailId { get; set; }

        [Column(TypeName = "bigint")]
        public long PhoneNo { get; set; }

        //public virtual ICollection<Purchase> Purchases { get; set; }
        //public virtual ICollection<Reciept> Reciepts { get; set; }
    }
}
