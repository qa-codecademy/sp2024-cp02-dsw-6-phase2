using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopApp.Domain.Enums.ProductEnum;

namespace WebShopApp.Domain.Models
{
    public class Productt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Brand { get; set; }

        public int QuantityAvailable { get; set; }


        public decimal ShippingCost { get; set; }

        public int ShippingTime { get; set; }

        public string OriginalImagePath { get; set; }
        public string SavedImagePath { get; set; }
        public int? Discount { get; set; }


        
    }
}
