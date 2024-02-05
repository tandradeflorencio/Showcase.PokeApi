using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Services;
using Showcase.PokeApi.Utils;
using System;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController(ILogger logger) : ControllerBase
    {
        private const string PrefixoLog = $"{ConstantesUtil.PrefixoLog}{nameof(BaseController)} -";

        protected async Task<IActionResult> TratarResultadoAsync(Func<Task<IActionResult>> servico)
        {
            try
            {
                return await servico();
            }
            catch (BrokenCircuitException ex)
            {
                logger.Error(ex, $"{PrefixoLog} {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status502BadGateway, new BaseResponse { Mensagem = "Quantidade de requisições extrapoladas ou parceiro indiponível." });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{PrefixoLog} {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Mensagem = ConstantesUtil.MensagemErroPadrao });
            }
        }
    }
}