using Common.Shared.RequestFeatures;
using Domain.Entities.Models;

namespace Domain.Contracts.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange = false);
    void CreateEmployeeForCompany(Guid companyId, Employee employee);
    Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters);
}

