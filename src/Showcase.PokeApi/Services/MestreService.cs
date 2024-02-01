using Microsoft.AspNetCore.Http;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Entities;
using Showcase.PokeApi.Models.Requests;
using Showcase.PokeApi.Models.Responses;
using Showcase.PokeApi.Repositories.Interfaces;
using Showcase.PokeApi.Services.Interfaces;
using Showcase.PokeApi.Utils;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services
{
    public class MestreService : IMestreService
    {
        private const string PrefixoLog = $"{ConstantesUtil.PrefixoLog}{nameof(MestreService)} -";

        private readonly ILogger _logger;

        private readonly IMestreRepository _mestreRepository;

        public MestreService(ILogger logger, IMestreRepository mestreRepository)
        {
            _logger = logger;
            _mestreRepository = mestreRepository;
        }

        public async Task<BaseResponse> InserirAsync(InserirMestreRequest requisicao)
        {
            try
            {
                const int MinimoValorDeIdentificador = 1;

                _logger.Information($"{PrefixoLog} ({nameof(InserirAsync)}) Recebida nova requisição ({JsonSerializer.Serialize(requisicao)}).");

                var mestre = await _mestreRepository.ObterAsync(Mestre.Mapear(requisicao));

                if (mestre != null)
                {
                    var resposta = InserirMestreResponse.Mapear(mestre);

                    resposta.StatusCode = StatusCodes.Status200OK;

                    return resposta;
                }

                mestre = Mestre.Mapear(requisicao);

                await _mestreRepository.InicializarTabelaAsync();

                mestre = await _mestreRepository.InserirAsync(mestre);

                if (mestre.Identificador < MinimoValorDeIdentificador)
                    return new BaseResponse
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                        Mensagem = "Não foi possível processar a requisição. Por favor, tente novamente após alguns segundos."
                    };

                _logger.Information($"{PrefixoLog} ({nameof(InserirAsync)}) ({mestre.Identificador}) Mestre inserido.");

                var respostaInserir = InserirMestreResponse.Mapear(mestre);

                respostaInserir.StatusCode = StatusCodes.Status201Created;

                return respostaInserir;
            }
            catch (Exception ex)
            {
                _logger.Information($"{PrefixoLog} ({nameof(InserirAsync)}) Erro: ({ex.Message})");

                return new BaseResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Mensagem = "Não foi possível processar a requisição. Por favor, tente novamente após alguns segundos."
                };
            }
        }
    }
}