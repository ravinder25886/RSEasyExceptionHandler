using RS_EasyExceptionHandling7.Contracts;
using RS_EasyExceptionHandling7.Services.Comman;

namespace RS_EasyExceptionHandling7.Services.ErrorLog.Commands
{
    public interface IErrorLogCommandsService
    {
        public Task<Response<Guid>> AddAsync(AddErrorLogCommand command, CancellationToken cancellationToken);
        public Task<Response<bool>> DeleteAllAsync(CancellationToken cancellationToken);
        public Task<Response<bool>> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<Response<bool>> DeleteBy_LessThanDateAsync(string date, CancellationToken cancellationToken);
    }
}
