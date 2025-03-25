namespace HMSBusinessLogic.Resource
{
    public class PrescriptionResource
    {
        public int? Id { get; set; }

        public string Dosage { get; set; }

        public int Quantity { get; set; }

        public MedicalRecordResource? MedicalRecord { get; set; }

        public MedicineResource? Medicine { get; set; }

        public DateTime? DispinsedOn { get; set; }

        public UserResource? DispinsedBy { get; set; }
    }
}
