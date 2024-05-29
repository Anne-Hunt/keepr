using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultService _vaultService;
  private readonly KeepService _keepService;
  private readonly VaultKeepService _vaultkeepService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultService vaultService, KeepService keepService, VaultKeepService vaultkeepService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultService = vaultService;
    _keepService = keepService;
    _vaultkeepService = vaultkeepService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]

  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      if (user == null)
      {
        throw new Exception("Please log in to continue!");
      }
      List<Vault> vaults = _vaultService.GetMyVaults(user.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("keeps")]
  [Authorize]
  public async Task<ActionResult<List<Keep>>> GetMyKeeps()
  {
    try
    {
      Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      if (user == null)
      {
        throw new Exception("Please log in to continue!");
      }
      List<Keep> keeps = _keepService.GetMyKeeps(user.Id);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("vaultkeeps")]
  [Authorize]
  public async Task<ActionResult<List<VaultKeep>>> GetMyVaultKeeps()
  {
    try
    {
      Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      if (user == null)
      {
        throw new Exception("Please log in to continue!");
      }
      List<VaultKeep> keeps = _vaultkeepService.GetVaultKeepsByAccount(user.Id);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPut("{accountId}")]
  [Authorize]
  public async Task<ActionResult<Account>> EditAccount([FromBody] Account accountData, string accountId)
  {
    try
    {
      Account accountUser = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string accountEmail = accountUser.Email;
      Account account = _accountService.Edit(accountData, accountEmail);
      return account;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
