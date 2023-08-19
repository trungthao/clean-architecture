﻿using Application.Service.Contracts;
using AutoMapper;
using Common.Shared.DataTransferObjects;
using Common.Shared.RequestFeatures;
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
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employee)
        {
            await CheckIfCompanyExists(companyId);

            var employeeEntity = _mapper.Map<Employee>(employee);
            _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repositoryManager.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId);
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters)
        {
            await CheckIfCompanyExists(companyId);

            var employeesFromDb = await _repositoryManager.Employee.GetEmployeesAsync(companyId, employeeParameters);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return employeesDto;
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdate)
        {
            await CheckIfCompanyExists(companyId);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges: true);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(employeeId);
            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repositoryManager.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges = false)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
        }

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges = false)
        {
            var employeeDb = await _repositoryManager.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
                throw new EmployeeNotFoundException(id);
            return employeeDb;
        }

    }
}
