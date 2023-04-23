using Microsoft.AspNetCore.Http;
using RS_EasyExceptionHandling7.Contracts;
using RS_EasyExceptionHandling7.Services.ErrorLog;
using RS_EasyExceptionHandling7.Services.ErrorLog.Commands;
using RS_EasyExceptionHandling7.Services.ErrorLog.Commands.UpdateCommand;
using RS_EasyExceptionHandling7.Services.Mail.ErrorLogMails;
using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
namespace RS_EasyExceptionHandling7
{
    public class RS_ErrorHandlingMiddleware : IMiddleware
    {
        private readonly IErrorLogCommandsService _commandsService;
        private readonly IErrorLogMailService _errorLogMailService;
        private readonly IErrorLogUpdateCommandsService _updateCommandsService;
        public RS_ErrorHandlingMiddleware(IErrorLogCommandsService commandsService, IErrorLogMailService errorLogMailService, IErrorLogUpdateCommandsService updateCommandsService)
        {
            _commandsService = commandsService;
            _errorLogMailService = errorLogMailService;
            _updateCommandsService = updateCommandsService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

            }
            catch (Exception ex)
            {   // we can use ILogger  or 
                //This is the place where we can call our save log function
                 SaveErrorLog(ex);

                //we will return a cusom message to the end user, becouse we don't want to share real error detail to the end user;
                var resposen = context.Response;
                resposen.ContentType = "application/json";
                var (status, message) = GetErrorMessage(ex);
                resposen.StatusCode = (int)status;
                await resposen.WriteAsync(message);

            }
        }

        private async void SaveErrorLog(Exception ex)
        {
            if (ex != null)
            {
                StackTrace trace = new StackTrace(ex, true);
                if (trace != null)
                {
                  //
                   // string method = trace.GetFrame(1).GetMethod().Name;
                    // string   method = new StackTrace(ex).GetFrame(0).GetMethod().Name;
                    //   method += trace.GetFrame(0).GetMethod().ReflectedType.FullName;
                    //Console.WriteLine(trace.GetFrame(0).GetMethod().ReflectedType.FullName);
                    //Console.WriteLine("Line: " + trace.GetFrame(0).GetFileLineNumber());
                    //Console.WriteLine("Column: " + trace.GetFrame(0).GetFileColumnNumber());
                    string _stackTrace = ex.StackTrace;
                    if (_stackTrace.Length >= 4000)
                    {
                        _stackTrace = _stackTrace.Substring(0, 3999);
                    }
                    AddErrorLogCommand addErrorLogCommand = new AddErrorLogCommand(
                        trace.GetFrame(0).GetMethod().ReflectedType.FullName+">" + trace.GetFrame(0).GetMethod().Name+ "| Line: " + trace.GetFrame(0).GetFileLineNumber()
                        , ex.Message+" | " + trace.GetFrame(0).GetMethod().ReflectedType.FullName + ">" + trace.GetFrame(0).GetMethod().Name + "| Line: " + trace.GetFrame(0).GetFileLineNumber()
                        , _stackTrace);
                    
                  var _saveLogResult=  await _commandsService.AddAsync(addErrorLogCommand, new CancellationToken());
                    if(_saveLogResult != null )
                    {
                        if (_saveLogResult.Succeeded)
                        {
                          var _sendErrorLogEmailResponse=    await _errorLogMailService.SendErrorLogEmailAsync(_saveLogResult.Data, new CancellationToken());
                            if (_sendErrorLogEmailResponse != null)
                            {
                                await _updateCommandsService.UpdateEmailSentAsync(_saveLogResult.Data,_sendErrorLogEmailResponse.Succeeded,  new CancellationToken());
                            }
                        }
                    }
                }
            }


        }
        private (HttpStatusCode code, string message) GetErrorMessage(Exception exception)
        {
            HttpStatusCode code;
            string _message;//set your custom  messages 
            switch (exception)
            {
                case KeyNotFoundException or FileNotFoundException:
                 code = HttpStatusCode.NotFound;
                    _message = "NotFound";
                    break;

                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    _message = "Unauthorized";
                    break;
                case  ArgumentException
                or InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    _message = "BadRequest";
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    _message = "InternalServerError";
                    break;
            }

           return ( code,_message );
        }
         
    }
}
