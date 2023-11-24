using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders(BaseFilterRequest filters);
        Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId);
        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto RequestDto);
        Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto RequestDto);
        Task<BaseResponse<bool>> RemoveProvider(int providerId);
    }
}
