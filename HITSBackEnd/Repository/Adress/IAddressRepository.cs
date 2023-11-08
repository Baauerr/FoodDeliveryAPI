using HITSBackEnd.Dto.AdressDTO;
using HITSBackEnd.Models.AddressModels;

namespace HITSBackEnd.Services.Adresses
{
    public interface IAddressRepository
    {

        public List<AddressElementDTO> GetBuilding(long parentObjId, string query);
        public List<AddressElementDTO> GetChain(string objectGuid);
    }
}
