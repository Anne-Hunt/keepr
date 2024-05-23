namespace keepr.Repositories;

public class KeepRepository
{
    private readonly IDbConnection db;
    public KeepRepository(IDbConnection _db)
    {
        _db = db;
    }
}