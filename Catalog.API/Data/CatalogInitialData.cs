using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        { 
            return;
        }
        
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }
    
    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new()
            {
                Id = new Guid("9b1deb4d-3b7d-4bad-9bdd-2b0d7b3dcb6d"),
                Name = "MSI A340 Gaming Keyboard",
                Description = "Ergonomic keyboard, 104 keys, RGB lighting",
                Price = 20,
                ImageFile = "product-1.png",
                Category = ["Keyboards"]
            },
            new()
            {
                Id = new Guid("77c1b1f8-3e1e-4f0c-8b7b-0f0b9b7b8eac"),
                Name = "Delux A20 Wireless Mouse",
                Description = "Wireless mouse, 6 buttons, 1600 DPI",
                Price = 10,
                ImageFile = "product-2.png",
                Category = ["Mice"]
            },
            new()
            {
                Id = new Guid("b4e3f8e3-0b0b-4f0c-8b7b-0f0b9b7b8eac"),
                Name = "LG ThinQ G7 Monitor",
                Description = "27-inch monitor, 4K resolution, 144Hz refresh rate",
                Price = 200,
                ImageFile = "product-3.png",
                Category = ["Monitors"]
            },
            new()
            {
                Id = new Guid ("8f1383c4-1171-42a9-b863-21ebc917e296"),
                Name = "Razer Kraken X Headset",
                Description = "Wired headset, 7.1 surround sound, noise-cancelling microphone",
                Price = 50,
                ImageFile = "product-4.png",
                Category = ["Headsets"]
            },
            new()
            {
                Id = new Guid("468c97b2-4773-48d5-913c-2f45dd87426b"),
                Name = "Logitech G502 Hero Mouse",
                Description = "Wired mouse, 11 buttons, 16000 DPI",
                Price = 30,
                ImageFile = "product-5.png",
                Category = ["Mice"]
            },
            new()
            {
                Id = new Guid("25d44c18-0238-494c-8dba-c41d5ca569b0"),
                Name = "Corsair K70 RGB Keyboard",
                Description = "Mechanical keyboard, 104 keys, RGB lighting",
                Price = 100,
                ImageFile = "product-6.png",
                Category = ["Keyboards"]
            },
            new()
            {
                Id = new Guid("f6e30abc-0e7a-41a6-b333-33686e36be57"),
                Name = "Samsung Odyssey G9 Monitor",
                Description = "49-inch monitor, 5K resolution, 240Hz refresh rate",
                Price = 1000,
                ImageFile = "product-7.png",
                Category = ["Monitors"]
            },
        };
    }
}