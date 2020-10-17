using Comics.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsAPI.Services
{
    public interface ICharactersService
    {
        Task<List<ResultFullView>> GetCharacters(CharactersRequest request, List<int> comicIds, List<int> serieIds, List<int> eventIds, List<int> storyIds, string[] orderByValues, int requestLimit);
        Task<ResultFullView> GetCharacterById(int id);
    }
}
