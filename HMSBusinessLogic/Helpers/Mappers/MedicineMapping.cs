using HMSBusinessLogic.Resource;
using HMSContracts.Model.Medicine;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class MedicineMapping
    {
        public static MedicineResource ToResource(this MedicineEntity entity) => new()
        {
            Id = entity.Id,
            Type=entity.Type,
            Name=entity.Name,
            Price=entity.Price,
            Amount=entity.Amount,

        };

        public static MedicineEntity ToEntity(this MedicineModel model) => new()
        {
            Type = model.Type,
            Name = model.Name,
            Price = model.Price,
            Amount = model.Amount,

        };

    }
}
