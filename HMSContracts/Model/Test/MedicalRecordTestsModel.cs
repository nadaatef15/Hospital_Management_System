using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Test
{
    public class MedicalRecordTestsModel
    {
        [Required]
        public int TestId { get; set; }

        [Required]
        public int MedicalRecordId { get; set; }

        [Required]
        [MinLength(1)]
        public string TestResult { get; set; }

    }
}
