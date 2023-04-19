using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling.Contracts;
using RS_EasyExceptionHandling.Models;
using RS_EasyExceptionHandling.Persistence;
using RS_EasyExceptionHandling.Services.Comman;
using RS_EasyExceptionHandling.Services.ErrorLog.Commands.UpdateCommand;
using RS_EasyExceptionHandling.Services.Mail;
using RS_EasyExceptionHandling.Services.Mail.SMTP;

namespace RS_EasyExceptionHandling.Services.ErrorLog.Commands
{
    public class ErrorLogCommandsService: IErrorLogCommandsService
    {
        private readonly IRS_App_DbContext _dbcontext;
        
        public ErrorLogCommandsService(IRS_App_DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Response<Guid>> AddAsync(AddErrorLogCommand command, CancellationToken cancellationToken)
        {
            var _errorData = await CheckExistingLogDetail(command.ErrorDetail);

            try
            {

                if (_errorData == null)
                {
                    RS_AppErrorLogs rS_AppErrorLogs = new RS_AppErrorLogs()
                    {
                        ErrorDate = DateTime.UtcNow,
                        ErrorDetail = command.ErrorDetail,
                        ErrorSource = command.ErrorSource,
                        ErrorTitle = command.ErrorTitle,
                        ErrorCount = 1,
                        Id = Guid.NewGuid(),
                        LogType = "Error"
                    };
                  
                    await _dbcontext.rSAppErrorLogs.AddAsync(rS_AppErrorLogs);
                    var _status=  await _dbcontext.SaveChangesAsync(cancellationToken);

                    return new Response<Guid> { Data = rS_AppErrorLogs.Id, Succeeded = true, Message = "error log table has been saved!" };
                    
                }
                else
                {
                    if (_errorData != null)
                    {
                        DateTime lastDate = _errorData.ErrorDate;
                        _errorData.ErrorCount = _errorData.ErrorCount + 1;
                        _errorData.ErrorDate = DateTime.UtcNow;
                        _errorData.ErrorSource = command.ErrorSource;

                        await _dbcontext.SaveChangesAsync(cancellationToken);

                        return new Response<Guid> { Data = _errorData.Id, Succeeded = true, Message = "error log table has been saved!" };
                    }

                }
                return new Response<Guid> { Succeeded = false };

            }
            catch (Exception ex)
            {
                return new Response<Guid> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<bool>> DeleteAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var all = _dbcontext.rSAppErrorLogs;
                _dbcontext.rSAppErrorLogs.RemoveRange(all);
                await _dbcontext.SaveChangesAsync(cancellationToken);
                return new Response<bool> { Succeeded = true, Message = "error log table has been reset!" };
            }
            catch (Exception ex)
            {
                return new Response<bool> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<bool>> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbcontext.rSAppErrorLogs
                .FindAsync(new object[] { Id }, cancellationToken);
                if (entity != null)
                {
                    _dbcontext.rSAppErrorLogs.Remove(entity);
                    await _dbcontext.SaveChangesAsync(cancellationToken);
                    return new Response<bool> { Succeeded = true, Message = "log has been deleted!" };
                }

                return new Response<bool> { Succeeded = false, Message = "Log not found!" };
            }
            catch (Exception ex)
            {
                return new Response<bool> { Succeeded = false, Message = ex.Message };
            }
        }
    
        private async Task<RS_AppErrorLogs> CheckExistingLogDetail(string errorDetail)
        {
            try
            {
                var _data = await _dbcontext.rSAppErrorLogs.AsQueryable().Where(x => x.ErrorDetail.Contains(errorDetail)
                ).FirstOrDefaultAsync();
                return _data;
            }
            catch (Exception)
            {

                return new RS_AppErrorLogs();
            }
        }
        
   
    }
}
