namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.OrderModel>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
        }
    }
}
