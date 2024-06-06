using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Service;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _iVisitorService;
        public VisitorController(IVisitorService iVisitorService)
        {
            _iVisitorService = iVisitorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitorAndSendMailToManager(VisitorModel visitorModel)
        {
            bool isExist = await _iVisitorService.IsVisitorExist(visitorModel.Email);

            if (isExist)
            {
                return BadRequest("User already exists!!");
            }

            var response = await _iVisitorService.AddVisitorAndSendMailToManager(visitorModel);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetVisitorByUId(string userId)
        {
            var response = await _iVisitorService.GetVisitorByUId(userId);

            if (response == null)
            {
                return NotFound("Unable to find data!!");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorModel visitorModel)
        {
            var isExist = await _iVisitorService.GetVisitorByUId(visitorModel.UserId);

            if (isExist == null)
            {
                return BadRequest("Unable to find data!!");
            }

            var response = await _iVisitorService.UpdateVisitor(visitorModel);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteVisitorByUId(string userId)
        {
            var isExist = await _iVisitorService.GetVisitorByUId(userId);

            if (isExist == null)
            {
                return NotFound("Unable to find data!!");
            }

            var response = await _iVisitorService.DeleteVisitorByUId(userId);

            return Ok(response);
        }
    }
}
