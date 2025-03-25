using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class MedicineEntity : Trackable , ISoftDelete
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public int Price { get; set; }

        public string Type {  get; set; }

        public int Amount {  get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
