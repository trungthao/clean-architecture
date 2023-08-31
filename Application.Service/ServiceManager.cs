using Application.Service.Contracts;
using AutoMapper;
using Common.Shared.DataTransferObjects;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(
            IRepositoryManager repositoryManager, 
            IMapper mapper,
            IDataShaper<EmployeeDto> employeeDataShaper,
            UserManager<User> userManager,
            IConfiguration configuration
        )
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, mapper, employeeDataShaper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration));
        }

        public ICompanyService Company => _companyService.Value;

        public IEmployeeService Employee => _employeeService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
