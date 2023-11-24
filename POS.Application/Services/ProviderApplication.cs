﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ProviderApplication : IProviderApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public ProviderApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }

        public async Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders(BaseFilterRequest filters)
        {
            var response = new BaseResponse<IEnumerable<ProviderResponseDto>>();

            try
            {
                var providers = _unitOfWork.Provider.
                                    GetAllQueryable().
                                    Include(x => x.DocumentType).
                                    AsQueryable();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            providers = providers.Where(x => x.Name.Contains(filters.TextFilter));
                            break;
                        case 2:
                            providers = providers.Where(x => x.Email.Contains(filters.TextFilter));
                            break;
                        case 3:
                            providers = providers.Where(x => x.DocumentNumber.Contains(filters.TextFilter));
                            break;
                    }
                }

                if (filters.StateFilter is not null)
                {
                    providers = providers.Where(x => x.State.Equals(filters.StateFilter));
                }

                if (filters.StartDate is not null && filters.EndDatE is not null)
                {
                    providers = providers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate)
                                                && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDatE).AddDays(1));

                }

                if (filters.Sort is null){ filters.Sort = "Id"; }

                var items = await _orderingQuery.Ordering(filters, providers, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await providers.CountAsync();
                response.Data = _mapper.Map<IEnumerable<ProviderResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }

            

            return response;
        }

        public async Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId)
        {
            var response = new BaseResponse<ProviderResponseDto>();
            var provider = await _unitOfWork.Provider.GetByIdAsync(providerId);

            if(provider is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProviderResponseDto>(provider);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto RequestDto)
        {
            var response = new BaseResponse<bool>();
            var provider = _mapper.Map<Provider>(RequestDto);

            response.Data = await _unitOfWork.Provider.RegisterAsync(provider);

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

        public async Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto RequestDto)
        {
            var response = new BaseResponse<bool>();

            var providerById = await ProviderById(providerId);

            if(providerById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var provider = _mapper.Map<Provider>(RequestDto);

            provider.Id = providerId;
            response.Data = await _unitOfWork.Provider.EditAsync(provider);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveProvider(int providerId)
        {
            var response = new BaseResponse<bool>();

            var providerById = await ProviderById(providerId);

            if (providerById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            
            response.Data = await _unitOfWork.Provider.RemoveAsync(providerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELTE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
