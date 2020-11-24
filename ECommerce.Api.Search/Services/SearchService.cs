using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;

        public SearchService(IOrderService orderService,IProductsService productsService, ICustomersService customersService)
        {
            _orderService = orderService;
            _productsService = productsService;
            _customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchReslt)> SearchAsync(int customerId)
        {
            var customeResult = await _customersService.GetCustomerAsync(customerId);
            var orderResult = await _orderService.GetOrderAsync(customerId);
            var productResult = await _productsService.GetProductsAsync();
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName =productResult.IsSuccess?
                            productResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name
                            :"Product information is not available";
                    }
                }
                var result = new
                {
                    Customer=customeResult.IsSuccess ?
                    customeResult.Customer:
                    new {name= "Customer information is not available" },

                    Orders = orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
