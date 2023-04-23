using RS_EasyExceptionHandling7.Services.Comman;

namespace RS_EasyExceptionHandling7.Services.Mail.ErrorLogMails
{
    public interface IErrorLogMailService
    {
        Task<Response<bool>> SendErrorLogEmailAsync(Guid Id, CancellationToken cancellationToken);
    }
}
