using BL.Infrastructure;
using BL.Secuirty;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.APIModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BL.Security
{
    public interface IAuthenticateService
    {
        Users AuthenticateUser(ApiUserLoginModel entity, out string token);

        Customer AuthenticateCustomer(ApiLoginModel request, out string token);
        Customer GetCustomerByToken(string token);
    }

    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;
        private readonly IUnitOfWork _uow;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement, IUnitOfWork uow)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
            _uow = uow;
        }

        public Customer AuthenticateCustomer(ApiLoginModel request, out string token)
        {
            token = string.Empty;
            var customer = _userManagementService.IsValidCustomer(request.Email, request.Password);
            if (customer != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Email, request.Email) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddDays(_tokenManagement.AccessExpiration);
                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expireDate,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
                token = tokenHandler.WriteToken(tokenObj);
            }
            return customer;
        }
        public Users AuthenticateUser(ApiUserLoginModel entity, out string token)
        {
            token = string.Empty;
            var user = _userManagementService.IsValidUser(entity.Mobile, entity.Password);
            if (user != null)
            {
                var claims = new[] { new Claim(ClaimTypes.MobilePhone, entity.Mobile) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddDays(_tokenManagement.AccessExpiration);
                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expireDate,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
                token = tokenHandler.WriteToken(tokenObj);
                user.OnLine = true;
                _uow.UsersRepository.Update(user);
            }
            return user;
        }

        public Customer GetCustomerByToken(string Token)
        {

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Token);
            var tokenS = handler.ReadToken(Token) as JwtSecurityToken;
            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            var customer = _uow.CustomerRepository.GetMany(ent => ent.Email == email).FirstOrDefault();

            return customer;
        }
    }

    public interface IUserManagementService
    {
        Customer IsValidCustomer(string email, string password);
        Users IsValidUser(string mobil, string password);
    }

    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _uow;
        public UserManagementService(IUnitOfWork uow) { _uow = uow; }

        public Customer IsValidCustomer(string email, string password)
        {
            var user = _uow.CustomerRepository.GetMany(ent => (ent.Email.ToLower() == email.ToLower().Trim() ||
            ent.Mobile.ToLower() == email.ToLower().Trim()) && ent.Password == EncryptANDDecrypt.EncryptText(password) &&
            !ent.IsDeleted && ent.IsActive
            ).ToList();
            return user.Count() == 1 ? user.FirstOrDefault() : null;
        }
        public Users IsValidUser(string mobil, string password)
        {
            var user = _uow.UsersRepository.GetUsers().Where(ent => ent.Mobile.ToLower() == mobil.Trim() &&
            ent.Password == EncryptANDDecrypt.EncryptText(password)).ToList();
            return user.Count() == 1 ? user.FirstOrDefault() : null;
        }
    }
}
