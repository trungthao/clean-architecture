using Domain.Entities.Models;

namespace Domain.Contracts.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyAsync(Guid companyId, bool isTrackChange = false);
    void CreateCompany(Company company);
    Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids);
    void DeleteCompany(Company company);
    void UpdateCompany(Company company);
}

