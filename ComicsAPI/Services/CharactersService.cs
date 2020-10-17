using Comics.Data.Repository;
using Comics.Domain;
using Comics.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsAPI.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly IComicCharacterRepository _comicCharacterRepository;
        public CharactersService(IComicCharacterRepository comicCharacterRepository)
        {
            _comicCharacterRepository = comicCharacterRepository;
        }

        public async Task<List<ResultFullView>> GetCharacters(CharactersRequest request,
            List<int> comicIds, List<int> serieIds, List<int> eventIds, List<int> storyIds, string[] orderByValues, int requestLimit)
        {
            List<ResultFullView> result = new List<ResultFullView>();

            // Apply order by
            string orderBy = string.Empty;

            if (orderByValues?.Length > 0)
            {
                orderBy = "order by";

                foreach (var orderByValue in orderByValues)
                {
                    if (orderByValue.StartsWith("-"))
                    {
                        if (orderBy.IndexOf(orderByValue[1..]) == -1)
                            orderBy = $"{orderBy} {orderByValue[1..]} desc,";
                    }
                    else
                    {
                        if (orderBy.IndexOf(orderByValue) == -1)
                            orderBy = $"{orderBy} {orderByValue},";
                    }
                }

                orderBy = orderBy[0..^1];
            }

            var characters = await _comicCharacterRepository.GetAllCharacters(orderBy);

            if (characters == null || !characters.Any())
                return result;

            // Apply filters
            if (!string.IsNullOrEmpty(request.Name))
                characters = characters.Where(x => x.Name.ToUpper() == request.Name.ToUpper());

            if (!string.IsNullOrEmpty(request.NameStartsWith))
                characters = characters.Where(x => x.Name.ToUpper().StartsWith(request.NameStartsWith.ToUpper()));

            characters = characters.Where(x => x.Modified >= request.ModifiedSince);

            if (comicIds?.Count > 0)
                characters = characters.Where(x => x.Comics != null && x.Comics.Items != null && x.Comics.Items.Select(x => x.Id).Any(y => comicIds.Contains(y)));

            if (serieIds?.Count > 0)
                characters = characters.Where(x => x.Series != null && x.Series.Items != null && x.Series.Items.Select(x => x.Id).Any(y => serieIds.Contains(y)));

            if (eventIds?.Count > 0)
                characters = characters.Where(x => x.Events != null && x.Events.Items != null && x.Events.Items.Select(x => x.Id).Any(y => eventIds.Contains(y)));

            if (storyIds?.Count > 0)
                characters = characters.Where(x => x.Stories != null && x.Stories.Items != null && x.Stories.Items.Select(x => x.Id).Any(y => storyIds.Contains(y)));

            // Apply limit and offset
            // Convert
            if (request.Offset > 0)
                characters = characters.Skip(request.Offset);
            if (requestLimit > 0)
                characters = characters.Take(requestLimit);

            result = characters.Select(x => ConvertToDto(x)).ToList();

            return result;
        }

        public async Task<ResultFullView> GetCharacterById(int id)
        {
            ResultFullView result = new ResultFullView();

            var character = await _comicCharacterRepository.GetById(id);

            if (character == null)
                return result;

            var resultDTO = ConvertToDto(character);
            return resultDTO;
        }

        private ResultFullView ConvertToDto(ComicCharacter comicCharacter)
        {
            var result = new ResultFullView()
            {
                Id = comicCharacter.Id,
                Name = comicCharacter.Name,
                Description = comicCharacter.Description,
                Modified = comicCharacter.Modified,
                Thumbnail = new Comics.DTO.Thumbnail()
                {
                    Path = comicCharacter.Thumbnail.Path,
                    Extension = comicCharacter.Thumbnail.Extension
                },
                ResourceURI = comicCharacter.ResourceURI
            };
            //TODO: map comics, stories, events, series
            return result;
        }
    }
}
