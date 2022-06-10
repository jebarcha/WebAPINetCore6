using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(MyStoreContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductosMasCaros(int quantity) =>
                    await _context.Products
                        .OrderByDescending(p => p.Price)
                        .Take(quantity)
                        .ToListAsync();

    public override async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .FirstOrDefaultAsync(p => p.Id == id);

    }
    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
                .Include(u => u.Brand)
                .Include(u => u.Category)
                .ToListAsync();
    }

    public override async Task<(int totalRecords, IEnumerable<Product> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var toSearch = _context.Products as IQueryable<Product>;

        if (!String.IsNullOrEmpty(search))
        {
            toSearch = toSearch.Where(p => p.Name.ToLower().Contains(search.ToLower()));
        }

        var totalRegistros = await toSearch  //_context.Products
                                    .CountAsync();

        var registros = await toSearch  // _context.Products
                                .Include(u => u.Brand)
                                .Include(u => u.Category)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totalRegistros, registros);
    }

}
