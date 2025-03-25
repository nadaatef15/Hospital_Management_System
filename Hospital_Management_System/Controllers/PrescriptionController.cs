using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Prescription;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Prescription;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{

    public class PrescriptionController : BaseController
    {
        public readonly IPrescriptionManager _prescriptionManager;
        public PrescriptionController(IPrescriptionManager prescriptionManager)=>
            _prescriptionManager = prescriptionManager;

        [HttpPost(Name = "CreatePrescription")]
        [PermissionRequirement($"{Permission}.{Prescription}.{Create}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionModel model)
        {
            await _prescriptionManager.CreatePrescription(model);
            return Created();
        }

        [HttpPut("Id", Name = "UpdatePrescription")]
        [PermissionRequirement($"{Permission}.{Prescription}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePrescription(int Id, PrescriptionModel model)
        {
            await _prescriptionManager.UpdatePrescription(Id, model);
            return NoContent();
        }

        [HttpGet("Id", Name = "GetPrescriptionById")]
        [PermissionRequirement($"{Permission}.{Prescription}.{View}")]
        [ProducesResponseType(typeof(PrescriptionResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionManager.GetPrescriptionById(id);
            return Ok(prescription);
        }

        [HttpGet(Name = "GetAllPrescriptions")]
        [PermissionRequirement($"{Permission}.{Prescription}.{View}")]
        [ProducesResponseType(typeof(List<PrescriptionResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptionManager.GetAllPrescriptions();
            return Ok(prescriptions);
        }

        [HttpDelete("Id", Name = "DeletePrescription")]
        [PermissionRequirement($"{Permission}.{Prescription}.{Delete}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            await _prescriptionManager.DeletePrescription(id);
            return NoContent();
        }

        [HttpPatch(("DispensePrescription"),Name = "HandlePrescriptionDispenseUpdate")]
        [PermissionRequirement($"{Permission}.{Prescription}.{Edit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> HandlePrescriptionDispenseUpdate(PrescriptionDispensedModel model)
        {
            await _prescriptionManager.HandlePrescriptionDispenseUpdate(model);
            return NoContent();
        }


    }
}
