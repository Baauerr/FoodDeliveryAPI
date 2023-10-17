namespace HITSBackEnd.baseClasses
{
    public class AdressElement
    {
        public Guid id { get; set; }
        public Guid objectId { get; set; }
        public string objectGuid { get; set; }
        public string name { get; set; }
        public string typename { get; set; }
        public int level { get; set; }
        public bool isActive { get; set; }
    }
}
