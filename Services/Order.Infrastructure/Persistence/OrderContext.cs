using Microsoft.EntityFrameworkCore;
using Order.Domain.Common;
using Order.Domain.Entities;


namespace Order.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public DbSet<OrderModel> Orders { get; set; }

    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        if (Orders.Count()==0)
        {
            Orders.AddRange(GetPreconfiguredOrders());
            base.SaveChanges();
        }

    
    }



    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "efekahveci";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "efekahveci";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<OrderModel> GetPreconfiguredOrders()
    {
        return new List<OrderModel>
            {
                new OrderModel() {UserName = "efekahveci", FirstName = "Efe", LastName = "Kahveci", EmailAddress = "efekhvci3@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
    }
}
