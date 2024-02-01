using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using System;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MestresController : BaseController
    {
        public MestresController(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        /// Adiciona um meste pokémon
        /// </summary>        
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InserirAsync([FromBody] InserirMestreRequest requisicao)
        {
            throw new NotImplementedException();
        }
    }
}