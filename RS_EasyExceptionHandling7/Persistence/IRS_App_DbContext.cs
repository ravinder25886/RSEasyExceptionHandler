using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling7.Models;
 

namespace RS_EasyExceptionHandling7.Persistence
{
    public interface IRS_App_DbContext
    {
        DbSet<RS_AppErrorLogs> rSAppErrorLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
