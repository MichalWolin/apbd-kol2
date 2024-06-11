using System.Transactions;
using kolokwium.DTOs;
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

    [HttpPost("{id}/backpacks")]
    public async Task<IActionResult> AddItemToCharacter(int id, List<int> items)
    {
        if (!await _dbService.DoesCharacterExist(id))
            return NotFound($"Character with id = {id} does not exist!");
        
        foreach (var item in items)
            if (!await _dbService.DoesItemExist(item))
                return NotFound($"Item with id = {item} does not exist!");

        if (!await _dbService.CanCharacterCarryMore(id, items))
            return BadRequest("Character cannot carry these items!");
        
        // using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        // {
        //     
        //
        //     scope.Complete();
        // }
        
        return Ok(items);
    }
}