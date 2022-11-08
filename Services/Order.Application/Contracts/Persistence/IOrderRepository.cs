using Order.Domain.Entities;

namespace Order.Application.Contracts.Persistence;

public interface IOrderRepository : IAsyncRepository<OrderModel>
{
    Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName);
}
