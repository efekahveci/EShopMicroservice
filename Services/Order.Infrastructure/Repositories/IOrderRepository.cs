using Order.Domain.Entities;

namespace Order.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName);
    }
}