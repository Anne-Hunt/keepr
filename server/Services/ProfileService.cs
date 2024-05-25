

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

    internal List<KeptVaultKeep> GetKeepsByProfile(string profileId)
    {
        List<KeptVaultKeep> keeps = _keepService.GetKeepsByProfile(profileId);
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