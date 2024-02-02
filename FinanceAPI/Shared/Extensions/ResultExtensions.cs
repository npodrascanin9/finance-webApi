using FinanceAPI.Shared.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Shared.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult FromResult<T>(
            this ControllerBase controller,
            Result<T> result)
        {
            return result is not null
                ? InitializeResponse(controller, result)
                : controller.BadRequest("An error occured on the server");
        }

        private static ActionResult InitializeResponse<T>(
            ControllerBase controller,
            Result<T> result)
        {
            return result.ResultType switch
            {
                ResultType.Ok => result.Data is null
                    ? controller.NoContent()
                    : controller.Ok(result.Data),
                ResultType.NotFound => controller.NotFound(result.Errors),
                ResultType.Invalid => controller.BadRequest(result.Errors),
                ResultType.Unexpected => controller.BadRequest(result.Errors),
                ResultType.Unauthorized => controller.Unauthorized(result.Errors),
                _ => throw new Exception("An unhandled result has occurred as a result of a service call."),
            };
        }
    }
}
