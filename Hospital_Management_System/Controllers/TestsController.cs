using HMSBusinessLogic.Filter;
using HMSBusinessLogic.Manager.Test;
using HMSBusinessLogic.Resource;
using HMSContracts.Model.Test;
using Microsoft.AspNetCore.Mvc;
using static HMSContracts.Constants.SysConstants;


namespace Hospital_Management_System.Controllers
{
    public class TestsController : BaseController
    {
        public readonly ITestManager _testManager;
        public TestsController(ITestManager testManager)=>
            _testManager = testManager;


        [HttpPost(Name = "CreateTest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [PermissionRequirement($"{Permission}.{Test}.{Create}")]
        public async Task<IActionResult> CreateTest(TestModel model)
        {
            await _testManager.CreateTest(model);
            return Created();
        }

        [HttpPut("{Id}",Name = "UpdateTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [PermissionRequirement($"{Permission}.{Test}.{Edit}")]
        public async Task<IActionResult> UpdateTest(int Id, TestModel model)
        {
            await _testManager.UpdateTest(Id, model);
            return NoContent();
        }

        [HttpGet("{Id}", Name = "GetTestById")]
        [ProducesResponseType(typeof(TestResource), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Test}.{View}")]
        public async Task<IActionResult> GetTestById(int Id)
        {
            var Test = await _testManager.GetTestById(Id);
            return Ok(Test);
        }


        [HttpGet(Name ="GetAllTests")]
        [ProducesResponseType(typeof(List<TestResource>), StatusCodes.Status200OK)]
        [PermissionRequirement($"{Permission}.{Test}.{View}")]
        public async Task<IActionResult> GetAllTests()
        {
            var result = await _testManager.GetAllTest();
            return Ok(result);
        }


        [HttpDelete("{Id}", Name = "DeleteTestById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [PermissionRequirement($"{Permission}.{Test}.{Delete}")]
        public async Task<IActionResult> DeleteTestById(int Id)
        {
            await _testManager.DeleteTest(Id);
            return NoContent();
        }
    }
}
