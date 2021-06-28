using External.Dto;
using System.Threading.Tasks;

namespace External.Services
{
    public interface IClientService
    {
        Task<ClientDto> CreateClient(string userId);
        Task<bool> ClientKeyExist(string key);
        Task<ClientDto> RetrievedCacheClient(string userId);
        Task UpdateClient(string userId, ClientDto clientDto);
    }
}
