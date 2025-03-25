using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Diagnose;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Diagnose;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class DiagnosesController : BaseController
    {
        public readonly IDiagnoseManager _diagnoseManager;
        public DiagnosesController(IDiagnoseManager diagnoseManager) =>
            _diagnoseManager = diagnoseManager;

        [HttpPost(Name = "CreateDiagnose")]
        [PermissionRequirement($"{Permission}.{Diagnoses}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDiagnose(DiagnoseModel model)
        {
            await _diagnoseManager.CreateDiagnose(model);
            return Created();
        }

        [HttpPut("{Id}", Name = "UpdateDiagnose")]
        [PermissionRequirement($"{Permission}.{Diagnoses}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDiagnose(int Id, DiagnoseModel model)
        {
            await _diagnoseManager.UpdateDiagnose(Id, model);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetDiagnoseById")]
        [PermissionRequirement($"{Permission}.{Diagnoses}.{View}")]
        [ProducesResponseType(typeof(DiagnoseResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientById(int Id)
        {
            var diagnose = await _diagnoseManager.GetDiagnoseById(Id);
            return Ok(diagnose);
        }


        [HttpGet(Name ="GetAllDiagnoses")]
        [PermissionRequirement($"{Permission}.{Diagnoses}.{View}")]
        [ProducesResponseType(typeof(List<DiagnoseResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDiagnoses()
        {
            var diagnoses = await _diagnoseManager.GetAllDiagnoses();
            return Ok(diagnoses);
        }


        [HttpDelete("{Id}", Name = "DeleteDiagnoseById")]
        [PermissionRequirement($"{Permission}.{Diagnoses}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDiagnoseById(int Id)
        {
            await _diagnoseManager.DeleteDiagnose(Id);
            return NoContent();
        }
    }
}
