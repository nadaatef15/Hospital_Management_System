using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class AppointmentMapping
    {
        public static AppointmentEntity ToEntity(this AppointmentModel model) => new()
        {
            Date = model.Date,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            ReasonOfVisit = model.ReasonOfVisit,
            Status = model.Status,
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
        };

        public static AppointmentResource ToResource(this AppointmentEntity entity) => new()
        {
            Date = entity.Date,
            SartTime = entity.StartTime,
            EndTime = entity.EndTime,
            ReasonOfVisit = entity.ReasonOfVisit,
            Doctor = entity.Doctor?.ToResource(),
            Patient = entity.Patient?.ToPatientResource(),
            Status = entity.Status,
        };

    }
}
