using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;
[ApiController]
[Route("api/[controller]")]
public class KeepController : ControllerBase
{
    private readonly KeepService _keepService;
    private readonly Auth0Provider _auth0Provider;

    public KeepController(KeepService keepService, Auth0Provider auth0Provider)
    {
        _keepService = keepService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keeps = _keepService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepService.GetKeepById(keepId);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepService.CreateKeep(keepData, user.Id);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> UpdateKeep(Keep keepData, int keepId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepService.UpdateKeep(keepData, keepId, user.Id);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<string>> TrashKeep(int keepId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepService.TrashKeep(keepId, user.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}