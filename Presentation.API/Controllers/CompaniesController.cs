using Application.Service.Contracts;
using Common.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _service.Company.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("{companyId:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid companyId)
        {
            var company = _service.Company.GetCompany(companyId);
            return Ok(company);
        }

        [HttpPost("")]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _service.Company.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/{ids}", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection(string ids)
        {
            var companies = _service.Company.GetByIds(ids);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = _service.Company.CreateCompanyCollection(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids },
            result.companies);
        }

        [HttpDelete("{companyId:guid}")]
        public IActionResult DeleteCompany(Guid companyId)
        {
            _service.Company.DeleteCompany(companyId);
            return NoContent();
        }

        [HttpPut("{companyId:guid}")]
        public IActionResult UpdateCompany(Guid companyId, [FromBody] CompanyForUpdateDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");
            _service.Company.UpdateCompany(companyId, company);
            return NoContent();
        }

    }
}
