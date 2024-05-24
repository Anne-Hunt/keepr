using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;
[ApiController]
[Route("api/[controller]s")]
public class VaultController : ControllerBase
{
    private readonly VaultService _vaultService;
    private readonly Auth0Provider _auth0Provider;

    public VaultController(VaultService vaultService, Auth0Provider auth0Provider)
    {
        _vaultService = vaultService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<Vault>> GetVaults()
    {
        try
        {
            List<Vault> vaults = _vaultService.GetVaults();
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public ActionResult<Vault> GetVaultById(int vaultId)
    {
        try
        {
            Vault vault = _vaultService.GetVaultById(vaultId);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultService.CreateVault(vaultData, user.Id);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> UpdateVault(Vault vaultData, int vaultId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultService.UpdateVault(vaultData, vaultId, user.Id);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> TrashVault(int vaultId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultService.TrashVault(vaultId, user.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}