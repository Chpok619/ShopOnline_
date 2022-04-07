using ShopBackend.Repositories;

namespace ShopBackend.UnitOfWork;

public interface IUnitOfWork: IDisposable
{
    IAccountRepository AccountRepository { get; }
    IProductRepository ProductRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}