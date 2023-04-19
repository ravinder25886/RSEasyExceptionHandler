using RS_EasyExceptionHandling.Contracts;
using RS_EasyExceptionHandling.Services.Comman;
 
namespace RS_EasyExceptionHandling.Services.ErrorLog.Queres
{
    public interface IErrorLogQueresService
    {
        public Task<PagedResponse<List<ErrorLogResponse>>> GetListAsync(PaginationFilter filter);
    }
}
