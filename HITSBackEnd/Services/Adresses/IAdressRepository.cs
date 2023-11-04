using HITSBackEnd.Dto.AdressDTO;

namespace HITSBackEnd.Services.Adresses
{
    public interface IAdressRepository
    {
        public List<AddressElementDTO> GetBuilding(int parentObjId, string query);
        public List<AddressElementDTO> GetChain(string objectGuid);
    }
}
