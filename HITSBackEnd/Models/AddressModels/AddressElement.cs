namespace HITSBackEnd.Models.AddressModels
{
    public class AsAddrObject
    {
        public long id { get; set; }
        public long objectId { get; set; }
        public Guid objectGuid { get; set; }
        public string name { get; set; }
        public string typename { get; set; }
        public string level { get; set; }
        public int isActive { get; set; }
        public int isActual { get; set; }
    }
}
