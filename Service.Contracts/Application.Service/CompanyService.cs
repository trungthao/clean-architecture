using Application.Service.Contracts;
using Domain.Contracts.Repositories;

namespace Application.Service;

public class CompanyService : ICompanyService
{
    public CompanyService(IRepositoryManager repository)
    {
    }
}

