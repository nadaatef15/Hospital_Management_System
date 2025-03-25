using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class MedicalRecordTestsMapping
    {
        public static MedicalRecordTests toEntity(this MedicalRecordTestsModel model) => new()
        {
            TestId=model.TestId,
            MedicalRecordId=model.MedicalRecordId,
            TestResult=model.TestResult,
        };

        public static MedicalRecordTestsResource ToResource(this MedicalRecordTests entity) => new()
        {
            TestId = entity.TestId,
            MedicalRecordId = entity.MedicalRecordId,
            ExecutedOn = entity.ExecutedOn,
            TestResult = entity.TestResult,
            ExecutedBy=entity.LabTechnicianId,
        };
    }
}
