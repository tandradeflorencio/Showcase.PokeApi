using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using Showcase.PokeApi.Services.Interfaces;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MestresController : BaseController
    {
        private readonly IMestreService _mestreService;

        public MestresController(ILogger logger, IMestreService mestreService) : base(logger)
        {
            _mestreService = mestreService;
        }

        /// <summary>
        /// Adiciona um mestre pokémon
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InserirAsync([FromBody] InserirMestreRequest requisicao) =>
        await TratarResultadoAsync(async () =>
        {
            var resultado = await _mestreService.InserirAsync(requisicao);

            return new ObjectResult(resultado) { StatusCode = resultado.StatusCode };
        });
    }
}