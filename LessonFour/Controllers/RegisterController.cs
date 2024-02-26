using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Requests;
using LessonFour.Abstractions;
using LessonFour.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LessonFour.Controllers
{
    
    [Route("registration")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthorizService _service;

        public RegisterController(IAuthorizService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost(template: "login")]
        public async Task<IResult> Login([FromBody]UserAuthorizationRequest request)
        {
            var result = await _service.Login(request);
            return result;
        }
        [AllowAnonymous]
        [HttpPost(template: "register")]
        public async Task<IResult> Register([FromBody]UserAuthorizationRequest request)
        {
            var result = await _service.Register(request);
            return result;
        }
       
        [Authorize(Roles = "Administrator, User")]
        [HttpGet(template: "secret - user")]
        public IActionResult SecretUser()
        {
            return Ok("Success");
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet(template: "secret - administrator")]
        public IActionResult SecretAdministrator()
        {
            return Ok("Success");
        }

    }
}

