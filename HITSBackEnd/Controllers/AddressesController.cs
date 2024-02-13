using HITSBackEnd.Dto.AdressDTO;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Exceptions;
using HITSBackEnd.Services.Adress;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/adress")]
    [ApiController]
    [ProducesResponseType(typeof(RegistrationLoginResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseModel), 500)]
    public class AdressController : ControllerBase
    {
        private readonly IAddressRepository _adressRepository;

        public AdressController(IAddressRepository buildingRepository)
        {
            _adressRepository = buildingRepository;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(List<AddressElementDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetBuilding(long parentObjId, string? query)
        {
            var building = await _adressRepository.GetBuilding(parentObjId, query);
            return Ok(building);
        }

        [HttpGet("chain")]
        [ProducesResponseType(typeof(List<AddressElementDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetChain(string objectId)
        {
            var chain = await _adressRepository.GetChain(objectId);
            return Ok(chain);
        }
    }
}
