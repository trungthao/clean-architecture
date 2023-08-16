using Domain.Contracts.Repositories;
using Domain.Entities.Models;

namespace Infrastructure.Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Insert(employee);
    }

    public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChange = false)
    {
        return FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId))
            .FirstOrDefault();
    }
}
