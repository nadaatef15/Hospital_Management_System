using HMSBusinessLogic.Manager.Specialty;
using HMSContracts.Model.Specialty;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class SpecialtyController : BaseController
    {
        private readonly ISpecialtiesManager _specialtiesManager;
        public SpecialtyController( ISpecialtiesManager specialtiesManager)=>
        
            _specialtiesManager = specialtiesManager;

        [HttpPost(Name ="CreateSpecialty")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSpecalty(SpecialtyModel model)
        {
            await _specialtiesManager.CreateSpecialty(model);
              return Created();
        }

        [HttpPut(Name = "UpdateSpecialty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSpecalty( int id, SpecialtyModel model)
        {
            await _specialtiesManager.UpdateSpecialty(id,model);
            return NoContent();
        }

        [HttpGet("Id", Name = "GetSpecialtyById")]
        [ProducesResponseType(typeof(SpecialtyModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var specialty = await _specialtiesManager.GetSpecialityById(id);
            return Ok(specialty);
        }


        [HttpGet("GetAllSpecialties")]
        [ProducesResponseType(typeof(List<SpecialtyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSpecialties()
        {
            var result = await _specialtiesManager.GetAllSpecialities();
            return Ok(result);
        }


        [HttpDelete("id", Name = "DeleteSpecialtyById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePatientById(int id)
        {
            await _specialtiesManager.DeleteSpecialty(id);
            return NoContent();
        }
    }
}
