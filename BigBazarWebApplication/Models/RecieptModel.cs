using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BigBazarWebApplication.Models
{
    public class RecieptModel
    {
        [Display(Name = "Reciept Id")]
        public int RecieptId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Total Bill")]
        public int TotalBill { get; set; }

        [Display(Name = "Number of Items")]
        public int Quantity { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
