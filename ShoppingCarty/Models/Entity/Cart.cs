using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCarty.Models.Entity
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public string UserId { get; set; }
    }
}
