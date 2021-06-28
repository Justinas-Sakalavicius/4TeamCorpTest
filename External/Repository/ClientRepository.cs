using External.Dto;
using External.Generator;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace External.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabaseAsync _database;
        private readonly IClientGenerator _clientGenerator;
        private readonly TimeSpan _cacheKeyExpirationTime;

        public ClientRepository(IConnectionMultiplexer redis, IClientGenerator clientGenerator, IConfiguration configuration)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
            _clientGenerator = clientGenerator;
            _cacheKeyExpirationTime = new(configuration.GetValue<int>("CacheKeyExpirationTimeInTicks"));
        }

        public async Task<bool> CheckIfKeyExist(string userId)
        {
            return await _database.KeyExistsAsync(userId);
        }

        public async Task<ClientDto> CreateClient(string userId)
        {
            var generatedClient = CreateClient();

            await _database.ListLeftPushAsync(userId, SerializeClient(generatedClient));
            await AddKeyExpireAsync(userId);

            return generatedClient;
        }

        public async Task<ClientDto> RetrieveClient(string userId)
        {
            return DeserializeClient(await _database.ListRightPopLeftPushAsync(userId, userId));
        }

        public async Task UpdateClient(string userId, ClientDto clientDto)
        {
            await _database.ListLeftPopAsync(userId);
            await _database.ListLeftPushAsync(userId, SerializeClient(clientDto));
            await AddKeyExpireAsync(userId);
        }

        private async Task AddKeyExpireAsync(string userId)
        {
            await _database.KeyExpireAsync(userId, _cacheKeyExpirationTime);
        }

        private ClientDto CreateClient()
        {
            return _clientGenerator.Generate();
        }

        private string SerializeClient(ClientDto clientDto)
        {
            return JsonConvert.SerializeObject(clientDto);
        }

        private ClientDto DeserializeClient(string data)
        {
            return JsonConvert.DeserializeObject<ClientDto>(data);
        }
    }
}
