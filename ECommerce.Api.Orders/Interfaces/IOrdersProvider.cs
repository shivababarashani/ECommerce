using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
