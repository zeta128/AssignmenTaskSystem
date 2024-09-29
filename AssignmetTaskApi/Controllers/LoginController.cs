using AssignmetTaskApi.Common;
using AssignmetTaskApi.Domain.DTOs;
using AssignmetTaskApi.Domain.Entities;
using AssignmetTaskApi.Infraestructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssignmetTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AssignmetTaskDBContext _dbContext;
        private readonly Utilities _utilities;

        public LoginController(AssignmetTaskDBContext dbContext, Utilities utilities)
        {
            _dbContext = dbContext;
            _utilities = utilities;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var userFound = await _dbContext.Users.Where(usr => usr.Email == loginDTO.Email && usr.Password == loginDTO.Password).FirstOrDefaultAsync();
            if (userFound == null)
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true, token = _utilities.generateJWT(userFound) });
        }
    }
}
