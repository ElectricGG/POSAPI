using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Application.Commons.Bases.Response
{
    public class BaseFilterRequest : BasePaginationRequest
    {
        public int? NumFilter { get; set; } = null;
        public string? TextFilter { get; set; } = null;
        public int? StateFilter { get; set; } = null;
        public string? StartDate { get; set; } = null;
        public string? EndDatE { get; set; } = null;
        public bool? Download { get; set; } = false;
    }
}
