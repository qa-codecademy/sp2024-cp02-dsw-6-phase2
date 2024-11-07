using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Product;

namespace WebShopApp.Mappers
{
    public static class ProductMapper
    {
        public static Productt ToProduct(this AddProductDto addProductDto)
        {
            return new Productt
            {
                ProductName = addProductDto.ProductName,
                ProductDescription = addProductDto.ProductDescription,
                Price = addProductDto.Price,
                Category = addProductDto.Category,
                Brand = addProductDto.Brand,
                QuantityAvailable = addProductDto.QuantityAvailable,
                OriginalImagePath = addProductDto.OriginalImagePath,
                ShippingCost = addProductDto.ShippingCost,
                ShippingTime = addProductDto.ShippingTime,
                Discount = addProductDto.Discount,
                
            };
        }

        public static ProductDto ToProductDto(this Productt product)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductName= product.ProductName,
                ProductDescription= product.ProductDescription,
                Price = product.Price,
                Brand= product.Brand,
                QuantityAvailable= product.QuantityAvailable,
                ShippingTime= product.ShippingTime,
                OriginalImagePath= product.OriginalImagePath,
                ShippingCost=product.ShippingCost,
                Discount=product.Discount
                

            };
        }
    }
}
