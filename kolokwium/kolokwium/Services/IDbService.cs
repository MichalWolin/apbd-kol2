using kolokwium.DTOs;

namespace kolokwium.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int id);
    Task<IEnumerable<CharacterInfoDto>> GetCharacterInfo(int id);
}