//using crud_dotnet_api.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace crud_dotnet_api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase
//    {
//        private readonly EmployeeRepository _employeeRepository;
//        public EmployeeController(EmployeeRepository employeeRepository) {
//            _employeeRepository = employeeRepository;
//        }

//        [HttpPost]
//        public async Task<ActionResult> AddEmployee([FromBody] Employee model)
//        {
//            await _employeeRepository.AddEmployee(model);
//            return Ok();

//        }
//        [HttpGet]
//        public async Task<ActionResult> GetEmployeeList()
//        {
//            var employeeList = await _employeeRepository.GetAllEmployee();
//            return Ok(employeeList);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult> GetEmployeeById([FromRoute] int id)
//        {
//            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
//            return Ok(employee);    
//        }
//        [HttpPut("{id}")]
//        public async Task<ActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee model)
//        {
//            await _employeeRepository.UpdateEmployee(id, model);
//            return Ok();    
//        }
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteEmployee([FromRoute] int id)
//        {
//            await _employeeRepository.DeleteEmployee(id); 
//            return Ok();                    
//        }
//    }
//}
