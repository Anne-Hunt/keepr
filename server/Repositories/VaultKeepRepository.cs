namespace keepr.Repositories;

public class VaultKeepRepository
{
    private readonly IDbConnection db;
    public VaultKeepRepository(IDbConnection _db)
    {
        _db = db;
    }

}