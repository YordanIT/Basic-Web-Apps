using SMS.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models
{
    public class Product
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.ProductNameMaxLength)]
        public string Name { get; set; }

        [Range((double)Const.PriceMinValue, (double)Const.PriceMaxValue)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}

