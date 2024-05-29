using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;
[ApiController]
[Route("api/[controller]s")]
public class VaultController : ControllerBase
{
    private readonly VaultService _vaultService;
    private readonly Auth0Provider _auth0Provider;
    private readonly KeepService _keepService;
    private readonly VaultKeepService _vaultkeepService;

    public VaultController(VaultService vaultService, Auth0Provider auth0Provider, KeepService keepService, VaultKeepService vaultkeepService)
    {
        _vaultService = vaultService;
        _auth0Provider = auth0Provider;
        _keepService = keepService;
        _vaultkeepService = vaultkeepService;
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

    [HttpGet("{vaultId}/vaultkeeps")]
    public ActionResult<List<Vault>> GetVaultKeepsByVault(int vaultId)
    {
        try
        {
            List<VaultKeep> vaultkeeps = _vaultkeepService.GetVaultKeepByVault(vaultId);
            return Ok(vaultkeeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            if (user != null)
            {
                Vault vaultClosed = _vaultService.GetPrivateVault(vaultId, user.Id);
                return Ok(vaultClosed);
            }
            if (user == null)
            {
                Vault vaultOpen = _vaultService.GetPublicVault(vaultId);
                return Ok(vaultOpen);
            }
            return Ok("Vault Found");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<KeptVaultKeep>>> GetKeepsByVault(int vaultId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<KeptVaultKeep> keeps = new List<KeptVaultKeep>();
            List<VaultKeep> vaultkeeps = new List<VaultKeep>();
            if (user != null)
            {
                Vault vaultClosed = _vaultService.GetPrivateVault(vaultId, user.Id);
                List<VaultKeep> vaultkeepsClosed = _vaultkeepService.GetVaultKeepByVault(vaultId);
                vaultkeeps = vaultkeepsClosed;
            }
            if (user == null)
            {
                Vault vaultOpen = _vaultService.GetPublicVault(vaultId);
                List<VaultKeep> vaultkeepsOpen = _vaultkeepService.GetVaultKeepByVault(vaultId);
                vaultkeeps = vaultkeepsOpen;
            }
            foreach (VaultKeep vaultkeep in vaultkeeps)
            {
                int keepId = vaultkeep.KeepId;
                KeptVaultKeep keep = _keepService.GetKeepsByVaultId(keepId);
                keeps.Add(keep);
            }
            return Ok(keeps);
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