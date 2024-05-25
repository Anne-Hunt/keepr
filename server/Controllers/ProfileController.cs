using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;
[ApiController]
[Route("api/[controller]s")]
public class ProfileController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly ProfileService _profileService;
    private readonly KeepService _keepService;
    private readonly VaultService _vaultService;

    public ProfileController(Auth0Provider auth0Provider, ProfileService profileService, KeepService keepService, VaultService vaultService)
    {
        _auth0Provider = auth0Provider;
        _profileService = profileService;
        _keepService = keepService;
        _vaultService = vaultService;
    }

    [HttpGet("{profileId}")]

    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _profileService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<KeptVaultKeep>> GetKeepsByProfile(string profileId)
    {
        try
        {
            List<KeptVaultKeep> keeps = _keepService.GetKeepsByProfile(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]

    public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
    {
        try
        {
            List<Vault> vaults = _profileService.GetVaultsByProfile(profileId);
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}