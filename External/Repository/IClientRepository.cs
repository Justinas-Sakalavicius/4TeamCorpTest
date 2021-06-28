using External.Dto;
using System.Threading.Tasks;

namespace External.Repository
{
    public interface IClientRepository
    {
        Task<bool> CheckIfKeyExist(string userId);
        Task<ClientDto> CreateClient(string userId);
        Task<ClientDto> RetrieveClient(string userId);
        Task UpdateClient(string userId, ClientDto clientDto);
    }
}
