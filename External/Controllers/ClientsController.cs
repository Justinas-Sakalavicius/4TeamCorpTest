using External.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using External.Services;

namespace External.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IClientService _clientService;

        public ClientsController(IConnectionMultiplexer redis, IClientService clientService)
        {
            _redis = redis;
            _clientService = clientService;
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{UserId}")]
        public async Task<IActionResult> Put([Required] string userId, [FromBody] ClientDto clientDto)
        {
            if(!await _clientService.ClientKeyExist(userId))
            {
                return BadRequest($"key: \"{userId}\" does not exist");
            }

            if (string.IsNullOrEmpty(clientDto.FirstName) || string.IsNullOrEmpty(clientDto.MiddleName) 
                || string.IsNullOrEmpty(clientDto.LastName) || clientDto.Age == null)
            {
                return BadRequest("Invalid request data");
            }

            await _clientService.UpdateClient(userId, clientDto);

            return Ok();
        }

        // GET api/<ClientsController>/5
        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get([Required] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            if (await _clientService.ClientKeyExist(userId))
            {
                return Ok(await _clientService.RetrievedCacheClient(userId));
            }
            var generatedClient = await _clientService.CreateClient(userId);

            return Ok(generatedClient);
        }
    }
}
