using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HMSContracts.Model.MedicalRecord
{
    public class MedicalRecordModel
    {
        public int? Id { get; set; } 
        [Required]
        [MaxLength(500)]
        public string Treatment { get; set; }

        [Required]
        [Range(1,int.MaxValue , ErrorMessage ="the value must be positive")]
        [DefaultValue(1)]
        public int Price { get; set; }

        public string Note { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> Diagnoses { get; set; } = new List<int>();

        public List<int> Tests { get; set; }= new List<int>();
    }
}
