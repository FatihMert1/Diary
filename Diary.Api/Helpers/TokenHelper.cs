using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Diary.Api.Helpers
{
    public static class TokenHelper
    {
        public static bool IsThereHasClaim(string header, params string[] claims)
        {
            if (string.IsNullOrEmpty(header))
                return false;
            try
            {
                var token = new JwtSecurityToken(jwtEncodedString: header);
                return claims.All(item => token.Claims.Any(s => s.Type == item) != false);
            }
            catch (Exception e)
            {
                throw;
                return false;
            }
        }

        public static void InjectClaim(string enCodedToken ,Dictionary<string,string> dict)
        {
            if(!IsThereHasClaim(enCodedToken,dict.Keys.ToArray()))
                throw new ArgumentException("The Token no contain  Keys  ");
            //Console.WriteLine();

            var token = new JwtSecurityToken(enCodedToken);

            foreach (var claim in token.Claims)
            {
                dict[claim.Type] = claim.Value;
            }
        }
    }
}