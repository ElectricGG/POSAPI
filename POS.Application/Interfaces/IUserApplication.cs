using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<bool>> RegisterUser(UserRequestDto userRequestDto);
        Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto);
    }
}
