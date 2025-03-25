using HMSDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using static HMSContracts.Constants.SysEnums;

namespace HMSDataAccess.Entity
{
    public class AppointmentEntity : Trackable ,ISoftDelete
    {
        public int Id { get; set; } 

        public DateOnly Date {  get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }   

        public string ReasonOfVisit { get; set; }

        public Status Status { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public string DoctorId {  get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public DoctorEntity Doctor { get; set; }  

        public string PatientId {  get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PatientEntity Patient { get; set; }

    }
}
