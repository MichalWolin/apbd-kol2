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

    public async Task<bool> DoesItemExist(int id)
    {
        var result = await _context.Items.AnyAsync(c => c.Id.Equals(id));

        return result;
    }

    public async Task<bool> CanCharacterCarryMore(int id, List<int> items)
    {
        var characterWeight = await _context.Characters.Where(c => c.Id.Equals(id))
            .Select(c => c.CurrentWeight).FirstOrDefaultAsync();
        
        var characterMaxWeight = await _context.Characters.Where(c => c.Id.Equals(id))
            .Select(c => c.MaxWeight).FirstOrDefaultAsync();

        int itemsWeight = 0;

        foreach (var item in items)
        {
            itemsWeight += await _context.Items.Where(i => i.Id.Equals(item))
                .Select(i => i.Weight).FirstOrDefaultAsync();
        }
        
        return itemsWeight + characterWeight <= characterMaxWeight;
    }
}