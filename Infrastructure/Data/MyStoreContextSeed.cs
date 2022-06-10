using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using CsvHelper;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;
public class MyStoreContextSeed
{
    public static async Task SeedAsync(MyStoreContext context, ILoggerFactory loggerFactory) 
    {
        try
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Brands.Any()) {
                using(var readerBrands = new StreamReader(path+@"/Data/Csvs/marcas.csv")) {
                    using (var csvBrands = new CsvReader(readerBrands, CultureInfo.InvariantCulture)) {
                        var brands = csvBrands.GetRecords<Brand>();
                        context.Brands.AddRange(brands);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if (!context.Categories.Any()) {
                using(var readerCategories = new StreamReader(path+@"/Data/Csvs/categorias.csv")) {
                    using (var csvCategories = new CsvReader(readerCategories, CultureInfo.InvariantCulture)) {
                        var categories = csvCategories.GetRecords<Category>();
                        context.Categories.AddRange(categories);
                        await context.SaveChangesAsync();
                    }
                }
            }

             if (!context.Products.Any()) {
                using(var readerProducts = new StreamReader(path+@"/Data/Csvs/productos.csv")) {
                    using (var csvProducts = new CsvReader(readerProducts, CultureInfo.InvariantCulture)) {
                        var listProductsCsv = csvProducts.GetRecords<Product>();

                        List<Product> products = new List<Product>();
                        foreach (var item in listProductsCsv)
                        {
                            products.Add(new Product{
                                Id = item.Id,
                                Name = item.Name,
                                Price = item.Price,
                                CreationDate = item.CreationDate,
                                CategoryId = item.CategoryId,
                                BrandId = item.BrandId
                            });
                        }

                        context.Products.AddRange(products);
                        await context.SaveChangesAsync();
                    }
                }
            }


        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<MyStoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }

    public static async Task SeedRolesAsync(MyStoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>()
                        {
                            new Rol{Id=1, Name="Administrator"},
                            new Rol{Id=2, Name="Manager"},
                            new Rol{Id=3, Name="Employee"},
                        };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<MyStoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }

}
