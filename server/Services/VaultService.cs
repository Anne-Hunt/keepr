
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
        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _repository.UpdateVault(vaultToUpdate);
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

    internal Vault GetPublicVault(int vaultId)
    {
        Vault vaultCheck = GetVaultById(vaultId);
        if (vaultCheck.IsPrivate == true)
        {
            throw new Exception("You don't own this private vault!");
        }
        return vaultCheck;
    }

    internal Vault GetPrivateVault(int vaultId, string userId)
    {
        Vault vaultCheck = _repository.GetVaultById(vaultId);
        if (vaultCheck.CreatorId != userId)
        {
            throw new Exception("You don't own this private Vault!");
        }
        return vaultCheck;
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        List<Vault> vaults = _repository.GetMyVaults(userId);
        return vaults;
    }
}