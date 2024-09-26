using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Enums.ProductEnum;
using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.Product
{
    public class UpdateProductDto
    {
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
       
        public int? Discount { get; set; }


       

    }
}
