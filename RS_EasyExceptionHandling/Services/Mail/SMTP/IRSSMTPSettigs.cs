namespace RS_EasyExceptionHandling.Services.Mail.SMTP
{
    public interface IRSSMTPSettigsService
    {
        RSSMTPSettigs GetSMTPSettigs();
        bool IsValidEamil(string emailAddress);
    }
}
