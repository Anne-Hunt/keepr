

namespace keepr.Services;

public class ProfileService
{
    private readonly ProfileRepository _repository;
    private readonly VaultService _vaultService;
    private readonly KeepService _keepService;
    private readonly VaultKeepService _vaultkeepService;
    public ProfileService(ProfileRepository repository, VaultKeepService vaultkeepService, KeepService keepService, VaultService vaultService)
    {
        _repository = repository;
        _vaultkeepService = vaultkeepService;
        _keepService = keepService;
        _vaultService = vaultService;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
        List<Vault> vaults = _vaultService.GetPublicVaultsByProfile(profileId);
        List<Keep> keeps = new List<Keep>();
        List<VaultKeep> vaultkeeps = new List<VaultKeep>();
        foreach (Vault vault in vaults)
        {
            if (vault.IsPrivate == true)
            {
                return null;
            }
            List<VaultKeep> vaultkeepsByVault = _vaultkeepService.GetVaultKeepByVault(vault.Id);
            vaultkeeps.AddRange(vaultkeepsByVault);
        }
        foreach (VaultKeep vaultkeep in vaultkeeps)
        {
            Keep keepByVault = _keepService.GetKeepByVaultKeep(vaultkeep.KeepId);
            keeps.Add(keepByVault);
        }
        return keeps;
    }

    internal Profile GetProfileById(string profileId)
    {
        Profile profile = _repository.GetProfileById(profileId);
        return profile;
    }

    internal List<Vault> GetVaultsByProfile(string profileId)
    {
        List<Vault> vaults = _vaultService.GetPublicVaultsByProfile(profileId);
        return vaults;
    }
}