
using RS_EasyExceptionHandling7.Services.Comman;

namespace RS_EasyExceptionHandling7.Services.ErrorLog.Commands.UpdateCommand
{
    public interface IErrorLogUpdateCommandsService
    {
        Task<Response<Guid>> UpdateEmailSentAsync(Guid Id, bool MailStatus, CancellationToken cancellationToken);
    }
}
