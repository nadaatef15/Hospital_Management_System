using HMSContracts.Model.Users;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Resource
{
    public class DoctorResource
    {
        public string Id { get; set; }

        public int? Salary { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }
        public List<SpecialtyResource> DoctorSpecialties { get; set; } = new List<SpecialtyResource>();
    }
}
