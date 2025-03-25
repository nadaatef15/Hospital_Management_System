using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class TestEntity : Trackable , ISoftDelete
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedOn { get ; set ; }
        public string? DeletedBy { get ; set ; }

        public List<MedicalRecordTests> medicalRecordTests= new List<MedicalRecordTests>();
    }
}
