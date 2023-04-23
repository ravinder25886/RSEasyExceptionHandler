using RS_EasyExceptionHandling7.Contracts;
using RS_EasyExceptionHandling7.Services.Comman;
 
namespace RS_EasyExceptionHandling7.Services.ErrorLog.Queres
{
    public interface IErrorLogQueresService
    {
        public Task<PagedResponse<List<ErrorLogResponse>>> GetListAsync(PaginationFilter filter);
    }
}
