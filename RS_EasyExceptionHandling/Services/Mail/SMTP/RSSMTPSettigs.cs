using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace RS_EasyExceptionHandling.Services.Mail.SMTP
{
    public class RSSMTPSettigsService: IRSSMTPSettigsService
    {
        private readonly IConfiguration _config;

        public RSSMTPSettigsService(IConfiguration config)
        {
            _config = config;
        }
        public RSSMTPSettigs GetSMTPSettigs()
        {
            try
            {
                return new RSSMTPSettigs
                {
                    EnableSSL = Convert.ToBoolean(_config.GetSection("RSError_EmailSenderSMTP").GetSection("EnableSSL").Value),
                    Host = _config.GetSection("RSError_EmailSenderSMTP").GetSection("Host").Value,
                    Password = _config.GetSection("RSError_EmailSenderSMTP").GetSection("Password").Value,
                    Username = _config.GetSection("RSError_EmailSenderSMTP").GetSection("Username").Value,
                    Port = Convert.ToInt16(_config.GetSection("RSError_EmailSenderSMTP").GetSection("Port").Value),

                    TL_Emails = _config.GetSection("RSError_EmailSenderSMTP").GetSection("TL_Emails").Value,
                    Dev_Emails = _config.GetSection("RSError_EmailSenderSMTP").GetSection("Dev_Emails").Value,
                    enable_error_notification =Convert.ToBoolean(_config.GetSection("RSError_EmailSenderSMTP").GetSection("enable_error_notifications").Value)
                };
            }
            catch (Exception)
            {
                return new RSSMTPSettigs();
            }

        }
        public bool IsValidEamil(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
    public class RSSMTPSettigs
    {
    public bool enable_error_notification { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool EnableSSL { get; set; }
    public string TL_Emails { get; set; }
    public string Dev_Emails { get; set; }
}
}
