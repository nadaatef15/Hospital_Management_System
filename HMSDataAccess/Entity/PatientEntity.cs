namespace HMSDataAccess.Entity
{
    public class PatientEntity : UserEntity
    {
        public string BloodGroup { get; set; }
        public List<string>? Allergies { get; set; }
        public List<string>? MedicalHistory { get; set; }

        public List<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
        public List<MedicalRecordEntity> MedicalRecords { get; set; } = new List<MedicalRecordEntity>();
        public List<InvoiceEntity> Invoices { get; set; } = new List<InvoiceEntity>();
    }
}
