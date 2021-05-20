using FTSS.Logic.CommonOperations;
using FTSS.Models.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Security
{
    public class JWT
    {

        private readonly UserInfo _user;
        public UserInfo User
        {
            get
            {
                return _user;
            }
        }

        /// <summary>
        /// Generate token validation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        public static TokenValidationParameters GetTokenValidationParameters(string key, string issuer)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer))
                throw new ArgumentNullException("key and issuer could not be empty. Check application settings.");

            var rst = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(key))
            };

            return rst;
        }

        /// <summary>
        /// Get authentication event handler by JWT
        /// </summary>
        /// <returns></returns>
        public static JwtBearerEvents GetJWTEvents()
        {
            var rst = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");

                    return Task.CompletedTask;
                }
            };

            return rst;
        }

        public static Logic.Security.UserInfo GetUserModel(string keyValue,string issuerValue)
        {
			try
			{
                var userModel = new Logic.Security.JWT(GetJWTToken(), keyValue, issuerValue);
                if (userModel == null || !userModel.IsValid())
                    return new Logic.Security.UserInfo();

                return userModel.User;
            }
            catch(Exception ex)
			{
                throw new Exception(ex.Message);
            }
        }
        private static string GetJWTToken()
        {
            try
            {
                IHttpContextAccessor ctx = new HttpContextAccessor();
                if(ctx.HttpContext==null)
				{
                    throw new Exception("Context Is Null");
				}
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private UserInfo GetData(string token,string keyValue,string issuerValue)
        {
            var validateToken = GetPrincipal(token, keyValue,issuerValue);

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
                    model.User.Token = getValueFromClaim(validateToken.Claims, "Token");
                    model.User.Codemeli = getValueFromClaim(validateToken.Claims, "Codemeli");
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
        public static DBResult GenerateToken(UserInfo data,string keyValue,string issuerValue)
        {
            var symmetricKey = Convert.FromBase64String(keyValue);
            var tokenHandler = new JwtSecurityTokenHandler();
            string prs_no = "";
            string mobile = "";
            if(!string.IsNullOrEmpty(data.Prs_no))
			{
                prs_no = data.Prs_no;
			}
            if(string.IsNullOrEmpty(data.Mobile))
			{
                mobile = data.Mobile;
			}
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,data.Email),
                    new Claim("FirstName",data.FirstName),
                    new Claim("LastName",data.LastName),
                    new Claim("Codemeli",data.Codemeli),
                    new Claim("UserId",data.UserId.ToString()),
                    new Claim("Token",data.Token),
                    new Claim("PrsNo",prs_no),
                    new Claim("Mobile",mobile),
                    new Claim("AccessMenu", data.accessMenuJson),
                    new Claim("scope", Guid.NewGuid().ToString()),
                }),
                Issuer=issuerValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
            };
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return new DBResult(200,"", new { token, data.Prs_no,data.Mobile,data.Codemeli,data.FirstName,data.LastName } ,1);
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

        public JWT(string token,string keyValue,string issuerValue)
        {
            this._user = GetData(token, keyValue,issuerValue);
        }

        private static string GetClaimsFromContext(ClaimsPrincipal user, string key)
        {
            if (user == null)
                return null;
            var item = user.FindFirst(key);
            if (item == null)
                return null;
            return item.Value;
        }

        public static UserInfo GetUserInfoFromContext( ClaimsPrincipal user)
        {
            string accessMenuJson = GetClaimsFromContext(user, "AccessMenu");
            var reponse = new UserInfo
            {
                Username = GetClaimsFromContext(user, "UserId"),
                FirstName= GetClaimsFromContext(user, "FirstName"),
                LastName = GetClaimsFromContext(user, "LastName"),
                Codemeli = GetClaimsFromContext(user, "Codemeli"),
                Token = GetClaimsFromContext(user, "Token"),
                AccessMenu= !string.IsNullOrEmpty(accessMenuJson) && accessMenuJson != "null"? CommonOperations.JSON.jsonToT<List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>>(accessMenuJson):new List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>(),
            };
            return reponse;

        }
        private ClaimsPrincipal GetPrincipal(string token,string keyValue,string issuerValue)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = Convert.FromBase64String(keyValue);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidIssuer=issuerValue,
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
