using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCarty.Models.Entity
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime YearReleased { get; set; }

        public int TotQuantity { get; set; }

        public decimal ItemPrice { get; set; }

    }
}
