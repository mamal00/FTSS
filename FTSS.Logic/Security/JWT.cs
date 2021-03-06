﻿using FTSS.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    public class JWT : IToken<UserInfo>
    {
        private const string key = "501b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        private readonly UserInfo _user;
        public UserInfo User
        {
            get
            {
                return _user;
            }
        }

        public static Logic.Security.UserInfo GetUserModel()
        {
			try
			{
                var userModel = new Logic.Security.JWT(GetJWTToken());
                if (userModel == null || !userModel.IsValid())
                    return new Logic.Security.UserInfo();

                return userModel.User;
            }
            catch(Exception ex)
			{
                return new Logic.Security.UserInfo();
            }
        }
        private static string GetJWTToken()
        {
            try
            {
                IHttpContextAccessor ctx = new HttpContextAccessor();
                var headers = ctx.HttpContext.Request.Headers["Authorization"];
                if (headers.Count == 0)
                    //در صورتی که نتوانستی اطلاعات را بدست بیاوری، با حروف کوچک امتحان کن
                    headers = ctx.HttpContext.Request.Headers["authorization"];

                if (headers.Count == 0)
                    return null;

                var header = headers.FirstOrDefault();
                string token = "";
                if (header != null)
                {
                    token = header.Replace("Bearer ", "").Replace("bearer ", "");
                }
           
                return (token);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private UserInfo GetData(string token)
        {
            var validateToken = GetPrincipal(token);

            if (validateToken != null)
            {
                var model = new UserInfo();
                if (validateToken.Identity != null && !string.IsNullOrEmpty(validateToken.Identity.Name))
                {
                    model.Username = validateToken.Identity.Name;
                    var UserId = getValueFromClaim(validateToken.Claims, "UserId");
                    if (UserId != null)
                        model.User.UserId = int.Parse(UserId);

                    model.User.FirstName = getValueFromClaim(validateToken.Claims, "FirstName");
                    model.User.LastName = getValueFromClaim(validateToken.Claims, "LastName");
                    model.User.Token = getValueFromClaim(validateToken.Claims, "token");
                    var accessMenuJSON = getValueFromClaim(validateToken.Claims, "AccessMenu");
                    if (!string.IsNullOrEmpty(accessMenuJSON) && accessMenuJSON != "null")
                        model.AccessMenu = CommonOperations.JSON.jsonToT<List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>>(accessMenuJSON);

                    return model;
                }
            }

            return null;
        }

        /// <summary>
        /// Generate JWT token from UserInfo object
        /// </summary>
        /// <param name="data">UserInfo object</param>
        /// <returns>JWT token which is a string</returns>
        public string GenerateToken(UserInfo data)
        {
            var user = data.User;
            var accessMenu = data.AccessMenu;
            var accessMenuJSON = CommonOperations.JSON.ObjToJson(accessMenu);

            var symmetricKey = Convert.FromBase64String(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Token", user.Token),
                    new Claim("AccessMenu", accessMenuJSON),
                    new Claim("scope", Guid.NewGuid().ToString()),
                }),
                Expires = user.ExpireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }


        public bool IsValid()
        {
            if (_user == null || _user.User == null || _user.User.UserId <= 0)
                return false;
            
            return true;
        }

        public JWT()
        {
        }

        public JWT(string token)
        {
            this._user = GetData(token);
        }


       

        private ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = Convert.FromBase64String(key);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }

        /// <summary>
        /// Get a value from Claim by it's name
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string getValueFromClaim(IEnumerable<Claim> claims, string name)
        {
            try
            {
                var item = claims.FirstOrDefault(a => a.Type.ToLower().Equals(name.ToLower()));
                return (item.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
