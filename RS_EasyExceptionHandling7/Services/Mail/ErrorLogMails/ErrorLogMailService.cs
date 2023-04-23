using Microsoft.EntityFrameworkCore;
using RS_EasyExceptionHandling7.Persistence;
using RS_EasyExceptionHandling7.Services.Comman;
using RS_EasyExceptionHandling7.Services.Mail.SMTP;
using System.Threading;

namespace RS_EasyExceptionHandling7.Services.Mail.ErrorLogMails
{
    public class ErrorLogMailService : IErrorLogMailService
    {
        private readonly IRS_App_DbContext _dbcontext;
        private readonly IRSSMTPSettigsService _rSSMTPSettigsService;
        private readonly IEmailSenderService _emailSenderService;
        public ErrorLogMailService(IRS_App_DbContext dbcontext, IRSSMTPSettigsService rSSMTPSettigsService, IEmailSenderService emailSenderService)
        {
            _dbcontext = dbcontext;
            _rSSMTPSettigsService = rSSMTPSettigsService;
            _emailSenderService = emailSenderService;
        }
        public async Task<Response<bool>> SendErrorLogEmailAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var _mailsettings = _rSSMTPSettigsService.GetSMTPSettigs();
                if(_mailsettings == null)
                {
                    return new Response<bool> { Succeeded = false, Message = "RSError_EmailSenderSMTP setting are missing!" };
                }
                if (!_mailsettings.enable_error_notification)
                {
                    return new Response<bool> { Succeeded = false, Message = "error notification is disabled" };
                }


                var entity = await _dbcontext.rSAppErrorLogs
                .FindAsync(new object[] { Id }, cancellationToken);
                if (entity != null)
                {
                    if(entity.ErrorCount > 1)
                    {
                        if (entity.ErrorDate.Date ==entity.MailSentDate.Date)
                        {
                            return new Response<bool> { Succeeded = false, Message = "Mail already sent" };
                        }
                     }
                 
                    if (_mailsettings != null)
                    {
                        if (string.IsNullOrEmpty(_mailsettings.Dev_Emails))
                        {
                            return new Response<bool> { Succeeded = false, Message = "Pleaes add developer team email address" };
                        }

                        string _body = "<b> Dear Team! </b><br/><br/>";
                        _body = _body + "<b>ERROR REPORTING!</b><br/><br/>";
                        _body = _body + "<b>" + entity.ErrorDetail + "</b> <br/><br/>";
                        _body = _body + "<p style='color:red' >" + entity.ErrorSource + "</p><br/><br/>";

                       var _result=    _emailSenderService.SendEmailAsync("App error! " + entity.ErrorTitle, _body
                            , _mailsettings.Dev_Emails, _mailsettings.TL_Emails, "");


                        return _result;

                    }

                   
                }
                return new Response<bool> { Succeeded = false, Message = "Log not found!" };
            }
            catch (Exception ex)
            {

                return new Response<bool> { Succeeded = false, Message = ex.Message };
            }
        }
    }
}
