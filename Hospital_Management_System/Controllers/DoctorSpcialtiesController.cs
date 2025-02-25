using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Specialty;
using HMSDataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorSpcialtiesController : BaseController
    {
        private readonly IDoctorSpecialtiesManager _doctorSpecialtiesManager;
        public DoctorSpcialtiesController(IDoctorSpecialtiesManager doctorSpecialtiesManager)=>
            _doctorSpecialtiesManager = doctorSpecialtiesManager;

        [HttpPost(Name = "AddDoctorSpecialty")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDoctorSpecialty(DoctorSpecialtyModel doctorSpecialties)
        {
           await _doctorSpecialtiesManager.AddDoctorSpeciality(doctorSpecialties);
            return Created();
        }

        [HttpDelete(Name = "DeleteDoctorSpecialty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctorSpeciality(DoctorSpecialtyModel doctorSpecialty)
        {
            await _doctorSpecialtiesManager.DeleteDoctorSpecialty(doctorSpecialty);
            return NoContent(); 
        }

        [HttpGet(Name ="DoctorHasSpecialties")]
        [ProducesResponseType(typeof(List<DoctorSpecialtyResource>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorSpecialties(string docId)
        {
           var docSpecialty= await _doctorSpecialtiesManager.GetDoctorSpecialties(docId);
            return Ok(docSpecialty);
        }

    }
}
