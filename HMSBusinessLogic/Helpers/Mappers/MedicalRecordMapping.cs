using HMSBusinessLogic.Resource;
using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class MedicalRecordMapping
    {
        public static MedicalRecordResource ToResource(this MedicalRecordEntity entity) => new()
        {
            Id = entity.Id,
            Treatment = entity.Treatment,
            Price = entity.Price,
            Note = entity.Note,
            Patient = entity.Patient?.ToPatientResource(),
            Doctor = entity.Doctor?.ToResource(),
            Appointment = entity.Appointment?.ToResource(),
            Diagnoses = entity.MedicalRecordDiagnoses.Select(a => a.Diagnoses.ToResource()).ToList(),
            Tests = entity.MedicalRecordTests.Select(a => a.Test.ToResource()).ToList(),
        };

        public static MedicalRecordEntity ToEntity(this MedicalRecordModel model) => new()
        {
            Treatment = model.Treatment,
            Price = model.Price,
            Note = model.Note,
            AppointmentId = model.AppointmentId,
            MedicalRecordDiagnoses = model.Diagnoses.Select(a => new MedicalRecordDiagnoses { DiagnosesId = a }).ToList(),
            MedicalRecordTests = model.Tests.Select(a => new MedicalRecordTests { TestId = a }).ToList(),
        };

    }
}
