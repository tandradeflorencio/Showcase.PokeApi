using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Showcase.PokeApi.Configurations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Showcase.PokeApi.Test.Configurations
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddlewareConfigurationTest
    {
        private ErrorHandlingMiddlewareConfiguration _error;

        private RequestDelegate _next;

        private ILoggerFactory _loggerFactory;

        private HttpContext _http;

        [Fact]
        public void UseErrorHandling_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            var application = Substitute.For<IApplicationBuilder>();

            //Act
            application.UtilizarManipulacaoDeErros();

            //Assert
            application.Should().NotBeNull();
        }

        [Fact]
        public async Task InvokeAsync_QuandoExecutar_DeveFinalizarSemErros()
        {
            //Arrange
            _next = Substitute.For<RequestDelegate>();
            _loggerFactory = Substitute.For<ILoggerFactory>();
            _error = new ErrorHandlingMiddlewareConfiguration(_next, _loggerFactory);

            //Act
            await _error.InvokeAsync(default);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task InvokeAsync_QuandoNextForNulo_DeveRetornarException()
        {
            //Arrange
            _http = Substitute.For<HttpContext>();
            _loggerFactory = Substitute.For<ILoggerFactory>();
            _error = new ErrorHandlingMiddlewareConfiguration(_next, _loggerFactory);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotSupportedException>(() => _error.InvokeAsync(_http));
        }
    }
}