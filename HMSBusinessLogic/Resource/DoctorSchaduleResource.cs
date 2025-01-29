namespace HMSBusinessLogic.Resource
{
    public class DoctorScheduleResource
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string DoctorId {  get; set; }
    }
}
