namespace HMSDataAccess.Entity
{
    public class LabTechnicianEntity : UserEntity
    {
        public List<MedicalRecordTests> MedicalRecordTests { get; set; } = new List<MedicalRecordTests>();
    }
}
