using Common.Shared.DataTransferObjects;

namespace Application.Service.Contracts;

public interface IEmployeeService
{
    EmployeeDto GetEmployee(Guid companyId, Guid employeeId);

    void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate);

    EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employee);
}

