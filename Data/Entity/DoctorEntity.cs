namespace HMSDataAccess.Entity
{
    public class DoctorEntity : UserEntity
    {
        public List<DoctorSpecialties> DoctorSpecialties { get; set; } = new List<DoctorSpecialties>();
        public List<AppointmentEntity> Appointments { get; set; } = new List<AppointmentEntity>();
        public List<DoctorScheduleEntity> Schedules { get; set; } = new List<DoctorScheduleEntity>();
        public int? Salary { get; set; }
    }
}
