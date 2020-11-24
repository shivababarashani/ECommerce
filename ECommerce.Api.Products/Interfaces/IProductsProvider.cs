using ECommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductModel Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
