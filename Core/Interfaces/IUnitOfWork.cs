namespace Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IBrandRepository Brands { get; }
    ICategoryRepository Categories { get; }
    IRolRepository Roles { get; }
    IUserRepository Users { get; }
    Task<int> SaveAsync();
}
