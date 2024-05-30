using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class VaultKeepController : ControllerBase
{
    private readonly VaultKeepService _vaultKeepService;
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultService _vaultService;

    public VaultKeepController(VaultKeepService vaultKeepService, Auth0Provider auth0Provider, VaultService vaultService)
    {
        _vaultKeepService = vaultKeepService;
        _auth0Provider = auth0Provider;
        _vaultService = vaultService;
    }

    [HttpGet]
    public async Task<ActionResult<List<VaultKeep>>> GetVaultKeeps()
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = user.Id;
            List<VaultKeep> vaultkeeps = _vaultKeepService.GetVaultKeeps(userId);
            return Ok(vaultkeeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultkeepId}")]
    public ActionResult<VaultKeep> GetKeepById(int vaultkeepId)
    {
        try
        {
            VaultKeep vaultkeep = _vaultKeepService.GetVaultKeepById(vaultkeepId);
            return Ok(vaultkeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultkeepData)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            if (user == null)
            {
                throw new Exception("Unable to keep - please log in!");
            }
            Vault vault = _vaultService.GetVaultById(vaultkeepData.VaultId);
            if (user.Id != vault.CreatorId)
            {
                throw new Exception("Not your vault to keep things in!");
            }
            VaultKeep vaultkeep = _vaultKeepService.CreateVaultKeep(vaultkeepData, user.Id);
            return Ok(vaultkeep);
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