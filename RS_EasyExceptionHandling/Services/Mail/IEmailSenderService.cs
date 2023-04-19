
using RS_EasyExceptionHandling.Services.Comman;

namespace RS_EasyExceptionHandling.Services.Mail
{
    public interface IEmailSenderService
    {
        Response<bool> SendEmailAsync(string subject, string message, string toEmail, string ccEmails = "", string bccEmails = "");
    }
}

