
namespace keepr.Repositories;

public class KeepRepository
{
    private readonly IDbConnection db;
    public KeepRepository(IDbConnection _db)
    {
        _db = db;
    }

    internal void TrashKeep(int keepId)
    {
        throw new NotImplementedException();
    }
}