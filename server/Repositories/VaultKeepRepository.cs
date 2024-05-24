

namespace keepr.Repositories;

public class VaultKeepRepository
{
    private readonly IDbConnection db;
    public VaultKeepRepository(IDbConnection _db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData, string userId)
    {
        throw new NotImplementedException();
    }

    internal VaultKeep GetVaultKeepById(int vaultkeepId)
    {
        throw new NotImplementedException();
    }

    internal List<VaultKeep> GetVaultKeeps()
    {
        throw new NotImplementedException();
    }

    internal void TrashVaultKeep(int vaultkeepId)
    {
        throw new NotImplementedException();
    }
}