using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class DoctorScheduleEntity : Trackable,ISoftDelete
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
