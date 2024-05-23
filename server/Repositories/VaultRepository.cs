namespace keepr.Repositories;

public class VaultRepository
{
    private readonly IDbConnection db;
    public VaultRepository(IDbConnection _db)
    {
        _db = db;
    }
}