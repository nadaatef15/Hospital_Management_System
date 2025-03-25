using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSDataAccess.Entity
{
    [PrimaryKey(nameof(DoctorId), nameof(SpecialtyId))]
    public class DoctorSpecialties
    {
        [Column(Order = 1)]
        public string DoctorId {  get; set; }
        public DoctorEntity Doctor { get; set; }


        [Column(Order =2)]
        public int SpecialtyId {  get; set; }
        public SpecialtyEntity Specialty { get; set; }
    }
}
