using HMSDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HMSDataAccess.Entity
{
    [PrimaryKey(nameof(TestId), nameof(MedicalRecordId))]
    public class MedicalRecordTests : ISoftDelete
    {
        [Key]
        public int TestId {  get; set; }
        public TestEntity Test {  get; set; }

        [Key]
        public int MedicalRecordId {  get; set; } 
        public MedicalRecordEntity MedicalRecord { get; set; }

        public string? LabTechnicianId {  get; set; }
        public LabTechnicianEntity LabTechnician { get; set; }  

        public string? TestResult {  get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public DateTime? ExecutedOn { get; set; }

    }
}
