using HITSBackEnd.Dto.AdressDTO;
using HITSBackEnd.Models.AddressModels;

namespace HITSBackEnd.Services.Adresses
{
    public interface IAddressRepository
    {

        public Task<List<AddressElementDTO>> GetBuilding(long parentObjId, string query);
        public Task<List<AddressElementDTO>> GetChain(string objectGuid);
    }
}
