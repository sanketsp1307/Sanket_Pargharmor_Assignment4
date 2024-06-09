using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly SecurityServiceInterface _iSecurityService;
        public SecurityController(SecurityServiceInterface iSecurityService)
        {
            _iSecurityService = iSecurityService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurity(SecurityModel securityModel)
        {
            bool isExist = await _iSecurityService.IsSecurityExist(securityModel.Email);

            if (isExist)
            {
                return BadRequest("Data already Exists!!");
            }

            var response = await _iSecurityService.AddSecurity(securityModel);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetSecurityByUId(string userId)
        {
            var response = await _iSecurityService.GetSecurityByUId(userId);

            if (response == null)
            {
                return NotFound("Unable to find data!!");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSecurity(SecurityModel securityModel)
        {
            var isExist = await _iSecurityService.GetSecurityByUId(securityModel.UserId);

            if (isExist == null)
            {
                return BadRequest("Unable to find data!!");
            }

            var response = await _iSecurityService.UpdateSecurity(securityModel);

            return Ok(response);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteSecurityByUId(string userId)
        {
            var isExist = await _iSecurityService.GetSecurityByUId(userId);

            if (isExist == null)
            {
                return NotFound("Unable to find data!!");
            }

            var response = await _iSecurityService.DeleteSecurityByUId(userId);

            return Ok(response);
        }
    }
}
