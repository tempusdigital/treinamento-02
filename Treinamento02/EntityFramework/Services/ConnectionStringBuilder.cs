using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace EntityFramework.Services
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        readonly IConfiguration _configuration;
        readonly IActionContextAccessor _actionContextAccessor;
        readonly IMemoryCache _memoryCache;

        public ConnectionStringBuilder(IConfiguration configuration, IActionContextAccessor actionContextAccessor, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _actionContextAccessor = actionContextAccessor;
            _memoryCache = memoryCache;
        }

        public string ObterConnectionString()
        {
            var actionContext = _actionContextAccessor.ActionContext;

            var tenant = actionContext.RouteData.Values["tenant"].ToString();

            var key = $"{tenant}_connection_string";

            return _memoryCache.GetOrCreate(key, c => Obter(tenant));
        }

        private string Obter(string tenant)
        {
            var connectionString = _configuration.GetConnectionString("LojaContext");
            
            return $"{connectionString}Search Path={tenant}";
        }
    }
}
