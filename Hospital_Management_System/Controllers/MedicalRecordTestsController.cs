using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Test;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class MedicalRecordTestsController : BaseController
    {
        public readonly IMedicalRecordTestsManager _medicalRecordManager;

        public MedicalRecordTestsController(IMedicalRecordTestsManager medicalRecordManager)=>
            _medicalRecordManager = medicalRecordManager;

        [HttpPut("ExecuteTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [PermissionRequirement($"{Permission}.{MedicalRecordTest}.{Edit}")]
        public async Task<IActionResult> ExecuteMedicalRecordTest(MedicalRecordTestsModel model)
        {
            await _medicalRecordManager.ExecuteMedicalRecordTest(model);
            return NoContent();
        }

        [HttpGet("GetMedicalRecordTests", Name = "GetAllMedicalRecordTests")]
        [PermissionRequirement($"{Permission}.{MedicalRecordTest}.{View}")]
        [ProducesResponseType(typeof(List<MedicalRecordTestsResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicalRecordTests(int medicalRecordId)
        {
            var medicalRecords = await _medicalRecordManager.GetMedicalRecordTests(medicalRecordId);
            return Ok(medicalRecords);
        }

        [HttpGet("GetPatientMedicalRecordTests", Name = "GetAllMedicalRecordTestsForPatient")]
        [PermissionRequirement($"{Permission}.{MedicalRecordTest}.{View}")]
        [ProducesResponseType(typeof(List<MedicalRecordTestsResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicalRecordTestsForPatient(string patientId)
        {
            var medicalRecords = await _medicalRecordManager.GetMedicalRecordTestForPatient(patientId);
            return Ok(medicalRecords);
        }
    }
}
