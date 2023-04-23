# RS_EasyExceptionHandling7 helper library for ASP.Net Core 7 

The RS EasyExceptionHandling helper library developed by [Mr. Ravinder Singh (ਰਵਿੰਦਰ ਸਿੰਘ)](https://theravinder.com), it helps you to manage error handling in your ASP.Net Core Web API or Web Site. So no need to write code for error loging for your project. The library supports ASP.NET Core 7.

Note: For ASP.net Core 6, please use this package RSEasyExceptionHandler https://www.nuget.org/packages/RSEasyExceptionHandler/
## Features 
- ASP.Net Core 7 error tracking in the SQLite database
- Send an error notification to the team
- Easy to access log data with help ErrorLogController  

## Getting started


### Prerequisites

Requirements
Requires .NET 7.0.

Following Nuget packages 

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite

## How to use

Run the following command to install the package using the .NET CLI:

```C#
dotnet add package RSEasyExceptionHandler7 
```
Alternatively, from the Package Manager Console or Developer PowerShell, run the following command to install the latest version:
```C#
Install-Package RSEasyExceptionHandler7 
```

Alternatively, use the NuGet Package Manager for Visual Studio or the NuGet window for JetBrains Rider, then search for Twilio.AspNet.Core and install the package.

- Open Program.cs class and regester <i>AddRS_ErrorHandlingMiddleware</i> Dependency near builder.Services.AddSwaggerGen();

	```C#
    builder.Services.AddRS_ErrorHandlingMiddleware();
    ```
- Note: Devloper can set own database name and location in the project. 1st create a folder like "Databaes" at root folder and then set "Data Source=database\\RSAppErrorsLog.db" like following
    ```C#
    builder.Services.AddRS_ErrorHandlingMiddleware("Data Source=database\\MyAppErrorsLog.db");
    ```

- Next add following code lines in Program.cs near app.Run();
	 ```C#
     RSDependencyInjection.SetupLogDataBase(app.Services.CreateScope());
     app.UseMiddleware<RS_ErrorHandlingMiddleware>();
     
     ```
     - Program.cs code file https://github.com/ravinder25886/RSEasyExceptionHandler/blob/main/TestWebApp/Program.cs

- Add SMTP Settings in appsettings.json
    ```JSON
  "RSError_EmailSenderSMTP": {
    "Host": "smtp-host.sendinblue.com",
    "Port": 587,
    "EnableSSL": true,
    "Username": "login email address",
    "Password": "password",

    "TL_Emails": "", //Team Lead or Project Manager emails (CC)
    "Dev_Emails": "ravinder25886@gmail.com", //Development team emails(TO)
    "enable_error_notification": false // true= will send mail and false mean off mailing
  }
    ```

## How to read error log
We have developed very user friendly library so that developers can read error log without any hard work.

- For read create a API controller and past following code. Now, you can show error log in your front end.

   ```C#
        [Route("api/[controller]")]
        [ApiController]
        public class ErrorLogController : ControllerBase
        {
            private readonly IErrorLogService _errorLogService;

            public ErrorLogController(IErrorLogService errorLogService)
            {
                _errorLogService = errorLogService;
            }
       
            [HttpGet(Name = "GetErrorLog")]
            public async   Task<IActionResult> Get([FromQuery] PaginationFilter filter)
            {
                return Ok(await _errorLogService.GetListAsync(filter)); 
            }
        }
   ```

   Delete log functions
   ```C#
        [HttpPost]
        [Route("DeleteAll")]
        public async Task<IActionResult> DeleteAll(CancellationToken cancellationToken)
        {
            return Ok(await _errorLogCommandsService.DeleteAllAsync(cancellationToken));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAll(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _errorLogCommandsService.DeleteByIdAsync(id, cancellationToken));
        }
   ```
   - ErrorLogController code file https://github.com/ravinder25886/RSEasyExceptionHandler/blob/main/TestWebApp/Controllers/ErrorLogController.cs 
## Feedback

If  this package is help full to you, then please share this package with your developer's friends. Moroeover, if you want to share your comment then please use following chanels:- 
 
 [Contact me](https://theravinder.com/contact)
 
## About me

I am a senior programmer with good knowledge of both front-end and back-end techniques(FULL STACK).

  [Read more](https://theravinder.com)