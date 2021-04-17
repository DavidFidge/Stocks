using Microsoft.Extensions.Configuration;

using StocksApi.Controllers;

namespace StocksApi
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}