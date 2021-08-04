using ShoppingCarty.Models.Entity;
using System;
using System.ComponentModel.DataAnnotations;


namespace ShoppingCarty.Models.ViewModel
{
    public class CartItemViewModel
    {
        [Key]
        public string CartId { get; set; }

        public string UserId { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int TotQ { get; set; }

    }
}
