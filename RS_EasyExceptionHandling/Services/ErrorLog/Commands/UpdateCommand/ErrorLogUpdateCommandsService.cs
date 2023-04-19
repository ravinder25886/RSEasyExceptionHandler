using RS_EasyExceptionHandling.Persistence;
using RS_EasyExceptionHandling.Services.Comman;
 

namespace RS_EasyExceptionHandling.Services.ErrorLog.Commands.UpdateCommand
{
    public class ErrorLogUpdateCommandsService : IErrorLogUpdateCommandsService
    {
        private readonly IRS_App_DbContext _dbcontext;
        
        public ErrorLogUpdateCommandsService(IRS_App_DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Response<Guid>> UpdateEmailSentAsync(Guid Id, bool MailStatus, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbcontext.rSAppErrorLogs
                .FindAsync(new object[] { Id }, cancellationToken);
                if (entity != null)
                {
                    entity.MailSentDate = DateTime.UtcNow;
                    entity.IsMailSent = MailStatus;
                    await _dbcontext.SaveChangesAsync(cancellationToken);

                    return new Response<Guid> { Data = entity.Id, Succeeded = true, Message = "Record updated" };
                }

                return new Response<Guid> { Succeeded = false, Message = "Log not found!" };
            }
            catch (Exception ex)
            {
                return new Response<Guid> { Succeeded = false, Message = ex.Message };
            }
        }
    }
}
