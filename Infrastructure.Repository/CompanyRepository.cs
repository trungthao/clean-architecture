using Domain.Contracts.Repositories;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;

namespace Infrastructure.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
            Insert(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return FindAll()
                .OrderBy(c => c.Name)
                .ToList();
        }

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids)
        {
            return FindByCondition(x => ids.Contains(x.Id))
                .ToList();
        }

        public Company GetCompany(Guid companyId, bool isTrackChange)
        {
            return FindByCondition(c => c.Id.Equals(companyId), isTrackChange).SingleOrDefault();
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }
    }
}