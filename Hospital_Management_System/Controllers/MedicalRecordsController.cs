using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.MedicalRecord;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.MedicalRecord;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;
namespace Hospital_Management_System.Controllers
{
    public class MedicalRecordsController : BaseController
    {
        private readonly IMedicalRecordsManager _medicalRecordManager;
        public MedicalRecordsController(IMedicalRecordsManager medicalRecordManager) =>
            _medicalRecordManager = medicalRecordManager;


        [HttpPost(Name = "CreateMedicalRecord")]
        [PermissionRequirement($"{Permission}.{MedicalRecord}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] MedicalRecordModel model)
        {
            await _medicalRecordManager.CreateMedicalRecord(model);
            return Created();
        }

        [HttpDelete("{Id}", Name = "DeleteMedicalRecord")]
        [PermissionRequirement($"{Permission}.{MedicalRecord}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMedicalRecord(int Id)
        {
            await _medicalRecordManager.DeleteMedicalRecord(Id);
            return NoContent();
        }

        [HttpPut("{Id}", Name = "UpdateMedicalRecord")]
        [PermissionRequirement($"{Permission}.{MedicalRecord}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMedicalRecord(int Id ,[FromBody] MedicalRecordModel model)
        {
            await _medicalRecordManager.UpdateMedicalRecord(Id,model);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetMedicalRecordById")]
        [PermissionRequirement($"{Permission}.{MedicalRecord}.{View}")]
        [ProducesResponseType(typeof(MedicalRecordResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicalRecordById(int Id)
        {
          var medicalRecord =  await _medicalRecordManager.GetMedicalRecordById(Id);
            return Ok(medicalRecord);
        }

        [HttpGet(Name ="GetAllMedicalRecords")]
        [PermissionRequirement($"{Permission}.{MedicalRecord}.{View}")]
        [ProducesResponseType(typeof(List<MedicalRecordResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var medicalRecords =await _medicalRecordManager.GetAllMedicalRecords();
            return Ok(medicalRecords);
        }
    }
}
