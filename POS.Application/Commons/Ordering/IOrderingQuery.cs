using POS.Infrastructure.Commons.Bases.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Commons.Ordering
{
    public interface IOrderingQuery
    {
        IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class;
    }
}
