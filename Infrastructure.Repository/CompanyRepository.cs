using Domain.Contracts.Repositories;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await FindAll()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await FindByCondition(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId, bool isTrackChange)
        {
            return await FindByCondition(c => c.Id.Equals(companyId), isTrackChange)
                .SingleOrDefaultAsync();
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }
    }
}