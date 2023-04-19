namespace RS_EasyExceptionHandling.Contracts
{
    public class ErrorLogResponse
    {
        public Guid Id { get; set; }
        public string ErrorTitle { get; set; }
        public int ErrorCount { get; set; }
        public DateTime MailSentDate { get; set; }
        public bool IsMailSent { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorSource { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}
