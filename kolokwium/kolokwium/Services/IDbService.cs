using kolokwium.DTOs;

namespace kolokwium.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int id);
    Task<IEnumerable<CharacterInfoDto>> GetCharacterInfo(int id);
    Task<bool> DoesItemExist(int id);
    Task<bool> CanCharacterCarryMore(int id, List<int> items);
    Task AddItemsToCharacter(int id, List<int> items);
    Task UpdateCharactersWeight(int id, List<int> items);
}