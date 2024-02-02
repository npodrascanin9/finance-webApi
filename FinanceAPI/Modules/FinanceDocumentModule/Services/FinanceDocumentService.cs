using FinanceAPI.DataAccess.Models;
using FinanceAPI.Modules.FinanceDocumentModule.Args;
using FinanceAPI.Modules.FinanceDocumentModule.Repositories;
using FinanceAPI.Modules.FinanceDocumentModule.Requests;
using FinanceAPI.Modules.FinanceDocumentModule.Responses;
using FinanceAPI.Shared.Extensions;
using FinanceAPI.Shared.HttpResults;
using FinanceAPI.Shared.Repositories;

namespace FinanceAPI.Modules.FinanceDocumentModule.Services
{
    public interface IFinanceDocumentService
    {
        Task<Result<FinanceDocumentDetailResponse>> GetDetail(
            FinanceDocumentDetailRequest request);
    }

    public class FinanceDocumentService :
        IFinanceDocumentService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Tenant> _tenantRepository;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly IFinanceDocumentRepository _documentRepository;

        public FinanceDocumentService(
            IBaseRepository<Product> productRepository,
            IBaseRepository<Tenant> tenantRepository,
            IBaseRepository<Client> clientRepository,
            IFinanceDocumentRepository documentRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }


        public async Task<Result<FinanceDocumentDetailResponse>> GetDetail(
            FinanceDocumentDetailRequest request)
        {
            if (!request.IsValid())
                return new InvalidResult<FinanceDocumentDetailResponse>("Invalid request");
            var productResult = await InitializeProductByCodeResult(request.ProductCode);
            if (productResult?.HasErrors() ?? false)
                return new ForbiddenResult<FinanceDocumentDetailResponse>(productResult.Errors.First());
            if (!await _tenantRepository.Any(request.IsWhiteListedTenant()))
                return new ForbiddenResult<FinanceDocumentDetailResponse>($"Tenant with Id='{request.TenantId}' is not whitelisted");
            var clientResult = await InitializeClientByTenantIdResult(request.TenantId);
            if (clientResult?.HasErrors() ?? false)
                return new ForbiddenResult<FinanceDocumentDetailResponse>(clientResult.Errors.First());
            return new SuccessResult<FinanceDocumentDetailResponse>(
                data: await InitializeDetail(new FinanceDocumentDetailArgs
                {
                    CompanyRegistrationNumber = clientResult.Data.CompanyRegistrationNumber,
                    DocumentId = request.DocumentId,
                    Product = productResult.Data
                }));
        }

        private async Task<Result<Product>> InitializeProductByCodeResult(string code)
        {
            var product = await _productRepository.FirstOrDefault(x => x.Code.Equals(code));
            if (product is null)
                return new NotFoundResult<Product>($"Product with Code='{code}' does not exist");
            if (product.IsUnsupported())
                return new ForbiddenResult<Product>($"Product with Code='{code}' is not supported");
            return new SuccessResult<Product>(product);
        }

        private async Task<Result<Client>> InitializeClientByTenantIdResult(Guid tenantId)
        {
            var client = await _clientRepository.FirstOrDefault(x => x.TenantId.Equals(tenantId));
            if (client is null)
                return new ForbiddenResult<Client>($"Client with tenantId='{tenantId}' not found");
            if (!client.CanAccessData())
                return new ForbiddenResult<Client>($"Client with a company type '{ClientCompanyType.Small.GetDescription()}' can't access data");
            return new SuccessResult<Client>(client);
        }

        private async Task<FinanceDocumentDetailResponse> InitializeDetail(
            FinanceDocumentDetailArgs args)
        {
            return new FinanceDocumentDetailResponse()
            {
                RegistrationNumber = args.CompanyRegistrationNumber,
                ProductCode = args.Product.Code,
                Data = await _documentRepository.GetById(args.DocumentId, args.Product.Rule)
            };
        }
    }
}
