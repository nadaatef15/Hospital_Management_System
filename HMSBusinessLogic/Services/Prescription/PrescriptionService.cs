using HMSContracts.Model.Prescription;
using HMSDataAccess.Model;

namespace HMSBusinessLogic.Services.Prescription
{
    public interface IPrescriptionService
    {
        void SetValue(PrescriptionEntity entity , PrescriptionModel model);
    }
    public class PrescriptionService : IPrescriptionService
    {
        public void SetValue(PrescriptionEntity entity, PrescriptionModel model)
        {
            entity.Quentity=model.Quantity;
            entity.Dosage=model.Dosage;
            entity.MedicineId=model.MedicineId;
            entity.MedicalRecordId=model.MedicalRecordId;
        }
    }
}
