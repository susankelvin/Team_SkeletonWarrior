namespace MobileVendors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Subscription
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public decimal TotalIncome { get; set; }
        
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}