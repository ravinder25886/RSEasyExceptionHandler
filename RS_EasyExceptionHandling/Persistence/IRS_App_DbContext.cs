using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling.Models;
 

namespace RS_EasyExceptionHandling.Persistence
{
    public interface IRS_App_DbContext
    {
        DbSet<RS_AppErrorLogs> rSAppErrorLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
