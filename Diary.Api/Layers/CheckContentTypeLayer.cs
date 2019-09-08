using System.Threading.Tasks;
using Diary.Api.Consts;
using Diary.Api.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Diary.Api.Layers
{
    public class CheckContentTypeLayer
    {


        private readonly RequestDelegate _next;

        public CheckContentTypeLayer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var contentType = context.Request.ContentType;

            if (contentType != "application/json")
            {
                var response = new ApiResponse<string>
                {
                    Data = ResponseMessages.InvalidContentType, Error = true, Message = ResponseMessages.Failed,
                    StatusCode = 400
                };
                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
                return;
            }

            await _next(context);
        }
    }
}