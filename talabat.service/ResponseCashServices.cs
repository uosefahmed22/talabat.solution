using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.core.services;

namespace talabat.service
{
    public class ResponseCashServices : IResponseCashServices
    {
        private readonly IDatabase _database; 
        public ResponseCashServices(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CashResponseAsync(string CashKey, object Response, TimeSpan TimeAlive)
        {
            if(Response == null) return;
            var Options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonserlized = JsonSerializer.Serialize(Response, Options); 
            await _database.StringSetAsync(CashKey, jsonserlized, TimeAlive);
        }

        public async Task<string> GetCashedResponseAsync(string CashKey)
        {
         var CashedResponse= await _database.StringGetAsync(CashKey);
            if (CashedResponse.IsNullOrEmpty) return null;
            return CashedResponse;

        }
    }
}
