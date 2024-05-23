using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepController : ControllerBase
{
    private readonly VaultKeepService _vaultKeepService;
    private readonly Auth0Provider _auth0Provider;

    public VaultKeepController(VaultKeepService vaultKeepService, Auth0Provider auth0Provider)
    {
        _vaultKeepService = vaultKeepService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<VaultKeep>> GetVaultKeeps()
    {
        try
        {
            List<VaultKeep> vaultkeeps = _vaultKeepService.GetVaultKeeps();
            return Ok(vaultkeeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultkeepId}")]
    public ActionResult<Keep> GetKeepById(int vaultkeepId)
    {
        try
        {
            Keep keep = _vaultKeepService.GetVaultKeepById(vaultkeepId);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultkeepId}")]
    public async Task<ActionResult<string>> TrashVaultKeep(int vaultkeepId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultKeepService.TrashVaultKeep(vaultkeepId, user.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}