using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.User.Request;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.FileStorage;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;


namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorage _azureStorage;

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IAzureStorage azureStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _azureStorage = azureStorage;
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto)
        {
            var response = new BaseResponse<string>();

            var account = await _unitOfWork.User.AccountByUserName(requestDto.Username!);

            if(account is not null)
            {
                if (BC.Verify(requestDto.Password, account.Password))
                {
                    response.IsSuccess = true;
                    response.Data = GenerateToken(account);
                    response.Message = ReplyMessage.MESSAGE_TOKEN;

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                }
            }
            

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto userRequestDto)
        {
            var response = new BaseResponse<bool>();
            var account = _mapper.Map<User>(userRequestDto);
            account.Password = BC.HashPassword(account.Password);

            if(userRequestDto.Image is not null)
            {
                account.Image = await _azureStorage.SaveFile(AzureContainers.USERS, userRequestDto.Image);
            }

            response.Data = await _unitOfWork.User.RegisterAsync(account);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        private string GenerateToken(User user)
        {
            var jwt = _configuration["Jwt:Secret"].ToString();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
