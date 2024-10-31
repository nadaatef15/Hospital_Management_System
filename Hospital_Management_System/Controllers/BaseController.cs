using HMSBusinessLogic.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{

    [Route("HMS/[controller]")]
    [ApiController]
    [GlobalExceptionHandler]
    public class BaseController : ControllerBase
    {
    }
}
