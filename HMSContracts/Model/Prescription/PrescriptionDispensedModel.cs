using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Prescription
{
    public class PrescriptionDispensedModel
    {
        [Required]
        public int Id { get; set; }
    }
}
