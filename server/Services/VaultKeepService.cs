
namespace keepr.Services;

public class VaultKeepService
{
    private readonly VaultKeepRepository _repository;

    public VaultKeepService(VaultKeepRepository repository)
    {
        _repository = repository;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultkeepData, string userId)
    {
        vaultkeepData.CreatorId = userId;
        VaultKeep vaultkeep = _repository.CreateVaultKeep(vaultkeepData);
        return vaultkeep;
    }

    internal VaultKeep GetVaultKeepById(int vaultkeepId)
    {
        VaultKeep vaultkeep = _repository.GetVaultKeepById(vaultkeepId);
        if (vaultkeep == null)
        {
            throw new Exception("Unable to find vaultkeep!");
        }
        return vaultkeep;
    }

    internal List<VaultKeep> GetVaultKeeps(string userId)
    {
        List<VaultKeep> vaultkeeps = _repository.GetVaultKeeps(userId);
        return vaultkeeps;
    }

    internal string TrashVaultKeep(int vaultkeepId, string userId)
    {
        VaultKeep foundVK = GetVaultKeepById(vaultkeepId);
        if (foundVK.CreatorId != userId)
        {
            throw new Exception("You cannot delete what isn't yours!");
        }
        _repository.TrashVaultKeep(vaultkeepId);
        return "VaultKeep deleted!";
    }
}