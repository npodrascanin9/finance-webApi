using FinanceAPI.DataAccess.Models;

namespace FinanceAPI.Modules.FinanceDocumentModule.Responses
{
    public class FinanceDocumentDetailResponse
    {
        public string ProductCode { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Data { get; set; } = "{}";
    }
}
