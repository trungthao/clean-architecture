using Application.Service.Contracts;
using Domain.Contracts.Repositories;

namespace Application.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager));
    }

    public ICompanyService Company => _companyService.Value;

    public IEmployeeService Employee => _employeeService.Value;
}

