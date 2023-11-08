using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.AdressDTO;
using HITSBackEnd.Models.AddressModels;
using HITSBackEnd.Repository.Adress;
using Microsoft.EntityFrameworkCore;


namespace HITSBackEnd.Services.Adresses
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressesDbContext _db;
        public AddressRepository(AddressesDbContext addressesDbContext) {
            _db = addressesDbContext;
        }
        public async Task<List<AddressElementDTO>> GetBuilding(long parentObjId, string? query)
        {
            var queryResult = await (from hierarchy in _db.AsAdmHierarchy
                               where hierarchy.parentobjid == parentObjId
                               join adm in _db.AsAddrObjects on hierarchy.objectId equals adm.objectId into admJoin 
                               from adm in admJoin.DefaultIfEmpty()
                               join house in _db.AsHouses on hierarchy.objectId equals house.objectId into houseJoin
                               from house in houseJoin.DefaultIfEmpty()
                               where (adm != null && adm.isActive == 1)
                                || (house != null && house.isActive == 1)
                                && (adm.name.Contains(query) || house.houseNum.Contains(query) || query == null)
                               select new AddressElementDTO
                               {
                                   objectId = house != null ? house.objectId : (adm != null ? adm.objectId : 0),
                                   objectGuid = house != null ? house.objectGuid : (adm != null ? adm.objectGuid : Guid.Empty),
                                   text = house != null ? house.houseNum : (adm != null ? adm.typename + " " + adm.name : string.Empty),
                                   objectLevel = house != null
                                       ? ((HousesLevel)house.houseType).ToString()
                                       : ((AddressLevel)int.Parse(adm.level)).ToString(),
                                   objectLevelText = house != null
                                       ? AddressesHelper.GetDescription((HousesLevel)house.houseType)
                                       : AddressesHelper.GetDescription((AddressLevel)int.Parse(adm.level))
                               }).ToListAsync();
            return queryResult;
        }

        public async Task<List<AddressElementDTO>> GetChain(string objectGuid)
        {
            throw new NotImplementedException();
        }
    }
}
