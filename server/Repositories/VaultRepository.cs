
namespace keepr.Repositories;

public class VaultRepository
{
    private readonly IDbConnection db;
    public VaultRepository(IDbConnection _db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData, string userId)
    {
        throw new NotImplementedException();
    }

    internal Vault GetVaultById(int vaultId)
    {
        throw new NotImplementedException();
    }

    internal List<Vault> GetVaults()
    {
        throw new NotImplementedException();
    }

    internal void TrashVault(int vaultId)
    {
        throw new NotImplementedException();
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId)
    {
        throw new NotImplementedException();
    }
}