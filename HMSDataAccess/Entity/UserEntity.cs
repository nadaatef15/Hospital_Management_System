using HMSDataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using static HMSContracts.Constants.SysEnums;

namespace HMSDataAccess.Entity
{
    public class UserEntity :  IdentityUser , ISoftDelete , ITrackable
    {
        public string Address { get; set; }

        public string? ImagePath { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get ; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get ; set ; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
