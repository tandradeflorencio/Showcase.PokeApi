using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using Serilog;
using Showcase.PokeApi.Models;
using System;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly string _prefixoLog = "Showcase.PokeApi -";

        protected readonly ILogger _logger;

        private readonly string MensagemErroPadrao = "Ocorreu um erro ao processar a solicitação. Por favor, tente novamente mais tarde.";

        protected BaseController(ILogger logger) => _logger = logger;

        protected async Task<IActionResult> TratarResultadoAsync(Func<Task<IActionResult>> servico)
        {
            try
            {
                return await servico();
            }
            catch (BrokenCircuitException ex)
            {
                _logger.Error(ex, $"{_prefixoLog} {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status502BadGateway, new BaseResponse { Mensagem = "Quantidade de requisições extrapoladas ou parceiro indiponível." });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"{_prefixoLog} {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { Mensagem = MensagemErroPadrao });
            }
        }
    }
}