using System.Security.Claims;
using Diary.Api.Helpers;
using Xunit;

namespace Diary.Tests.Units.DiaryApiTests.HelperTests
{
    public class TestsTokenHelper
    {

        [Fact]
        public void IsThereHasClaim_Return_False()
        {

            var response = TokenHelper.IsThereHasClaim(Consts.Token, ClaimTypes.Actor);

            Assert.False(response);
        }
    }
}