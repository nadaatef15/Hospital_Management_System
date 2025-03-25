using HMSContracts.Model.Medicine;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Services.Medicine
{
    public interface IMedicineService
        {
            void SetValues(MedicineEntity entity, MedicineModel model);
        }
    public class MedicineService : IMedicineService
    {     
            public void SetValues(MedicineEntity entity, MedicineModel model)
            {
               entity.Price = model.Price;
               entity.Type = model.Type;
               entity.Amount = model.Amount;
               entity.Name = model.Name;
            }
       
    }
}
