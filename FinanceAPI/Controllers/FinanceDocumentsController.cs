using FinanceAPI.Modules.FinanceDocumentModule.Requests;
using FinanceAPI.Modules.FinanceDocumentModule.Services;
using FinanceAPI.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinanceAPI.Controllers
{
    [Route("api/financeDocuments")]
    [ApiController]
    public class FinanceDocumentsController : ControllerBase
    {
        private readonly IFinanceDocumentService _financeDocumentService;

        public FinanceDocumentsController(
            IFinanceDocumentService financeDocumentService)
        {
            _financeDocumentService = financeDocumentService ?? throw new ArgumentNullException(nameof(financeDocumentService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            [FromQuery] string searchOptions)
        {
            var request = JsonConvert.DeserializeObject<FinanceDocumentDetailRequest>(searchOptions);
            return request.DocumentId.Equals(id)
                ? this.FromResult(
                    result: await _financeDocumentService.GetDetail(request))
                : BadRequest("Route and query params don't match");
        }
    }
}
