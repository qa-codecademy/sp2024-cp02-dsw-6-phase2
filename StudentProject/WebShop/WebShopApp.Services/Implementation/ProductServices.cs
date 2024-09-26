using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Product;
using WebShopApp.Mappers;
using WebShopApp.Services.Interface;
using WebShopApp.Shared;

namespace WebShopApp.Services.Implementation
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductServices(IProductRepository productRepository , IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
        }
        public async Task AddProductAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null)
            {
                throw new ArgumentNullException(nameof(addProductDto), "Product data cannot be null.");
            }

            // Validate required fields
            if (string.IsNullOrEmpty(addProductDto.ProductName))
            {
                throw new ArgumentException("ProductName is required.", nameof(addProductDto.ProductName));
            }
            ValidationHelper.ValidateReqiredStrings(addProductDto.ProductName, "ProductName", 50);

            if (string.IsNullOrEmpty(addProductDto.ProductDescription))
            {
                throw new ArgumentException("ProductDescription is required.", nameof(addProductDto.ProductDescription));
            }
            ValidationHelper.ValidateReqiredStrings(addProductDto.ProductDescription, "ProductDescription", 500);

            if (string.IsNullOrEmpty(addProductDto.Brand))
            {
                throw new ArgumentException("Brand is required.", nameof(addProductDto.Brand));
            }
            ValidationHelper.ValidateReqiredStrings(addProductDto.Brand, "Brand", 50);

            if (addProductDto.Price <= 0)
            {
                throw new ArgumentException("Product must have a valid price.", nameof(addProductDto.Price));
            }

            if (addProductDto.Category == null)
            {
                throw new ArgumentException("Product category is required.", nameof(addProductDto.Category));
            }

            if (addProductDto.QuantityAvailable < 0)
            {
                throw new ArgumentException("QuantityAvailable cannot be negative.", nameof(addProductDto.QuantityAvailable));
            }

            if (addProductDto.ShippingCost < 0)
            {
                throw new ArgumentException("ShippingCost cannot be negative.", nameof(addProductDto.ShippingCost));
            }

            if (addProductDto.ShippingTime < 0)
            {
                throw new ArgumentException("ShippingTime cannot be negative.", nameof(addProductDto.ShippingTime));
            }

            if (string.IsNullOrEmpty(addProductDto.OriginalImagePath))
            {
                throw new ArgumentException("OriginalImagePath is required.", nameof(addProductDto.OriginalImagePath));
            }

            // Convert DTO to Product entity
            Product newProduct = addProductDto.ToProduct();

            // Save image asynchronously and update the product's SavedImagePath
            var savedImagePath = await SaveImageFromUrlAsync(addProductDto.OriginalImagePath);

            if (savedImagePath == null)
            {
                throw new InvalidOperationException("Failed to save image.");
            }

            newProduct.SavedImagePath = savedImagePath;

            // Add the new product to the repository
           await _productRepository.Add(newProduct);
        }

        public void DeleteProduct(int id)
        {
            Product productDb = _productRepository.GetById(id);
            if (productDb == null)
            {
                throw new Exception($"Product with product id {id} not found");
            }
            _productRepository.Delete(productDb);
        }

        public List<ProductDto> FilterProducts(int? category, string? brand)
        {
            List<Product>  products = _productRepository.FilterProducts(category, brand);
            List<ProductDto> productDtos = products.Select(x => x.ToProductDto()).ToList();
            return productDtos;
        }

        public ProductDto GetProductById(int id)
        {
            Product productDb = _productRepository.GetById(id);
            if(productDb == null)
            {
                throw new Exception($"Product with product id {id} not found");
            }
            return productDb.ToProductDto();
        }

        public List<ProductDto> GetProducts()
        {
            var productsDb = _productRepository.GetAll();
            return productsDb.Select(x => x.ToProductDto()).ToList();
        }

        public async Task UpdateProduct(UpdateProductDto updateProductDto)
        {
            Product productDb = _productRepository.GetById(updateProductDto.Id);
            if(productDb == null)
            {
                throw new Exception($"Product doesnt exists");

            }

            if (string.IsNullOrEmpty(updateProductDto.ProductName))
            {
                throw new ArgumentException("ProductName is required.", nameof(updateProductDto.ProductName));
            }
            ValidationHelper.ValidateReqiredStrings(updateProductDto.ProductName, "ProductName", 50);

            if (string.IsNullOrEmpty(updateProductDto.ProductDescription))
            {
                throw new ArgumentException("ProductDescription is required.", nameof(updateProductDto.ProductDescription));
            }
            ValidationHelper.ValidateReqiredStrings(updateProductDto.ProductDescription, "ProductDescription", 500);

            if (string.IsNullOrEmpty(updateProductDto.Brand))
            {
                throw new ArgumentException("Brand is required.", nameof(updateProductDto.Brand));
            }
            ValidationHelper.ValidateReqiredStrings(updateProductDto.Brand, "Brand", 50);

            if (updateProductDto.Price <= 0)
            {
                throw new ArgumentException("Product must have a valid price.", nameof(updateProductDto.Price));
            }

            if (updateProductDto.Category == null)
            {
                throw new ArgumentException("Product category is required.", nameof(updateProductDto.Category));
            }

            if (updateProductDto.QuantityAvailable < 0)
            {
                throw new ArgumentException("QuantityAvailable cannot be negative.", nameof(updateProductDto.QuantityAvailable));
            }

            if (updateProductDto.ShippingCost < 0)
            {
                throw new ArgumentException("ShippingCost cannot be negative.", nameof(updateProductDto.ShippingCost));
            }

            if (updateProductDto.ShippingTime < 0)
            {
                throw new ArgumentException("ShippingTime cannot be negative.", nameof(updateProductDto.ShippingTime));
            }

            if (string.IsNullOrEmpty(updateProductDto.OriginalImagePath))
            {
                throw new ArgumentException("OriginalImagePath is required.", nameof(updateProductDto.OriginalImagePath));
            }
            // Save image asynchronously and update the product's SavedImagePath
            var savedImagePath = await SaveImageFromUrlAsync(updateProductDto.OriginalImagePath);

            if (savedImagePath == null)
            {
                throw new InvalidOperationException("Failed to save image.");
            }
            productDb.ProductName = updateProductDto.ProductName;
            productDb.ProductDescription = updateProductDto.ProductDescription;
            productDb.Price = updateProductDto.Price;
            productDb.Category= updateProductDto.Category;
            productDb.Brand = updateProductDto.Brand;   
            productDb.QuantityAvailable= updateProductDto.QuantityAvailable;
            productDb.ShippingCost= updateProductDto.ShippingCost;
            productDb.ShippingTime= updateProductDto.ShippingTime;
            productDb.OriginalImagePath = savedImagePath;
            productDb.Discount= updateProductDto.Discount;

            await _productRepository.Update(productDb);



        }












        private async Task<string> SaveImageFromUrlAsync(string imageUrl)
        {
            try
            {
                // Generate a unique file name for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(new Uri(imageUrl).AbsolutePath);

                // Define the path to save the image in the wwwroot/images folder
                var savePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                // Create directory if it doesn't exist
                var imagesPath = Path.Combine(_environment.WebRootPath, "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                // Download the image from the URL
                using (var httpClient = new HttpClient())
                {
                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    // Save the image to the server folder
                    await System.IO.File.WriteAllBytesAsync(savePath, imageBytes);
                }

                // Return the relative path of the saved image
                return Path.Combine("images", fileName);
            }
            catch (Exception ex)
            {
                // Log exception in production (e.g., use a logging framework)
                // Log.Error(ex, "Error saving image from URL");
                return null;
            }
        }
    }
}
