namespace HITSBackEnd.Models.AddressModels
{
    public class AsHouses
    {
        public long id {  get; set; }
        public long objectId { get; set; }
        public Guid objectGuid { get; set; }
        public int houseType { get; set; }
        public string houseNum { get; set; }
        public int isActual { get; set; }
        public int isActive { get; set; }
    }
}
