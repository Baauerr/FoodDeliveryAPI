using HITSBackEnd.DataBase;
using Microsoft.Owin.BuilderProperties;

namespace HITSBackEnd.DataValidation
{
    public class AddressValidator
    {

        private readonly AddressesDbContext _db;

        public AddressValidator(AddressesDbContext addressesDbContext) {
            _db = addressesDbContext;
        }

        public bool isAddressExist(Guid addressGuid)
        {
                var addressNormal = (_db.AsAddrObjects.Where(a => a.objectGuid == addressGuid && a.isActual == 1)).Any() ||
                                   (_db.AsHouses.Where(h => h.objectGuid == addressGuid && h.isActive == 1).Any());
            return addressNormal;
        }
    }
}
