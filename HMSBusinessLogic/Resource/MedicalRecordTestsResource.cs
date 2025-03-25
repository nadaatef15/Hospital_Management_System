using System.ComponentModel.DataAnnotations;

namespace HMSBusinessLogic.Resource
{
    public class MedicalRecordTestsResource
    { 
        public int TestId { get; set; }
     
        public int MedicalRecordId { get; set; }

        public string? TestResult { get; set; }

        public DateTime? ExecutedOn { get; set; }

        public string? ExecutedBy { get; set; }
    }
}
