﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FunctionalityController : Controller
    {
        private readonly FunctionalityServiceInterface _iFunctionalityService;
        public FunctionalityController(FunctionalityServiceInterface iFunctionalityService)
        {
            _iFunctionalityService = iFunctionalityService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPass(PassModel passModel)
        {
            var response = await _iFunctionalityService.AddPass(passModel);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApprovalStatusOfPass(string PassId, bool IsApproved)
        {
            var response = await _iFunctionalityService.UpdateApprovedStatusOfPass(PassId, IsApproved);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorsByStatusAndOfficeId(string PassId, bool IsApproved)
        {
            var response = await _iFunctionalityService.GetVisitorsByStatusAndOfficeId(PassId, IsApproved);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string role)
        {
            var response = await _iFunctionalityService.Login(email, password, role);

            if (!response)
            {
                return Unauthorized();
            }

            return Ok("Logged in Successfully!!");
        }
    }
}
