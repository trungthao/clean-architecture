using Common.Shared.DataTransferObjects;

namespace Application.Service.Contracts;

public interface IEmployeeService
{
    Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId);

    Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdate);

    Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employee);
}

