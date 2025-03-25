using HMSBusinessLogic.Resource;
using HMSContracts.Model.Diagnose;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class DiagnoseMapping
    {
        public static DiagnosesEntity ToEntity(this DiagnoseModel model) => new()
        {
            Name=model.Name,
        };

        public static DiagnoseModel ToModel(this DiagnosesEntity entity) => new()
        {
            Name = entity.Name,
            Id = entity.Id
        };
        public static DiagnoseResource ToResource(this DiagnosesEntity entity) => new()
        {
            Name = entity.Name,
            Id = entity.Id
        };

    }
}
