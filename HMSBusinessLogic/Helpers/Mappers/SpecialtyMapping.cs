using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class SpecialtyMapping
    {
        public static SpecialtyEntity ToEntity(this SpecialtyModel model) => new()
        {
            Name = model.Name,
        };

        public static SpecialtyResource ToResource(this SpecialtyEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
