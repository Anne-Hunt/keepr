namespace keepr.Controllers;

public class VaultController
{
    private readonly VaultService _vaultService;
    private readonly Auth0Provider _auth0Provider;

    public VaultController(VaultService vaultService, Auth0Provider auth0Provider)
    {
        _vaultService = vaultService;
        _auth0Provider = auth0Provider;
    }
}