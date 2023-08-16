using Domain.Entities.Models;

namespace Domain.Contracts.Repositories;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies();
    Company GetCompany(Guid companyId, bool isTrackChange = false);
    void CreateCompany(Company company);
    IEnumerable<Company> GetByIds(IEnumerable<Guid> ids);
    void DeleteCompany(Company company);
    void UpdateCompany(Company company);
}

