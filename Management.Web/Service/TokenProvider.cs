﻿using Management.Web.Service.IService;
using Management.Web.Utility;
using Newtonsoft.Json.Linq;

namespace Management.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetails.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken= _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            //to set the token value in cookie so that other API can access it 
            _contextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token);
        }
    }
}
