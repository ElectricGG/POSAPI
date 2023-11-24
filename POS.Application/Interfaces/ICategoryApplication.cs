using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;

namespace POS.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<IEnumerable<CategoryResponseDto>>> ListCategories(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveCategory(int categoryId);
    }
}
