using Comics.Data.Queries;
using Comics.Domain;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comics.Data.Repository
{
    public class ComicCharacterRepository : BaseRepository, IComicCharacterRepository
    {
        private readonly ICommandText _commandText;

        public ComicCharacterRepository(IConfiguration configuration, ICommandText commandText, ILogger<BaseRepository> logger) : base(configuration, logger)
        {
            _commandText = commandText;
        }

        public async Task<IEnumerable<ComicCharacter>> GetAllCharacters(string orderBy)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<ComicCharacter, Thumbnail, ComicCharacter>(
                    $"{_commandText.GetCharacters} {orderBy}",
                    map: (character, thumbnail) =>
                    {
                        character.Thumbnail = thumbnail;
                        return character;
                    },
                    splitOn:"Path"
                );
                return query;
            });
        }

        public async ValueTask<ComicCharacter> GetById(int id)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<ComicCharacter, Thumbnail, ComicCharacter>(
                    _commandText.GetCharactersById,
                    param: new { Id = id },
                    map: (character, thumbnail) =>
                    {
                        character.Thumbnail = thumbnail;
                        return character;
                    },
                    splitOn: "Path"
                );
                return query.FirstOrDefault();
            });
        }
    }
}
