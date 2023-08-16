using Application.Service.Contracts;
using AutoMapper;
using Common.Shared.DataTransferObjects;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employee)
        {
            var companyEntity = _repositoryManager.Company.GetCompany(companyId);
            if (companyEntity == null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employee);
            _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repositoryManager.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId)
        {
            var employeeEntity = _repositoryManager.Employee.GetEmployee(companyId, employeeId);
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate)
        {
            var company = _repositoryManager.Company.GetCompany(companyId);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _repositoryManager.Employee.GetEmployee(companyId, id, trackChange: true);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);
            _mapper.Map(employeeForUpdate, employeeEntity);
            _repositoryManager.Save();
        }

    }
}
