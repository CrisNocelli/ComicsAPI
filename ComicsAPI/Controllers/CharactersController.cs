using Comics.DTO;
using ComicsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComicsAPI.Controllers
{
    [Route("api/v{version:apiVersion}/public/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersService _charactersService;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ICharactersService charactersService, ILogger<CharactersController> logger)
        {
            _charactersService = charactersService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<DataWrapper> Get([FromQuery] CharactersRequest request)
        {
            if (request == null)
                request = new CharactersRequest();

            var response = ValidateRequest(request,
                out List<int> comicIds, out List<int> serieIds, out List<int> eventIds, out List<int> storyIds, out string[] orderByValues, out int requestLimit);

            if (response != null)
                return response;

            response = new DataWrapper(true);

            try
            {
                response.Data.Offset = request.Offset;
                response.Data.Limit = requestLimit;

                var results = await _charactersService.GetCharacters(request, comicIds, serieIds, eventIds, storyIds, orderByValues, requestLimit);

                if (results?.Count > 0)
                {
                    response.Data.Total = results.Count;

                    var remainingRows = results.Count - request.Offset;

                    if (remainingRows > 0)
                    {
                        if (remainingRows >= requestLimit)
                            response.Data.Count = requestLimit;
                        else
                            response.Data.Count = remainingRows;
                    }

                    response.Data.Results = results;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType() + " [GET]", ex);
            }

            return response;
        }

        [HttpGet]
        [Route("{characterId}")]
        public async Task<DataWrapper> GetById(string characterId)
        {
            int.TryParse(characterId, out int id);

            if (id <= 0)
            {
                return new DataWrapper()
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Status = "We couldn't find that character"
                };
            }

            var response = new DataWrapper(true);

            try
            {
                var result = await _charactersService.GetCharacterById(id);

                if (result == null)
                {
                    return new DataWrapper()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Status = "We couldn't find that character"
                    };
                }

                response.Data.Total = 1;
                response.Data.Count = 1;

                response.Data.Results = new List<ResultFullView> { result };
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType() + " [GET]", ex);
            }

            return response;
        }

        private DataWrapper ValidateRequest(CharactersRequest request,
            out List<int> comicIds, out List<int> serieIds, out List<int> eventIds, out List<int> storyIds, out string[] orderByValues, out int requestLimit)
        {
            comicIds = new List<int>();
            serieIds = new List<int>();
            eventIds = new List<int>();
            storyIds = new List<int>();
            orderByValues = new string[] { };
            requestLimit = 0;

            // 409 Limit greater than 100.
            // 409 Limit invalid or below 1.

            if (string.IsNullOrEmpty(request.Limit))
            {
                request.Limit = "20";
            }

            int.TryParse(request.Limit, out int limit);

            if (limit <= 0)
            {
                return new DataWrapper()
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Status = "You must pass an integer limit greater than 0."
                };
            }

            if (limit > 100)
            {
                return new DataWrapper()
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Status = "You may not request more than 100 items."
                };
            }

            requestLimit = limit;

            //409 Invalid or unrecognized ordering parameter.
            orderByValues = request.OrderBy?.Split(',').Select(s => s.Trim()).ToArray();
            var validOrderByValues = new string[] { "name", "modified", "-name", "-modified" };

            if (orderByValues?.Length > 0 && orderByValues.Any(x => !validOrderByValues.Contains(x)))
            {
                return new DataWrapper()
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Status = "Invalid or unrecognized ordering parameter."
                };
            }

            // 409 Too many values sent to a multi-value list filter.
            // 409 Invalid value passed to filter.
            DataWrapper response = ValidateParameter(request.Comics, "comics", out comicIds);
            if (response != null)
                return response;

            response = ValidateParameter(request.Series, "series", out serieIds);
            if (response != null)
                return response;

            response = ValidateParameter(request.Events, "events", out eventIds);
            if (response != null)
                return response;

            response = ValidateParameter(request.Stories, "stories", out storyIds);
            if (response != null)
                return response;

            return null;
        }

        private DataWrapper ValidateParameter(string parameter, string parameterName, out List<int> parameterIds)
        {
            DataWrapper response = null;

            int temp;
            parameterIds = parameter?.Split(',')
                .Select(s => new { P = int.TryParse(s, out temp), I = temp })
                .Where(x => x.P)
                .Select(x => x.I)
                .ToList();

            if (parameterIds?.Count > 10)
            {
                response = new DataWrapper
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Status = $"You may not submit more than 10 {parameterName} ids."
                };

                return response;
            }

            var invalidParameterIds = parameter?.Split(',')
                .Select(s => new { P = int.TryParse(s, out temp), I = s })
                .Where(x => !x.P)
                .Select(x => x.I)
                .ToArray();

            if (invalidParameterIds?.Length > 0)
            {
                response = new DataWrapper
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Status = $"You must pass at least one valid {parameterName} if you set the {parameterName} filter."
                };

                return response;
            }

            return response;
        }
    }
}