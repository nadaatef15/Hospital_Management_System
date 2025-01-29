using HMSBusinessLogic.Resource;
using HMSContracts.Model.DoctorSchadule;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.Doctor
{
    public interface IDoctorScheduleService
    {
        void SetValues(DoctorScheduleEntity doctorSchedule, DoctorScheduleModel resource);
    }
    public class DoctorScheduleService : IDoctorScheduleService
    {
        public void SetValues(DoctorScheduleEntity entity , DoctorScheduleModel model)
        {
            entity.Id = model.Id;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.DoctorId = model.DoctorId;   
            entity.Date = model.Date;   
        }
    }
}
