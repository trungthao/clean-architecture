using Application.Service.Contracts;
using Common.Shared.DataTransferObjects;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILoggerManager _logger;

        public EmployeeController(IServiceManager service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _service.Employee.GetEmployeeAsync(companyId, employeeId);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var employeeToReturn = await _service.Employee.CreateEmployeeForCompanyAsync(companyId, employee);
            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                employeeId = employeeToReturn.Id
            },
            employeeToReturn);
        }

        [HttpPut("{employeeId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdateDto employee)
        {
            await _service.Employee.UpdateEmployeeForCompanyAsync(companyId, employeeId, employee);
            return NoContent();
        }


    }
}
