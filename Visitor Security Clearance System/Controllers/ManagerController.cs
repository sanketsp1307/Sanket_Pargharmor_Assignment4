using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _iManagerService;
        public ManagerController(IManagerService iManagerService)
        {
            _iManagerService = iManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(ManagerModel managerModel)
        {
            var isExist = await _iManagerService.IsManagerExist(managerModel.Email);

            if (isExist)
            {
                return BadRequest("Data already Exists!!");
            }

            ManagerModel response = await _iManagerService.AddManager(managerModel);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetManagerByUId(string userId)
        {
            var response = await _iManagerService.GetManagerByUId(userId);

            if (response == null)
            {
                return NotFound("Unable to find data!!");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManager(ManagerModel managerModel)
        {
            var isExist = await _iManagerService.GetManagerByUId(managerModel.UserId);

            if (isExist == null)
            {
                return BadRequest("Unable to find data!!");
            }

            var response = await _iManagerService.UpdateManager(managerModel);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteManagerByUId(string userId)
        {
            var isExist = await _iManagerService.GetManagerByUId(userId);

            if (isExist == null)
            {
                return NotFound("Unable to find data!!");
            }

            var response = await _iManagerService.DeleteManagerByUId(userId);

            return Ok(response);
        }
    }
}
