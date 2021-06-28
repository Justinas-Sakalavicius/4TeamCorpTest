using External.Dto;
using External.Repository;
using System.Threading.Tasks;

namespace External.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> ClientKeyExist(string key)
        {
            return await _clientRepository.CheckIfKeyExist(key);
        }

        public async Task<ClientDto> RetrievedCacheClient(string userId)
        {
            return await _clientRepository.RetrieveClient(userId);
        }

        public async Task<ClientDto> CreateClient(string userId)
        {
            return await _clientRepository.CreateClient(userId);
        }

        public async Task UpdateClient(string userId, ClientDto clientDto)
        {
            await _clientRepository.UpdateClient(userId, clientDto);
        }
    }
}
