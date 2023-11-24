using AutoMapper;
using POS.Application.Dtos.DocumentType.Response;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Mappers
{
    public class DocumentTypeMappinsProfile:Profile
    {
        public DocumentTypeMappinsProfile()
        {
            CreateMap<DocumentType, DocumentTypeResponseDto>()
                .ForMember(x=> x.DocumentTypeId, x=> x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
