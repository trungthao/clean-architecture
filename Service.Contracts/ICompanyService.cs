using Common.Shared.DataTransferObjects;

namespace Application.Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();

    Task<CompanyDto> GetCompanyAsync(Guid companyId);
    Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
    Task<IEnumerable<CompanyDto>> GetByIdsAsync(string ids);

    Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection);

    Task DeleteCompanyAsync(Guid companyId);

    Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate);

}

