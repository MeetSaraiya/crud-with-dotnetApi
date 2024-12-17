using crud_with_dotnetApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud_with_dotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepositery _employeeRepositery;

        public EmployeeController(EmployeeRepositery employeeRepositery)
        {
            _employeeRepositery = employeeRepositery;
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee model)
        {
            await _employeeRepositery.AddEmployeeAsync(model);
            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var employeeList = await _employeeRepositery.GetEmployeesAsync();
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById([FromRoute] int id)
        {
            var employee = await _employeeRepositery.GetSingleEmployeeAsync(id);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute]int id, [FromBody]Employee model)
        {
             await _employeeRepositery.UpdateEmployeeAsync(id, model);
            return Ok();
        }

        [HttpDelete("({id})")]
        public async Task<ActionResult> DeleteEmployee([FromRoute]int id)
        {
            await _employeeRepositery.DeleteEmployee(id);
            return Ok();
        }
        
    }
}
