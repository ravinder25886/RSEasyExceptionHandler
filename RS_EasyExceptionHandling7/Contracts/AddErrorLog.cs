namespace RS_EasyExceptionHandling7.Contracts
{
    public record AddErrorLogCommand
    ( 
        string ErrorTitle,
        string ErrorDetail, 
        string ErrorSource 
    );
}
