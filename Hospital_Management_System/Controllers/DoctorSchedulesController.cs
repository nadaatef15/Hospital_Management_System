using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.DoctorSchadule;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;
namespace Hospital_Management_System.Controllers
{
    public class DoctorSchedulesController : BaseController
    {
        private readonly IDoctorScheduleManager _doctorScheduleManager;
        public DoctorSchedulesController(IDoctorScheduleManager doctorScheduleManager) =>
            _doctorScheduleManager = doctorScheduleManager;


        [HttpPost(Name = "CreateDoctorSchedule")]
        [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDoctorSchedule(DoctorScheduleModel model)
        {
            await _doctorScheduleManager.CreateDoctorSchedule(model);
            return Created();
        }

        [HttpGet("GetDoctorSchedulesByDoctorId", Name = "GetDoctorSchedules")]
        [PermissionRequirement($"{Permission}.{DoctorSchedule}.{View}")]
        [ProducesResponseType(typeof(List<DoctorScheduleResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSchedulesForDoctor(string DoctorId, DateOnly? dateFrom , DateOnly? dateTo)
        {
            var docSchedules = await _doctorScheduleManager.GetDoctorSchedulesForDoctor(DoctorId , dateFrom , dateTo);
            return Ok(docSchedules);
        }


        [HttpPut("{Id}", Name = "UpdateDoctorSchedule")]
        [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDoctorSchedule(int Id, DoctorScheduleModel model)
        {
            await _doctorScheduleManager.UpdateDoctorSchedule(Id, model);
            return NoContent();
        }

        [HttpDelete("{Id}", Name = "DeleteDoctorSchedule")]
        [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctorSchedule(int Id)
        {
            await _doctorScheduleManager.DeleteDoctorSchedule(Id);
            return NoContent();
        }
    }
}
