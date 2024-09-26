using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.DTOs.Product;

namespace WebShopApp.Services.Interface
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProductById(int id);

        Task AddProductAsync(AddProductDto addProductDto);

        Task UpdateProduct (UpdateProductDto updateProductDto);
        void DeleteProduct(int id);
        List<ProductDto> FilterProducts(int? category , string? brand  );
    }
}
