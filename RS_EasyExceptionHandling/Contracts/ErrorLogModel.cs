namespace RS_EasyExceptionHandling.Contracts
{
    public class ErrorLogModel
    {
        public Guid Id { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorSource { get; set; }
    }
}
