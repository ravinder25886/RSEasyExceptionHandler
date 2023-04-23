namespace RS_EasyExceptionHandling7.Contracts
{
    public class ErrorLogModel
    {
        public Guid Id { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorSource { get; set; }
    }
}
