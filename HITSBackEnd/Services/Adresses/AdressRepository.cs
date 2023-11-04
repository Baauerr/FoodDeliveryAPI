using HITSBackEnd.Dto.AdressDTO;

namespace HITSBackEnd.Services.Adresses
{
    public class AdressRepository : IAdressRepository
    {
        public List<AddressElementDTO> GetBuilding(int parentObjId, string query)
        {
            throw new NotImplementedException();
        }

        public List<AddressElementDTO> GetChain(string objectGuid)
        {
            throw new NotImplementedException();
        }
    }
}
