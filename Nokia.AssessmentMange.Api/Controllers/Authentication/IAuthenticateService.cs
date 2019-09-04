using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nokia.AssessmentMange.Api.Controllers.UserManage;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Controllers.Authentication
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
    }
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;
            User user = _userManagementService.IsValidUser(request.Username, request.Password);
            if (user == null) { return false; }
            var claim = new[] { new Claim(ClaimTypes.Name, request.Username), new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user") };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience,
                claim, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

        public User ParseToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var readableToken = jwtHandler.CanReadToken(token);
            if (readableToken != true)
            {
                throw new TokenWrongFormat(token);
            }
            else
            {
                var jwtToken = jwtHandler.ReadJwtToken(token);
                IEnumerable<Claim> claims = jwtToken.Claims;
                string loginName = claims.FirstOrDefault(item => item.Type == ClaimTypes.Name).Value;
                bool isAdmin = claims.FirstOrDefault(item => item.Type == ClaimTypes.Role).Value == "admin";
                return new User(loginName, isAdmin);
            }
        }
    }
}
