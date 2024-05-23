
namespace keepr.Services;

public class VaultKeepService
{
    private readonly VaultKeepRepository _repository;

    public VaultKeepService(VaultKeepRepository repository)
    {
        _repository = repository;
    }

    internal Keep GetVaultKeepById(int vaultkeepId)
    {
        throw new NotImplementedException();
    }

    internal List<VaultKeep> GetVaultKeeps()
    {
        throw new NotImplementedException();
    }

    internal string TrashVaultKeep(int vaultkeepId, string id)
    {
        throw new NotImplementedException();
    }
}