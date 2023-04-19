using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling.Models;

namespace RS_EasyExceptionHandling.Persistence
{
    public class RS_App_DbContext:DbContext, IRS_App_DbContext
    {

        public RS_App_DbContext(DbContextOptions<RS_App_DbContext> options)
            : base(options)
        {

        }
        
        public DbSet<RS_AppErrorLogs> rSAppErrorLogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            // database provider is configured before runtime migration update is applied e.g:
            //  optionsBuilder.UseSqlite("Data Source=RSAppErrorsLog.db");
            //  Database.Migrate();
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
   
}
