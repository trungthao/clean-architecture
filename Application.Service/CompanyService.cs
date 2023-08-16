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
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public CompanyDto CreateCompany(CompanyForCreationDto companyForCreationDto)
        {
            var companyEntity = _mapper.Map<Company>(companyForCreationDto);
            _repositoryManager.Company.CreateCompany(companyEntity);
            _repositoryManager.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                _repositoryManager.Company.CreateCompany(company);
            }
            _repositoryManager.Save();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return (companies: companyCollectionToReturn, ids);
        }

        public void DeleteCompany(Guid companyId)
        {
            var company = _repositoryManager.Company.GetCompany(companyId);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            _repositoryManager.Company.DeleteCompany(company);
            _repositoryManager.Save();
        }

        public void DeleteCompany(Guid companyId, CompanyForUpdateDto companyForUpdate)
        {
            var companyEntity = _repositoryManager.Company.GetCompany(companyId, isTrackChange: true);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            _mapper.Map(companyForUpdate, companyEntity);
            _repositoryManager.Save();
        }

        public IEnumerable<CompanyDto> GetAllCompanies()
        {
            var companies = _repositoryManager.Company.GetAllCompanies();
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public IEnumerable<CompanyDto> GetByIds(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
                throw new IdParametersBadRequestException();

            var idList = ids.Split(';').AsEnumerable().Select(x => Guid.Parse(x));
            var companyEntities = _repositoryManager.Company.GetByIds(idList);
            if (idList.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }

        public CompanyDto GetCompany(Guid companyId)
        {
            var company = _repositoryManager.Company.GetCompany(companyId);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate)
        {
            var companyEntity = _repositoryManager.Company.GetCompany(companyId, isTrackChange: true);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            _mapper.Map(companyForUpdate, companyEntity);
            _repositoryManager.Save();
        }
    }
}
