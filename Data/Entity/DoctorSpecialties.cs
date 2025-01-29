using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSDataAccess.Entity
{
    [PrimaryKey(nameof(DoctorId), nameof(SpecialtiesId))]
    public class DoctorSpecialties
    {
        [Key]
        [Column(Order = 1)]
        public string DoctorId {  get; set; }

        [Key]
        [Column(Order =2)]
        public int SpecialtiesId {  get; set; }
    }
}
