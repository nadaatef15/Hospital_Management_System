using HMSContracts.CustomValidation;
using HMSContracts.Model.Identity;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Users
{
    public class DoctorModel : UserModel
    {
        public int? Salary { get; set; }

        [Required]
        [NotRepeated]
        public List<int> DoctorSpecialtiesIds { get; set; } = [];
    }
}
