using Application.Service.Contracts;
using AutoMapper;
using Common.Shared.DataTransferObjects;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;

namespace Application.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto companyForCreationDto)
        {
            var companyEntity = _mapper.Map<Company>(companyForCreationDto);
            _repositoryManager.Company.CreateCompany(companyEntity);
            await _repositoryManager.SaveAsync();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                _repositoryManager.Company.CreateCompany(company);
            }
            await _repositoryManager.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return (companies: companyCollectionToReturn, ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId)
        {
            var company = await GetCompanyAndCheckIfItExists(companyId);

            _repositoryManager.Company.DeleteCompany(company);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _repositoryManager.Company.GetAllCompaniesAsync();
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                throw new IdParametersBadRequestException();

            var idList = ids.Split(';').AsEnumerable().Select(Guid.Parse);
            var companyEntities = await _repositoryManager.Company.GetByIdsAsync(idList);
            if (idList.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId)
        {
            var company = await GetCompanyAndCheckIfItExists(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate)
        {
            var companyEntity = await GetCompanyAndCheckIfItExists(companyId, trackChanges: true);

            _mapper.Map(companyForUpdate, companyEntity);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Company> GetCompanyAndCheckIfItExists(Guid id, bool trackChanges = false)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
            return company;
        }

    }
}
