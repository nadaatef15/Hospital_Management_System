using HMSDataAccess.Entity;
using HMSDataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSDataAccess.Model
{
    public class PrescriptionEntity : Trackable, ISoftDelete
    {
        public int Id { get; set; }
        public string Dosage {  get; set; }
        public DateTime Date {  get; set; } = DateTime.Now;
        public int Quentity { get; set; }

        public int MedicalRecordId { get; set; }
        public MedicalRecordEntity MedicalRecord {  get; set; }

        public int MedicineId {  get; set; }        
        public MedicineEntity Medicine { get; set; }

        [ForeignKey(nameof(Pharmasist))]       
        public string? DispinsedById {  get; set; }   
        public PharmacistEntity? Pharmasist { get; set; }

        public DateTime? DispinsedOn {  get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
