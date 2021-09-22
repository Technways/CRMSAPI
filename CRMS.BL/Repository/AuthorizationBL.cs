using CRMS.BL.Interface;
using CRMS.DL;
using CRMS.DL.Interfaces;
using CRMS.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.BL.Repository
{
   public class AuthorizationBL : IAuthorizationBL
    {

        private readonly IAuthorizationDL _authorizationDL;

        private readonly IUserDetailsDL _userDetailsDL;
        private readonly IConfiguration _configuration;

        public AuthorizationBL(IAuthorizationDL authorizationDL, IUserDetailsDL userDetailsDL, IConfiguration configurationL)
        {
            _authorizationDL = authorizationDL;
            _userDetailsDL = userDetailsDL;
            _configuration = configurationL;
        }
        public async Task<TokenResponse> GetUserToken(TokenRequest tokenRequest)
        {
            TokenResponse response = new TokenResponse();
            if (tokenRequest != null && tokenRequest.Email != null && tokenRequest.Password != null)
            {
                UserResponse userResponse = new UserResponse();
                int userID = await _authorizationDL.ValidateUser(tokenRequest.Email, tokenRequest.Password);

                if (userID != 0 && userID > 0)
                {
                    userResponse = await _userDetailsDL.GetUserDetailsByIdAsync(userID);

                    if (userResponse != null)
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userResponse.UserId.ToString()),
                        new Claim("FirstName", userResponse.FirstName),
                        new Claim("LastName", userResponse.LastName),
                        new Claim("UserName", userResponse.FirstName + userResponse.UserId.ToString()),
                        new Claim("Email", userResponse.Email)
                        };


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                        response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        response.Message = "User validated successfully and Token got generated.";
                        return response;
                    }

                    return response;
                }
                else
                {
                    response.Message = "User is not validated successfully.";
                    return response;
                }
            }
            else
            {
                response.Message = "Required user details not provided.";
                return response;
            }

        }
    }
}
