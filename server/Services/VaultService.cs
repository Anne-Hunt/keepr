
namespace keepr.Services;

public class VaultService
{
    private readonly VaultRepository _repository;

    public VaultService(VaultRepository repository)
    {
        _repository = repository;
    }

    internal Vault GetVaultById(int vaultId)
    {
        throw new NotImplementedException();
    }

    internal List<Vault> GetVaults()
    {
        throw new NotImplementedException();
    }

    internal string TrashVault(int vaultId, string id)
    {
        throw new NotImplementedException();
    }

    internal Vault UpdateVault(int vaultId, string id)
    {
        throw new NotImplementedException();
    }
}