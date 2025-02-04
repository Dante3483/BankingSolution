using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.CustomerApi.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing customers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="repository">The customer repository.</param>
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>A collection of all customers.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Gets a customer by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The customer with the specified unique identifier.</returns>
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

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDTO">The customer data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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

        /// <summary>
        /// Deletes a customer by  its id.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
