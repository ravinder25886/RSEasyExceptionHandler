namespace RS_EasyExceptionHandling7.Services.Mail.SMTP
{
    public interface IRSSMTPSettigsService
    {
        RSSMTPSettigs GetSMTPSettigs();
        bool IsValidEamil(string emailAddress);
    }
}
