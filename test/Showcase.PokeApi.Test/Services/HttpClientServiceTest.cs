using Showcase.PokeApi.Services;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.PokeApi.Test.Services
{
    [ExcludeFromCodeCoverage]
    public class HttpClientServiceTest : BaseTest
    {
        [Fact]
        public async Task EnviarAsync_QuandoExecutarGet_DeveProcessarComSucesso()
        {
            //Arrange
            var servico = new HttpClientService(_httpClient);
            var uri = "https://microsoft.com";
            var metodoHttp = HttpMethod.Get;

            //Act
            await servico.EnviarAsync(uri, metodoHttp);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task EnviarAsync_QuandoExecutarGetComCabecalhos_DeveProcessarComSucesso()
        {
            //Arrange
            var servico = new HttpClientService(_httpClient);
            var uri = "https://microsoft.com";
            var metodoHttp = HttpMethod.Get;
            var cabecalhos = new Dictionary<string, string>
            {
                { "Cache-Control", "no-cache" },
                { "Accept", "application/json"},
            };
            var dadosDaRequisicao = new { };

            //Act
            await servico.EnviarAsync(uri, metodoHttp, cabecalhos, dadosDaRequisicao);

            //Assert
            Assert.True(true);
        }
    }
}