using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Entites.Product;

namespace talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync( StoreContext dbcontext )
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var brandsdata = File.ReadAllText("../talabat.Repository/Data/Dataseed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);
                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }

            if (!dbcontext.ProductTypes.Any())
            {
                var typesdata = File.ReadAllText("../talabat.Repository/Data/Dataseed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(type);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }

            if (!dbcontext.Products.Any())
            {
                var productsdata = File.ReadAllText("../talabat.Repository/Data/Dataseed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbcontext.Set<Product>().AddAsync(product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.deliveryMethods.Any())
            {
                var deliveryMethodsdata = File.ReadAllText("../talabat.Repository/Data/Dataseed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsdata);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        await dbcontext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }


        }
    }
}
