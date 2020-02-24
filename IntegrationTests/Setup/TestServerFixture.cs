using API;
using DepositSettlements.IntegrationTests.Extensions;
using DepositSettlements.IntegrationTests.XunitExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using Xunit.Abstractions;

namespace DepositSettlements.IntegrationTests.Setup
{
    public class TestServerFixture : IDisposable
    {
        public IHost Host { get; }
        public TestServer Server { get; }
        public HttpClient Client { get; }
        public ITestOutputHelper Output { get; }

        public TestServerFixture()
        {

        }

        public TestServerFixture(ITestOutputHelper output)
        {
            Output = output;

            var hostBuilder = new HostBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddXunit(Output);
                })
                .ConfigureWebHost(webHost =>
                {
                    webHost
                        .UseStartup<TestStartup>()
                        .BasedOn<Startup>() //Internal extension to re set the correct ApplicationKey
                        .UseTestServer();
                });

            Host = hostBuilder.Start();
            Server = Host.GetTestServer();
            Client = Host.GetTestClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
            Host.Dispose();
        }
    }
}
