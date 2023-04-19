
using RS_EasyExceptionHandling.Services.Comman;

namespace RS_EasyExceptionHandling.Services.ErrorLog.Commands.UpdateCommand
{
    public interface IErrorLogUpdateCommandsService
    {
        Task<Response<Guid>> UpdateEmailSentAsync(Guid Id, bool MailStatus, CancellationToken cancellationToken);
    }
}
