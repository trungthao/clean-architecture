using Domain.Entities.Models;

namespace Domain.Contracts.Repositories;

public interface IEmployeeRepository
{
    Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChange = false);
    void CreateEmployeeForCompany(Guid companyId, Employee employee);
}

