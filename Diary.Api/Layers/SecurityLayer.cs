using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Diary.Api.Consts;
using Diary.Api.Helpers;
using Diary.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using static Diary.Api.Consts.ResponseMessages;

namespace Diary.Api.Layers
{
    public class SecurityLayer
    {
        private readonly RequestDelegate _next;

        public SecurityLayer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiKey = context.Request.Headers["x-api-key"].ToString();
            if (string.IsNullOrEmpty(apiKey))
            {
                var response = new ApiResponse<string>(NotFoundApiKey, Wrong, 400, true);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }

            var dict = new Dictionary<string,string>();
            

            var hasClaims = TokenHelper.IsThereHasClaim(apiKey, ClaimTypes.Email, ClaimTypes.Country, ClaimTypes.Name);
            
            if (hasClaims)
            {
                dict[ClaimTypes.Name] = "";
                dict[ClaimTypes.Country] = "";
                dict[ClaimTypes.Email] = "";
                TokenHelper.InjectClaim(apiKey, dict);
                var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email,dict[ClaimTypes.Email]),
                    new Claim(ClaimTypes.Country,dict[ClaimTypes.Country]),
                    new Claim(ClaimTypes.Name,dict[ClaimTypes.Name])
                });
                context.User.AddIdentity(identity);
            }
            else
            {
                var response = new ApiResponse<string>(LessClaim, Failed, 400, true);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }

            await _next(context);
        }
    }
}