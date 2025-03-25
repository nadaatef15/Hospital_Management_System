using HMSBusinessLogic.Manager.Medicine;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Medicine;
using HMSContracts.Model.Specialty;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;

namespace Hospital_Management_System.Controllers
{
    public class MedicinesController : BaseController
    {
        public readonly IMedicineManager _medicineManager;
        public MedicinesController(IMedicineManager medicineManager) =>
            _medicineManager = medicineManager;

        [HttpPost(Name = "CreateMedicine")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMedicine(MedicineModel model)
        {
            await _medicineManager.CreateMedicine(model);
            return Created();
        }

        [HttpPut("{Id}",Name = "UpdateMedicine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMedicine(int Id, MedicineModel model)
        {
            await _medicineManager.UpdateMedicine(Id, model);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetMedicineById")]
        [ProducesResponseType(typeof(MedicineResource), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicineById(int Id)
        {
            var medicine = await _medicineManager.GetMedicineById(Id);
            return Ok(medicine);
        }


        [HttpGet(Name ="GetAllMedicines")]
        [ProducesResponseType(typeof(List<MedicineResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicines()
        {
            var result = await _medicineManager.GetAllMedicines();
            return Ok(result);
        }


        [HttpDelete("{Id}", Name = "DeleteMedicine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            await _medicineManager.DeleteMedicine(Id);
            return NoContent();
        }

    }
}
