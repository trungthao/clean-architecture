using Common.Shared.DataTransferObjects;

namespace Application.Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies();

    CompanyDto GetCompany(Guid companyId);
    CompanyDto CreateCompany(CompanyForCreationDto company);
    IEnumerable<CompanyDto> GetByIds(string ids);

    (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);

    void DeleteCompany(Guid companyId);
    void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate);

}

