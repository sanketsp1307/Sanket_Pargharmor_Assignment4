using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly OfficeServiceInterface _iOfficeService;
        public OfficeController(OfficeServiceInterface iOfficeService)
        {
            _iOfficeService = iOfficeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(OfficeModel officeModel)
        {
            bool isExist = await _iOfficeService.IsOfficeExist(officeModel.Email);

            if (isExist)
            {
                return BadRequest("Data already exists!!");
            }

            var response = await _iOfficeService.AddOffice(officeModel);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetOfficeByUId(string userId)
        {
            var response = await _iOfficeService.GetOfficeByUId(userId);

            if (response == null)
            {
                return NotFound("Unable to find data!!");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOffice(OfficeModel officeModel)
        {
            var isExist = await _iOfficeService.GetOfficeByUId(officeModel.UserId);

            if (isExist == null)
            {
                return BadRequest("Unable to find data!!");
            }

            var response = await _iOfficeService.UpdateOffice(officeModel);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteOfficeByUId(string userId)
        {
            var isExist = await _iOfficeService.GetOfficeByUId(userId);

            if (isExist == null)
            {
                return NotFound("Unable to find data!!");
            }

            var response = await _iOfficeService.DeleteOfficeByUId(userId);

            return Ok(response);
        }

    }
}
