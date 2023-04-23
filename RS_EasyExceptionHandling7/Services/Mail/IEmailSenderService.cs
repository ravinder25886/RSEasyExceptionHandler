
using RS_EasyExceptionHandling7.Services.Comman;

namespace RS_EasyExceptionHandling7.Services.Mail
{
    public interface IEmailSenderService
    {
        Response<bool> SendEmailAsync(string subject, string message, string toEmail, string ccEmails = "", string bccEmails = "");
    }
}

