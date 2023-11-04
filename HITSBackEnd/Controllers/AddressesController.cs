using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Services.Adresses;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/adress")]
    [ApiController]
    [ProducesResponseType(typeof(RegistrationLoginResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseModel), 500)]
    public class AdressController : ControllerBase
    {
        private readonly IAdressRepository _adressRepository;

        public AdressController(IAdressRepository buildingRepository)
        {
            _adressRepository = buildingRepository;
        }

        [HttpGet("search")]
        public IActionResult GetBuilding(int parentObjId, string query)
        {
            var building = _adressRepository.GetBuilding(parentObjId, query);
            return Ok(building);
        }

        [HttpGet("chain")]
        [ProducesResponseType(typeof(RegistrationLoginResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public IActionResult GetChain(string objectId)
        {
            var chain = _adressRepository.GetChain(objectId);
            return Ok(chain);
        }
    }
}
