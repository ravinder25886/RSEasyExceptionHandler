namespace RS_EasyExceptionHandling.Contracts
{
    public record AddErrorLogCommand
    ( 
        string ErrorTitle,
        string ErrorDetail, 
        string ErrorSource 
    );
}
