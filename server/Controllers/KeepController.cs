namespace keepr.Controllers;

public class KeepController
{
    private readonly KeepService _keepService;
    private readonly Auth0Provider _auth0Provider;

    public KeepController(KeepService keepService, Auth0Provider auth0Provider)
    {
        _keepService = keepService;
        _auth0Provider = auth0Provider;
    }
}