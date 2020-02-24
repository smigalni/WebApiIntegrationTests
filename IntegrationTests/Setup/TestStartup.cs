using API;
using Microsoft.Extensions.Configuration;

namespace DepositSettlements.IntegrationTests.Setup
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
