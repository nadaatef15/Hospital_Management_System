using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Specialty;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class SpecialtiesController : BaseController
    {
        private readonly ISpecialtiesManager _specialtiesManager;
        public SpecialtiesController(ISpecialtiesManager specialtiesManager) =>

            _specialtiesManager = specialtiesManager;

        [HttpPost(Name = "CreateSpecialty")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [PermissionRequirement($"{Permission}.{Specialty}.{Create}")]
        public async Task<IActionResult> CreateSpecalty(SpecialtyModel model)
        {
            await _specialtiesManager.CreateSpecialty(model);
            return Created();
        }

        [HttpPut("{Id}",Name = "UpdateSpecialty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [PermissionRequirement($"{Permission}.{Specialty}.{Edit}")]
        public async Task<IActionResult> UpdateSpecialty(int Id, SpecialtyModel model)
        {
            await _specialtiesManager.UpdateSpecialty(Id,model);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetSpecialtyById")]
        [ProducesResponseType(typeof(SpecialtyResource), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Specialty}.{View}")]
        public async Task<IActionResult> GetPatientById(int Id)
        {
            var specialty = await _specialtiesManager.GetSpecialityById(Id);
            return Ok(specialty);
        }


        [HttpGet(Name = "GetAllSpecialties")]
        [ProducesResponseType(typeof(List<SpecialtyResource>), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Specialty}.{View}")]
        public async Task<IActionResult> GetAllSpecialties()
        {
            var result = await _specialtiesManager.GetAllSpecialities();
            return Ok(result);
        }


        [HttpDelete("{Id}", Name = "DeleteSpecialtyById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [PermissionRequirement($"{Permission}.{Specialty}.{Delete}")]
        public async Task<IActionResult> DeletePatientById(int Id)
        {
            await _specialtiesManager.DeleteSpecialty(Id);
            return NoContent();
        }
    }
}
