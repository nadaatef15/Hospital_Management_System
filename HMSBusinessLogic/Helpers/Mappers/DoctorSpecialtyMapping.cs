using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class DoctorSpecialtyMapping
    {
        public static DoctorSpecialties ToEntity(this DoctorSpecialtyModel model) => new()
        {
            DoctorId = model.DoctorId,
            SpecialtyId = model.SpecialtyId,
        };

        public static DoctorSpecialtyResource ToResource(this DoctorSpecialties entity) => new()
        {
            DoctorId=entity.DoctorId,    
            SpecialtyId=entity.SpecialtyId,  
        };

     }
}
