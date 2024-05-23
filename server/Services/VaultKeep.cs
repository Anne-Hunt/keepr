namespace keepr.Services;

public class VaultKeepService
{
    private readonly VaultKeepRepository _repository;

    public VaultKeepService(VaultKeepRepository repository)
    {
        _repository = repository;
    }
}