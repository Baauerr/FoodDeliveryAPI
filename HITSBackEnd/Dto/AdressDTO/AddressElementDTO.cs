namespace HITSBackEnd.Dto.AdressDTO
{
    public class AddressElementDTO
    {
        public long objectId { get; set; }
        public Guid objectGuid {  get; set; }
        public string text {  get; set; }
        public string objectLevel { get; set; }
        public string objectLevelText {  get; set; }
    }
}
