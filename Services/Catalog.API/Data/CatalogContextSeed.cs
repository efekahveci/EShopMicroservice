using Bogus;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProduct = productCollection.Find(p => true).Any();
        if (!existProduct)
        {
            productCollection.InsertManyAsync(GetPreconfiguredProducts());
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new Faker<Product>()
                         .RuleFor(product => product.Id, x => x.Random.Number(10, 20).ToString())
                         .RuleFor(product => product.Name, x => x.Commerce.Product())
                         .RuleFor(product => product.Summary, x => x.Commerce.ProductDescription())
                         .RuleFor(product => product.Description, x => x.Commerce.ProductDescription())
                         .RuleFor(product => product.Price, x => x.Random.Number(0, 30))
                         .RuleFor(product => product.Category, x => x.Commerce.Product()).Generate(100);
}
