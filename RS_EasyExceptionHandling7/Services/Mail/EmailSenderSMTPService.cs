using RS_EasyExceptionHandling7.Services.Comman;
using RS_EasyExceptionHandling7.Services.Mail.SMTP;
using System.Net;
using System.Net.Mail;

namespace RS_EasyExceptionHandling7.Services.Mail
{
    public class EmailSenderSMTPService : IEmailSenderService
    {
        private readonly IRSSMTPSettigsService _rSSMTPSettigsService;
        public EmailSenderSMTPService(IRSSMTPSettigsService rSSMTPSettigsService)
        {
            _rSSMTPSettigsService = rSSMTPSettigsService;
        }
        public Response<bool> SendEmailAsync(string subject, string message, string toEmail,  string ccEmails = "", string bccEmails = "")
        {
            SmtpClient _smtpClient=new SmtpClient();
            try
            {
                var _SMTPSetings = _rSSMTPSettigsService.GetSMTPSettigs();
                if (_SMTPSetings == null)
                {
                    return new Response<bool> { Succeeded = false, Message = "RSError_EmailSenderSMTP setting are missing!" };
                }
                _smtpClient = new SmtpClient
                {
                    Host = _SMTPSetings.Host,
                    Port = _SMTPSetings.Port,
                    Credentials = new NetworkCredential(_SMTPSetings.Username, _SMTPSetings.Password),
                    EnableSsl = _SMTPSetings.EnableSSL
                };
                MailMessage msg = new MailMessage(
                              to: toEmail,
                              from: _SMTPSetings.Username,
                              subject: subject,
                              body: message
                              );
                if (!String.IsNullOrEmpty(ccEmails))
                {
                    msg.CC.Add(ccEmails);
                }
                if (!String.IsNullOrEmpty(bccEmails))
                {
                    msg.Bcc.Add(bccEmails);
                }
                msg.IsBodyHtml = true;
                  _smtpClient.Send(msg);
                msg.Dispose();
                _smtpClient.Dispose();

                return new Response<bool> {Data=true, Succeeded = true, Message = "eMail has been sent!" };
            }
            catch (Exception ex)
            {
                if (_smtpClient != null)
                {
                    _smtpClient.Dispose();
                }
                return new Response<bool> { Data = false, Succeeded = false, Message = ex.Message }; 
            }
        }
       
    }
     
}
