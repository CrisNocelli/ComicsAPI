using Comics.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comics.Data.Repository
{
    public interface IComicCharacterRepository
    {
        ValueTask<ComicCharacter> GetById(int id);
        Task<IEnumerable<ComicCharacter>> GetAllCharacters(string orderBy);
    }
}
