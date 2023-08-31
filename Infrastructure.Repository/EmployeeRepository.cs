﻿using Common.Shared.RequestFeatures;
using Domain.Contracts.Repositories;
using Domain.Entities.Models;
using Infrastructure.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange = false)
    {
        return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId))
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters)
    {
        var employees = await FindByCondition(e => e.CompanyId.Equals(companyId))
            .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
            .Search(employeeParameters.SearchTerm)
            .Sort(employeeParameters.OrderBy)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

        var count = await FindByCondition(e => e.CompanyId.Equals(companyId)).CountAsync();

        return PagedList<Employee>.ToPagedList(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
    }
}
