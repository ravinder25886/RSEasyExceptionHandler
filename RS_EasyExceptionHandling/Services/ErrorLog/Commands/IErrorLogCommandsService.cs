using RS_EasyExceptionHandling.Contracts;
using RS_EasyExceptionHandling.Services.Comman;

namespace RS_EasyExceptionHandling.Services.ErrorLog.Commands
{
    public interface IErrorLogCommandsService
    {
        public Task<Response<Guid>> AddAsync(AddErrorLogCommand command, CancellationToken cancellationToken);
        public Task<Response<bool>> DeleteAllAsync(CancellationToken cancellationToken);
        public Task<Response<bool>> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
