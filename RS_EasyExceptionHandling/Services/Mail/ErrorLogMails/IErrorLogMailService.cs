using RS_EasyExceptionHandling.Services.Comman;

namespace RS_EasyExceptionHandling.Services.Mail.ErrorLogMails
{
    public interface IErrorLogMailService
    {
        Task<Response<bool>> SendErrorLogEmailAsync(Guid Id, CancellationToken cancellationToken);
    }
}
