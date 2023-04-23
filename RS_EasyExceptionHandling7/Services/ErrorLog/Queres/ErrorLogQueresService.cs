using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling7.Contracts;
using RS_EasyExceptionHandling7.Models;
using RS_EasyExceptionHandling7.Persistence;
using RS_EasyExceptionHandling7.Services.Comman;
 
namespace RS_EasyExceptionHandling7.Services.ErrorLog.Queres
{
    public class ErrorLogQueresService: IErrorLogQueresService
    {
        private readonly IRS_App_DbContext _dbcontext;

        public ErrorLogQueresService(IRS_App_DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<PagedResponse<List<ErrorLogResponse>>> GetListAsync(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            List<ErrorLogResponse> errorLogs = new List<ErrorLogResponse>();
            try
            {
                List<RS_AppErrorLogs> _data = new List<RS_AppErrorLogs>();
                if (String.IsNullOrEmpty(filter.SearchText))
                {
                    _data = await _dbcontext.rSAppErrorLogs.OrderByDescending(x => x.ErrorDate)
                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                  .ToListAsync();
                }
                else
                {
                    _data = await _dbcontext.rSAppErrorLogs.OrderByDescending(x => x.ErrorDate)
                  .AsQueryable().Where(
                        p => p.ErrorDetail.Contains(filter.SearchText)
                        )
                 .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                 .Take(validFilter.PageSize)
                 .ToListAsync();
                }

                foreach (var error in _data)
                {
                    errorLogs.Add(new ErrorLogResponse()
                    {
                        ErrorDate = error.ErrorDate,
                        ErrorDetail = error.ErrorDetail,
                        ErrorSource = error.ErrorSource,
                        ErrorTitle = error.ErrorTitle,
                        Id = error.Id,
                        ErrorCount = error.ErrorCount,
                        IsMailSent = error.IsMailSent,
                        MailSentDate = error.MailSentDate

                    });
                }
                var totalRecords = await _dbcontext.rSAppErrorLogs.CountAsync();
                var pagedReponse = PaginationHelper.CreatePagedReponse<ErrorLogResponse>(errorLogs, validFilter, totalRecords);
                return pagedReponse;
            }
            catch (Exception ex)
            {
                var pagedReponse = PaginationHelper.CreatePagedReponse<ErrorLogResponse>(errorLogs, validFilter, 0);
                pagedReponse.Succeeded = false;
                pagedReponse.Message = ex.Message;
                return pagedReponse;
            }
        }
    }
}
