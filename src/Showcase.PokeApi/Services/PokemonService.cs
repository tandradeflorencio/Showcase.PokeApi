using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using Showcase.PokeApi.Models;
using Showcase.PokeApi.Models.Requests;
using Showcase.PokeApi.Models.Responses;
using Showcase.PokeApi.Repositories.Interfaces;
using Showcase.PokeApi.Services.Interfaces;
using Showcase.PokeApi.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services
{
    public class PokemonService : IPokemonService
    {
        private const string PrefixoLog = $"{ConstantesUtil.PrefixoLog}{nameof(PokemonService)} -";

        private const int QuantidadeDePokemonsListados = 10;

        private readonly ILogger _logger;

        private readonly IHttpClientService _httpClientService;

        private readonly ITelemetriaService _telemetriaService;

        private readonly IConfiguration _configuration;

        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(ILogger logger, IHttpClientService httpClientService, ITelemetriaService telemetriaService, IConfiguration configuration, IPokemonRepository transacaoRepository)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _telemetriaService = telemetriaService;
            _configuration = configuration;
            _pokemonRepository = transacaoRepository;
        }

        public async Task<ListaDePokemonsResponse> ListarAsync()
        {
            try
            {
                _logger.Information($"{PrefixoLog} ({nameof(ListarAsync)}) Recebida nova requisição.");

                var quantidadeDePokemons = await ObterQuantidadeAsync();

                var listaDePokemons = new List<PokemonResponse>(quantidadeDePokemons);

                if (quantidadeDePokemons > 0)
                {
                    await ListarAsync(listaDePokemons, quantidadeDePokemons);
                }

                _logger.Information($"{PrefixoLog} ({nameof(ListarAsync)}) Encontrados {listaDePokemons.Count} pokémons.");

                return new ListaDePokemonsResponse
                {
                    Pokemons = listaDePokemons,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.Information($"{PrefixoLog} ({nameof(ListarAsync)}) Erro: ({ex.Message})");

                return new ListaDePokemonsResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Mensagem = "Não foi possível processar a requisição. Por favor, tente novamente após alguns segundos."
                };
            }
        }

        public async Task<BaseResponse> ObterAsync(int identificador)
        {
            const int valorDeIdentificadorMinimo = 1;

            try
            {
                _logger.Information($"{PrefixoLog} ({nameof(ObterAsync)}) ({identificador}) Recebida nova requisição.");

                if (identificador < valorDeIdentificadorMinimo)
                    return new BaseResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Mensagem = "Identificador inválido."
                    };

                var uri = $"{_configuration.GetValue<string>("PokeApi:UrlApi")}pokemon/{identificador}";

                var retorno = await _httpClientService.EnviarAsync(uri, HttpMethod.Get);
                PokemonResponse pokemon = null;

                if (retorno != null)
                {
                    var resposta = await retorno.Content.ReadAsStringAsync();

                    if (retorno.IsSuccessStatusCode)
                    {
                        var detalhe = JsonSerializer.Deserialize<Models.Responses.PokeApi.PokemonDetalheResponse>(resposta);

                        retorno = await _httpClientService.EnviarAsync(detalhe?.Imagens?.PadraoDeFrente, HttpMethod.Get);

                        if (retorno != null)
                        {
                            var imagem = await retorno.Content.ReadAsByteArrayAsync();
                            detalhe.Imagens.PadraoDeFrente = Convert.ToBase64String(imagem);

                            pokemon = PokemonResponse.Mapear(detalhe);

                            _logger.Information($"{PrefixoLog} ({nameof(ObterAsync)}) ({identificador}) Pokémon {pokemon.Nome} encontrado.");
                        }
                    }
                }

                if (pokemon == null)
                    return new BaseResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };

                return pokemon;
            }
            catch (Exception ex)
            {
                _logger.Information($"{PrefixoLog} ({nameof(ObterAsync)}) Erro: ({ex.Message})");

                return new BaseResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Mensagem = "Não foi possível processar a requisição. Por favor, tente novamente após alguns segundos."
                };
            }
        }

        public Task<BaseResponse> InserirCapturadoAsync(int identificador)
        {
            throw new NotImplementedException();
        }

        public Task<ListaDePokemonsResponse> ListarCapturadosAsync(PaginacaoRequest requisicao)
        {
            throw new NotImplementedException();
        }

        private async Task ListarAsync(List<PokemonResponse> listaDePokemons, int quantidadeDePokemons)
        {
            var offSet = ObterOffSet(quantidadeDePokemons);

            var uri = $"{_configuration.GetValue<string>("PokeApi:UrlApi")}pokemon?limit={QuantidadeDePokemonsListados}&offset={offSet}";

            var retorno = await _httpClientService.EnviarAsync(uri, HttpMethod.Get);

            if (retorno != null)
            {
                var resposta = await retorno.Content.ReadAsStringAsync();

                if (retorno.IsSuccessStatusCode)
                {
                    var lista = JsonSerializer.Deserialize<Models.Responses.PokeApi.ListaDePokemonsResponse>(resposta);

                    var tasks = new List<Task>(lista.Resultados.Count);

                    foreach (var item in lista.Resultados)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            retorno = await _httpClientService.EnviarAsync(item.Url, HttpMethod.Get);

                            if (retorno != null)
                            {
                                resposta = await retorno.Content.ReadAsStringAsync();

                                if (retorno.IsSuccessStatusCode)
                                {
                                    var detalhe = JsonSerializer.Deserialize<Models.Responses.PokeApi.PokemonDetalheResponse>(resposta);

                                    retorno = await _httpClientService.EnviarAsync(detalhe?.Imagens?.PadraoDeFrente, HttpMethod.Get);

                                    if (retorno != null)
                                    {
                                        var imagem = await retorno.Content.ReadAsByteArrayAsync();
                                        detalhe.Imagens.PadraoDeFrente = Convert.ToBase64String(imagem);
                                    }

                                    listaDePokemons.Add(PokemonResponse.Mapear(detalhe));
                                }
                            }
                        }));
                    }

                    Task.WaitAll([.. tasks]);
                }
            }
        }

        private static int ObterOffSet(int quantidadeDePokemons)
        {
            var offSet = 0;

            if (quantidadeDePokemons > QuantidadeDePokemonsListados)
            {
                var maximoOffSet = quantidadeDePokemons / QuantidadeDePokemonsListados;
                var random = new Random();
                offSet = random.Next(0, maximoOffSet);
            }

            return offSet;
        }

        private async Task<int> ObterQuantidadeAsync()
        {
            var quantidadeDePokemons = 0;
            var uri = $"{_configuration.GetValue<string>("PokeApi:UrlApi")}pokemon?limit=1&offset=1";

            var retorno = await _httpClientService.EnviarAsync(uri, HttpMethod.Get);

            if (retorno != null)
            {
                var resposta = await retorno.Content.ReadAsStringAsync();

                if (retorno.IsSuccessStatusCode)
                {
                    var listaDePokemons = JsonSerializer.Deserialize<Models.Responses.PokeApi.ListaDePokemonsResponse>(resposta);
                    quantidadeDePokemons = listaDePokemons.Quantidade;
                }
            }

            return quantidadeDePokemons;
        }        
    }
}