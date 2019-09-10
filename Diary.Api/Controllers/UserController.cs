using System.Threading.Tasks;
using Diary.Api.Consts;
using Diary.Api.Models;
using Diary.Business.Services.Abstractions;
using Diary.Business.UOW;
using Diary.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Api.Controllers
{
    [ApiController]
    [Route("user")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get/{id}")]
        public async Task<ApiResponse<User>> Get(int id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            return new ApiResponse<User>(user, ResponseMessages.Success, 200, false);
        }

        [HttpPost("add")]
        public async Task<ApiResponse<User>> Add([FromBody] User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.SaveChangesAsync();

            var insertedUser = _unitOfWork.UserRepository.Get(user);
            return new ApiResponse<User>(insertedUser, ResponseMessages.Success, 200, false);
        }

        /// Delete Method Is Not Working, Because User and Diary Depends on DeleteBehavior.Restrict 
        [HttpDelete("delete/{id}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            _unitOfWork.UserRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessages.Success, 200, false);
        }
    }
}