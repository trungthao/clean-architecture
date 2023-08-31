using Common.Shared.DataTransferObjects;
using Common.Shared.RequestFeatures;
using System.Dynamic;

namespace Application.Service.Contracts;

public interface IEmployeeService
{
    Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId);

    Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdate);

    Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employee);

    Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters);
}

