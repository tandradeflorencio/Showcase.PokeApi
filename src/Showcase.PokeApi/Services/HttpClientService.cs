using Showcase.PokeApi.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Showcase.PokeApi.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> EnviarAsync(string uri, HttpMethod metodoHttp, Dictionary<string, string> cabecalhos = null, object dadosDaRequisicao = null)
        {
            using var request = new HttpRequestMessage(metodoHttp, uri);

            if (cabecalhos != null)
                foreach (var item in cabecalhos)
                    request.Headers.Add(item.Key, item.Value);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (dadosDaRequisicao != null)
                using (request.Content = new StringContent(JsonSerializer.Serialize(dadosDaRequisicao), Encoding.UTF8, "application/json"))
                    return await _httpClient.SendAsync(request);

            return await _httpClient.SendAsync(request);
        }
    }
}