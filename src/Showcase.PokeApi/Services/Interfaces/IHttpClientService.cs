using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> EnviarAsync(string uri, HttpMethod metodoHttp, Dictionary<string, string> cabecalhos = null, object dadosDaRequisicao = null);
    }
}