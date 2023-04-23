using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RS_EasyExceptionHandling7.Persistence;
using RS_EasyExceptionHandling7.Services.ErrorLog;
using RS_EasyExceptionHandling7.Services.ErrorLog.Commands;
using RS_EasyExceptionHandling7.Services.ErrorLog.Commands.UpdateCommand;
using RS_EasyExceptionHandling7.Services.ErrorLog.Queres;
using RS_EasyExceptionHandling7.Services.Mail;
using RS_EasyExceptionHandling7.Services.Mail.ErrorLogMails;
using RS_EasyExceptionHandling7.Services.Mail.SMTP;
using static System.Formats.Asn1.AsnWriter;

namespace RS_EasyExceptionHandling7
{
    public static class RSDependencyInjection
    {
        public static IServiceCollection AddRS_ErrorHandlingMiddleware(this IServiceCollection services,string SQLiteDBLocation= "Data Source=RSAppErrorsLog.db")
        {
            services.AddTransient<RS_ErrorHandlingMiddleware>();

            services.AddScoped<IRS_App_DbContext>(provider => provider.GetRequiredService<RS_App_DbContext>());

            services.AddScoped<IErrorLogCommandsService, ErrorLogCommandsService>();
            services.AddScoped<IErrorLogUpdateCommandsService, ErrorLogUpdateCommandsService>();
            services.AddScoped<IErrorLogQueresService, ErrorLogQueresService>();
            services.AddScoped<IEmailSenderService, EmailSenderSMTPService> ();
            services.AddSingleton<IRSSMTPSettigsService, RSSMTPSettigsService>();
            services.AddScoped<IErrorLogMailService, ErrorLogMailService>(); 


            services.AddDbContext<RS_App_DbContext>(
                options=>options.UseSqlite(SQLiteDBLocation)
                );
 
            return services;
        }
        public static void SetupLogDataBase(IServiceScope scope)
        {
            try
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<RS_App_DbContext>();

                dataContext.Database.Migrate();
            }
            catch  
            {

             
            }
        }
    }
}
