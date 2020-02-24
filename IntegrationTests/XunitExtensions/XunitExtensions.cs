using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace DepositSettlements.IntegrationTests.XunitExtensions
{
    public static class XunitExtensions
    {
        public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper output)
        {
            builder.Services.AddSingleton<ILoggerProvider>(new XunitLoggerProvider(output));
            return builder;
        }
    }
}
