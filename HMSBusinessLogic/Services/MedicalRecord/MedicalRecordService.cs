using HMSContracts.Model.MedicalRecord;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.MedicalRecord
{
    public interface IMedicalRecordService
    {
        void SetValues(MedicalRecordEntity entity, MedicalRecordModel model);
    }
    public class MedicalRecordService : IMedicalRecordService
    {
        public void SetValues(MedicalRecordEntity entity, MedicalRecordModel model)
        {
            entity.Treatment = model.Treatment;
            entity.Price = model.Price;
            entity.Note = model.Note;
            entity.AppointmentId = model.AppointmentId;
            entity.MedicalRecordDiagnoses = model.Diagnoses.Select(a => new MedicalRecordDiagnoses() { DiagnosesId = a }).ToList();

            ProcessMedicalRecordTests(entity, model);
        }

        private static void ProcessMedicalRecordTests(MedicalRecordEntity entity, MedicalRecordModel model)
        {
            var oldTests = entity.MedicalRecordTests.Select(a => a.TestId).ToList();
            var newTests = model.Tests;

            var testToBrRemoved = oldTests.Except(newTests).ToList();

            entity.MedicalRecordTests.RemoveAll(a => testToBrRemoved.Contains(a.TestId));

            var testsToBeAdded = newTests.Except(oldTests).ToList();

            entity.MedicalRecordTests.AddRange(testsToBeAdded
                .Select(a => new MedicalRecordTests() { TestId = a, MedicalRecordId = entity.Id }));
        }
    }
}
