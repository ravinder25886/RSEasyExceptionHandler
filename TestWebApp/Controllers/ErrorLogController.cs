using Microsoft.AspNetCore.Mvc;
using RS_EasyExceptionHandling.Services.Comman;
using RS_EasyExceptionHandling.Services.ErrorLog.Commands;
using RS_EasyExceptionHandling.Services.ErrorLog.Queres;
namespace TestWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly IErrorLogCommandsService _errorLogCommandsService;
        private readonly IErrorLogQueresService _errorQueresService;

        public ErrorLogController(IErrorLogCommandsService errorLogCommandsService, IErrorLogQueresService errorQueresService)
        {
            _errorLogCommandsService = errorLogCommandsService;
            _errorQueresService = errorQueresService;
        }

        [HttpGet(Name = "GetErrorLog")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            return Ok(await _errorQueresService.GetListAsync(filter));
        }
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
    }
}
