using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using Showcase.PokeApi.Models.Responses;
using Showcase.PokeApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PokemonsController : BaseController
    {
        private readonly IPokemonService _pokemonService;

        public PokemonsController(ILogger logger, IPokemonService pokemonService) : base(logger)
        {
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// Obtêm uma lista de 10 pokémons aleatórios
        /// </summary>
        /// <returns>Lista com dados de pokémons</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PokemonResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarAsync() =>
        await TratarResultadoAsync(async () =>
        {
            var resultado = await _pokemonService.ListarAsync();

            return !string.IsNullOrEmpty(resultado.Mensagem) ?
                    new ObjectResult(new { resultado.Mensagem }) { StatusCode = resultado.StatusCode } :
                    new ObjectResult(resultado.Pokemons) { StatusCode = resultado.StatusCode };
        });

        /// <summary>
        /// Obtêm um pokémon através de seu identificador
        /// </summary>
        /// <returns>Dados do pokémon</returns>
        [HttpGet("{identificador}")]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterAsync([FromRoute] int identificador) =>
        await TratarResultadoAsync(async () =>
        {
            var resultado = await _pokemonService.ObterAsync(identificador);

            return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
        });

        /// <summary>
        /// Adiciona um pokémon na lista de capturados
        /// </summary>        
        [HttpPut("{identificador}/capturados")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InserirCapturadoAsync([FromRoute] int identificador) =>
        await TratarResultadoAsync(async () =>
        {
            var resultado = await _pokemonService.InserirCapturadoAsync(identificador);

            return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
        });

        /// <summary>
        /// Lista os pokémons capturados
        /// </summary>        
        /// <returns>Lista com dados de pokémons capturados</returns>
        [HttpGet("capturados")]
        [ProducesResponseType(typeof(List<PokemonResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarCapturadosAsync([FromQuery] PaginacaoRequest requisicao) =>
        await TratarResultadoAsync(async () =>
        {
            var resultado = await _pokemonService.ListarCapturadosAsync(requisicao);

            return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
        });
    }
}