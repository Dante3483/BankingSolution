using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}", Name = nameof(GetCustomerByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Customer>> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddCustomerAsync([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO is null)
            {
                return BadRequest();
            }
            var customer = customerDTO.ToCustomer();
            await _repository.AddCustomerAsync(customer);
            return CreatedAtRoute(nameof(GetCustomerByIdAsync), new { id = customer.Id }, customer);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCustomerAsync(Guid id)
        {
            var customerToDelete = await _repository.GetCustomerByIdAsync(id);
            if (customerToDelete is null)
            {
                return NotFound();
            }
            await _repository.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
