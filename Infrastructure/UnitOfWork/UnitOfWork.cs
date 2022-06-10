using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MyStoreContext _context;
    private IProductRepository _products;
    private IBrandRepository _brands;
    private ICategoryRepository _categories;
    private IRolRepository _roles;
    private IUserRepository _users;

    public UnitOfWork(MyStoreContext context)
    {
        _context = context;
    }

    public ICategoryRepository Categories
    {
        get
        {
            if (_categories == null)
            {
                _categories = new CategoryRepository(_context);
            }
            return _categories;
        }
    }

    public IBrandRepository Brands
    {
        get
        {
            if (_brands == null)
            {
                _brands = new BrandRepository(_context);
            }
            return _brands;
        }
    }

    public IProductRepository Products
    {
        get
        {
            if (_products == null)
            {
                _products = new ProductRepository(_context);
            }
            return _products;
        }
    }

    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
