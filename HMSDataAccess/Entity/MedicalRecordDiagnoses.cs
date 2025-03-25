using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HMSDataAccess.Entity
{
    [PrimaryKey(nameof(MedicalRecordId), nameof(DiagnosesId))]
    public class MedicalRecordDiagnoses
    {
        [Key]
        public int MedicalRecordId { get; set; }
        public MedicalRecordEntity MedicalRecord { get; set; }

        [Key]
        public int DiagnosesId {  get; set; }
        public DiagnosesEntity Diagnoses { get; set; }

    }
}
