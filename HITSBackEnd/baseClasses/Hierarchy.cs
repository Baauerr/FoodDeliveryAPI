namespace HITSBackEnd.baseClasses
{
    public class Hierarchy
    {
        public int Id {  get; set; }
        public int ObjectId { get; set; }
        public (?) ParentObjId {  get; set; }
        public bool IsActive {  get; set; }
    }
}
