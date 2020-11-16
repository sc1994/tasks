using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.AppServices
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorAppService : AppService
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        [Route("/error")]
        public object Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return new
            {
                context.Error.Message,
                context.Error.StackTrace
            };
        }
    }
}
