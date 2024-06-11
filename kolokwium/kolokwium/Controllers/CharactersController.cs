using kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium.Controllers;

[ApiController]
[Route("api/characters")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterInfo(int id)
    {
        if (!await _dbService.DoesCharacterExist(id))
            return NotFound($"Character with id = {id} does not exist!");
        
        return Ok(await _dbService.GetCharacterInfo(id));
    }
}