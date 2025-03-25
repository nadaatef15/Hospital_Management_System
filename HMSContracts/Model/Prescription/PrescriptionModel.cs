using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Prescription
{
    public class PrescriptionModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Dosage { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "the value must be positive")]
        [DefaultValue(1)]
        public int Quantity { get; set; }

        public int MedicalRecordId { get; set; }

        public int MedicineId { get; set; }      
    }
}
