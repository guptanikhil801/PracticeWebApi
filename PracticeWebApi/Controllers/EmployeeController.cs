using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace PracticeWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IEmployeeService employeeService;

        public EmployeeController(IConfiguration configuration, IEmployeeService employeeService)
        {
            this.configuration = configuration;
            this.employeeService = employeeService;
        }

        //http://localhost:5010/api/Employee/login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] int employeeID)
        {
            var employees = employeeService.GetEmployee(employeeID);
            return (employees == null) ? BadRequest() : Ok(new { token = GenerateToken() });
        }

        // [Authorize]
        [HttpGet("getAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employees = employeeService.GetAllEmployees();
            return (employees.Count == 0) ? BadRequest() : Ok(employees);
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid || !await employeeService.AddEmployeeAsync(employee))
            {
                return BadRequest(false);
            }

            return Ok(true);
        }

        [HttpDelete("deleteEmployee")]
        public IActionResult DeleteEmployee(int empId)
        {
            if (empId != 0 || !employeeService.DeleteEmployee(empId))
            {
                return BadRequest(false);
            }

            return Ok(true);
        }

        [HttpGet("getEmployee/{empId}")]
        public IActionResult GetEmployee(int empId)
        {
            if (empId != 0)
            {
                var employee = employeeService.GetEmployee(empId);
                return Ok(employee);
            }
            return BadRequest(false);
        }

        [HttpPut("updateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (employee == null || !employeeService.UpdateEmployee(employee))
            {
                return NotFound(); ;
            }

            return NoContent();
        }

        private string GenerateToken()
        {
            var settings = configuration.GetSection("JwtValues").Get<JwtValues>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); ;
            var token = new JwtSecurityToken(
                settings.Issuer,
                settings.Audience, null,
                expires: DateTime.Now.AddMinutes(settings.ExpiresInMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}