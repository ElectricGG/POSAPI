﻿using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderApplication _providerApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public ProviderController(IProviderApplication providerApplication, IGenerateExcelApplication generateExcelApplication)
        {
            _providerApplication = providerApplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProviders([FromBody] BaseFilterRequest filters)
        {
            var response = await _providerApplication.ListProviders(filters);

            if ((bool)filters.Download!)
            {
                var columnNames = ExcelColumnNames.GetColumnsProviders();
                var fileBytes = _generateExcelApplication.GenerateExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }

        [HttpGet("{providerId:int}")]
        public async Task<IActionResult> ProviderById(int providerId)
        {
            var response = await _providerApplication.ProviderById(providerId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterProvider([FromBody] ProviderRequestDto requestDto)
        {
            var response = await _providerApplication.RegisterProvider(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{providerId:int}")]
        public async Task<IActionResult> EditProvider(int providerId, [FromBody] ProviderRequestDto requestDto)
        {
            var response = await _providerApplication.EditProvider(providerId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{providerId:int}")]
        public async Task<IActionResult> RemoveProvider(int providerId)
        {
            var response = await _providerApplication.RemoveProvider(providerId);
            return Ok(response);
        }
    }
}
