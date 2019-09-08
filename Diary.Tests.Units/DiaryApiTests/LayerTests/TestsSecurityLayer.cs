using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Diary.Api.Consts;
using Diary.Api.Helpers;
using Diary.Api.Layers;
using Diary.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Xunit;

namespace Diary.Tests.Units.DiaryApiTests.LayerTests
{
    public class TestsSecurityLayer
    {
        private readonly SecurityLayer _layer;
        private readonly HttpContext _context = null;

        public TestsSecurityLayer()
        {
            Task Delegate(HttpContext httpContext) => Task.FromResult(0);
            _layer = new SecurityLayer(Delegate);
            _context = new DefaultHttpContext();
        }

        [Fact]
        public async Task Return_Invalid_Type()
        {

            _context.Request.Path = "/diary";
            _context.Request.Headers["x-api-key"] = Consts.Token;
            await _layer.Invoke(_context);

            Assert.Empty(_context.User.Claims.Where(c => c.Type == ClaimTypes.Email));
        }

        [Fact]
        public async Task Invoke_ShouldReturnInvalidContentType_NullParameter()
        {
            var apiResponse =
                new ApiResponse<string>(ResponseMessages.NotFoundApiKey, ResponseMessages.Wrong, 400, true);
            var apiResponseAsString = JsonConvert.SerializeObject(apiResponse);
            var responseBodyAsString = await GetResponseBodyAsString();

            Assert.NotEmpty(responseBodyAsString);
            Assert.Equal(apiResponseAsString, responseBodyAsString);

        }

        [Fact]
        public async Task Invoke_ReturnInvalidHeaderType_LessXApiKeyClaims()
        {
            _context.Request.Headers["x-api-key"] = Consts.NoCountryToken;

            var expectedApiResponse =
                new ApiResponse<string>(ResponseMessages.LessClaim, ResponseMessages.Failed, 400, true);
            var expectedAsString = JsonConvert.SerializeObject(expectedApiResponse);
            var response = await GetResponseBodyAsString();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.Equal(expectedAsString, response);
        }

        [Fact]
        public async Task Invoke_AllBeFine()
        {
            _context.Request.Headers["x-api-key"] = Consts.FineToken;
            await _layer.Invoke(_context);
            
            var email = _context.User.Claims.First(c => c.Type == ClaimTypes.Email);
            var country = _context.User.Claims.First(c => c.Type == ClaimTypes.Country);
            var name = _context.User.Claims.First(c => c.Type == ClaimTypes.Name);
            
            Assert.NotNull(email);
            Assert.NotNull(country);
            Assert.NotNull(name);
            Assert.Equal("testing@test.com",email.Value);
            Assert.Equal("Fatih Rahman Mert", name.Value);
            Assert.Equal("Turkey", country.Value);
        }

    private async Task<string> GetResponseBodyAsString()
        {
            using (var responseBody = new MemoryStream())
            {
                _context.Response.Body = responseBody;
                await _layer.Invoke(_context);
                return  await FormatResponse(_context.Response);
            }
        }

    private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}