
namespace keepr.Services;

public class VaultService
{
    private readonly VaultRepository _repository;

    public VaultService(VaultRepository repository)
    {
        _repository = repository;
    }

    internal List<Vault> GetVaults()
    {
        List<Vault> vaults = _repository.GetVaults();
        return vaults;
    }

    internal Vault GetVaultById(int vaultId)
    {
        Vault vault = _repository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception("Unable to find vault!");
        }
        return vault;
    }

    internal Vault CreateVault(Vault vaultData, string userId)
    {
        vaultData.CreatorId = userId;
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId, string userId)
    {
        Vault vaultToUpdate = GetVaultById(vaultId);
        if (vaultToUpdate.CreatorId != userId)
        {
            throw new Exception("You can't update what isn't yours!");
        }
        vaultData.Id = vaultId;
        Vault vault = _repository.UpdateVault(vaultData);
        return vault;
    }

    internal string TrashVault(int vaultId, string userId)
    {
        Vault vaultTrash = GetVaultById(vaultId);
        if (vaultTrash.CreatorId != userId)
        {
            throw new Exception("You can't delete what isn't yours!");
        }
        _repository.TrashVault(vaultId);
        return "Vault deleted!";
    }

}