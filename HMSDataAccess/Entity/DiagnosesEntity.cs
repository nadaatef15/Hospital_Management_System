using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class DiagnosesEntity : Trackable , ISoftDelete
    {
        public int Id {  get; set; }    
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();
    }
}
