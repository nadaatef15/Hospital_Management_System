using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Doctor;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.DoctorSchadule;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;
namespace Hospital_Management_System.Controllers
{
    public class DoctorScheduleController : BaseController
    {
        private readonly IDoctorScheduleManager _doctorScheduleManager;
        public DoctorScheduleController(IDoctorScheduleManager doctorScheduleManager) =>
            _doctorScheduleManager = doctorScheduleManager;


        [HttpPost(Name = "CreateDoctorSchedule")]
       // [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDoctorSchedule(DoctorScheduleModel model)
        {
            await _doctorScheduleManager.CreateDoctorSchedule(model);
            return Created();
        }

        [HttpGet("DoctorId", Name = "GetDoctorSchedules")]
       // [PermissionRequirement($"{Permission}.{DoctorSchedule}.{View}")]
        [ProducesResponseType(typeof(List<DoctorScheduleResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSchedulesForDoctor(string DoctorId, DateOnly? dateFrom , DateOnly? dateTo)
        {
            var docSchedules = await _doctorScheduleManager.GetDoctorSchedulesForDoctor(DoctorId , dateFrom , dateTo);
            return Ok(docSchedules);
        }


        [HttpPut("ScheduleId", Name = "UpdateDoctorSchedule")]
      //  [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDoctorSchedule(int ScheduleId, DoctorScheduleModel model)
        {
            await _doctorScheduleManager.UpdateDoctorSchedule(ScheduleId, model);
            return NoContent();
        }

        [HttpDelete("Id", Name = "DeleteDoctorSchedule")]
       // [PermissionRequirement($"{Permission}.{DoctorSchedule}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctorSchedule(int id)
        {
            await _doctorScheduleManager.DeleteDoctorSchedule(id);
            return NoContent();
        }
    }
}
