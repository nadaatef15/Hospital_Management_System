using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class TestMapping
    {
        public static TestEntity ToEntity(this TestModel model) => new()
        {
             Name = model.Name,
             Price = model.Price,
        };

        public static TestResource ToResource(this TestEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
        };
    }
}
