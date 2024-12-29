namespace AIMS.Models.Entities
{
    public class Ward
    {
        public string Id { get; set; }  
        public string Name { get; set; } 

        public string DistrictId { get; set; }  
        public District District { get; set; }  
    }
}
