using HMSBusinessLogic.Resource;
using HMSContracts.Model.Prescription;
using HMSDataAccess.Model;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class PrescriptionMapping
    {
        public static PrescriptionEntity ToEntity(this PrescriptionModel model) => new()
        {
            Dosage = model.Dosage,
            Quentity=model.Quantity,
            MedicalRecordId = model.MedicalRecordId,
            MedicineId = model.MedicineId,   
        };

        public static PrescriptionResource ToResource(this PrescriptionEntity entity) => new()
        {
            Id = entity.Id,
            Dosage = entity.Dosage,
            Quantity = entity.Quentity,
            MedicalRecord = entity.MedicalRecord?.ToResource(),
            Medicine = entity.Medicine?.ToResource(),
            DispinsedBy = entity.Pharmasist?.ToResource(),
            DispinsedOn = entity?.DispinsedOn,
        };
    }
}
