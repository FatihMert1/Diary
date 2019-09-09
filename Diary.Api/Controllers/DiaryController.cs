using System.Threading.Tasks;
using Diary.Api.Consts;
using Diary.Api.Models;
using Diary.Business.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Api.Controllers
{
    [ApiController]
    [Route("diary")]
    public class DiaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ApiResponse<Data.Entities.Diary>>> Get(int id)
        {
            ApiResponse<Data.Entities.Diary> response = null;    
            var diary = _unitOfWork.DiaryRepository.Get(id);
            if (diary != null)
                response = new ApiResponse<Data.Entities.Diary>(diary, ResponseMessages.Success, 200, false);
            
            return response ?? new ApiResponse<Data.Entities.Diary>(null, ResponseMessages.NotFound, 400, true);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Data.Entities.Diary>>> Add([FromBody] Data.Entities.Diary diary)
        {
            ApiResponse<Data.Entities.Diary> response = null;
            if (diary == null)
                return new ApiResponse<Data.Entities.Diary>(null, ResponseMessages.Wrong, 400, true);

            _unitOfWork.DiaryRepository.Insert(diary);
            await _unitOfWork.SaveChangesAsync();

            var insertedDiary = _unitOfWork.DiaryRepository.Get(diary);
            response = new ApiResponse<Data.Entities.Diary>(insertedDiary, ResponseMessages.Success, 200, false);
            
            return response;
        }

        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<Data.Entities.Diary>>> Update([FromBody] Data.Entities.Diary diary)
        {
            if (diary == null)
                return new ApiResponse<Data.Entities.Diary>(null, ResponseMessages.Failed, 400, true);

            _unitOfWork.DiaryRepository.Update(diary);
            await _unitOfWork.SaveChangesAsync();

            var result = _unitOfWork.DiaryRepository.Get(diary);
            return new ApiResponse<Data.Entities.Diary>(result, ResponseMessages.Success, 200, false);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResponse<Data.Entities.Diary>>> Delete(int id)
        {
            _unitOfWork.DiaryRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<Data.Entities.Diary>(null, ResponseMessages.Success, 200, false);
        }
        
    }
}