using HMSDataAccess.Model;

namespace HMSDataAccess.Entity
{
    public class PharmacistEntity : UserEntity
    {
        public List<PrescriptionEntity> Prescriptions { get; set; }= new List<PrescriptionEntity>();
        
    }
}
