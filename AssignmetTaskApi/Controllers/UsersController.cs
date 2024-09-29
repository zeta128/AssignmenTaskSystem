using AssignmetTaskApi.Common;
using AssignmetTaskApi.Domain.DTOs;
using AssignmetTaskApi.Domain.Entities;
using AssignmetTaskApi.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmetTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AssignmetTaskDBContext _dbContext;
        private readonly Utilities _utilities;

        public UsersController(AssignmetTaskDBContext dbContext, Utilities utilities)
        {
            _dbContext = dbContext;
            _utilities = utilities;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var modelUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password
            };
            await _dbContext.Users.AddAsync(modelUser);
            await _dbContext.SaveChangesAsync();
            if (modelUser.IdUser != 0)
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false });
        }
    }
}
