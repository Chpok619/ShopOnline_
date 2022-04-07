using ShopBackend.Repositories;

namespace ShopBackend.UnitOfWork;

public class UnitOfWorkEF: IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _appDbContext;
    public IAccountRepository AccountRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWorkEF(AppDbContext appDbContext, IAccountRepository accountRepository, IProductRepository productRepository)
    {
        _appDbContext = appDbContext;
        AccountRepository = accountRepository;
        ProductRepository = productRepository;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _appDbContext.SaveChangesAsync(cancellationToken);


    public void Dispose() => _appDbContext.Dispose();
    public ValueTask DisposeAsync() => _appDbContext.DisposeAsync();
}