using HMSBusinessLogic.Resource;
using HMSContracts.Model.DoctorSchadule;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class DoctorScheduleMapping
    {
        public static DoctorScheduleEntity ToEntity(this DoctorScheduleModel model) => new()
        {
            StartTime = model.StartTime,
            EndTime= model.EndTime,
            Date= model.Date,
            DoctorId=model.DoctorId,
            
        };

        public static DoctorScheduleResource ToResource(this DoctorScheduleEntity entity) => new()
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Date = entity.Date,
            DoctorId = entity.DoctorId,

        };
    }
}
