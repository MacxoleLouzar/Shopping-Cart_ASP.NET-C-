using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCarty.Models.ViewModel
{
    public class ItemViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }


        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Year Released")]
        [DataType(DataType.Date)]
        public DateTime YearReleased { get; set; }

        [Required]
        [RegularExpression(@"\d{1,100}", ErrorMessage = "Invalid length! <br> Quantity ")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }


        public byte[] QRImage { get; set; }

    }
}
