

namespace keepr.Repositories;

public class KeepRepository
{
    private readonly IDbConnection db;
    public KeepRepository(IDbConnection _db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData, string userId)
    {
        throw new NotImplementedException();
    }

    internal Keep GetKeepById(int keepId)
    {
        throw new NotImplementedException();
    }

    internal List<Keep> GetKeeps()
    {
        throw new NotImplementedException();
    }

    internal void TrashKeep(int keepId)
    {
        throw new NotImplementedException();
    }

    internal Keep UpdateKeep(Keep keepdata, int keepId)
    {
        throw new NotImplementedException();
    }
}