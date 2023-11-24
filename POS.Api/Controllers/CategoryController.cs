using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]//El metodo es post para poder enviar los parametros en el body
        public async Task<IActionResult> ListCategories([FromQuery] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListCategories(filters);

            if ((bool)filters.Download!)
            {
                var columnNames = ExcelColumnNames.GetColumnsCategories();
                var fileBytes = _generateExcelApplication.GenerateExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }

        [AllowAnonymous]//ignora la seguridad
        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCategories()
        {
            var response = await _categoryApplication.ListSelectCategories();
            return Ok(response);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> CategoryById(int categoryId)
        {
            var response = await _categoryApplication.CategoryById(categoryId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategory([FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.RegisterCategory(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{categoryId:int}")]
        public async Task<IActionResult> EditCategory(int categoryId,[FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.EditCategory(categoryId,requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var response = await _categoryApplication.RemoveCategory(categoryId);
            return Ok(response);
        }
    }
}
