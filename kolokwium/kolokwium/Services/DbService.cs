using kolokwium.Controllers;
using kolokwium.Data;
using kolokwium.DTOs;
using kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesCharacterExist(int id)
    {
        var result = await _context.Characters.AnyAsync(c => c.Id.Equals(id));

        return result;
    }

    public async Task<IEnumerable<CharacterInfoDto>> GetCharacterInfo(int id)
    {
        var character = await _context.Characters.Where(c => c.Id.Equals(id))
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title).ToListAsync();

        var result = character.Select(c => new CharacterInfoDto
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            CurrentWeight = c.CurrentWeight,
            MaxWeight = c.MaxWeight,
            BackpackItems = c.Backpacks.Select(b => new BackpackItemDto
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = c.CharacterTitles.Select(t => new CharacterTitleDto
            {
                Title = t.Title.Name,
                AcquiredAt = t.AcquiredAt
            }).ToList()
        });

        return result;
    }
}