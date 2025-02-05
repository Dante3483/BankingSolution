using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing accounts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountRepository">The account repository.</param>
        /// <param name="customerRepository">The customer repository.</param>
        public AccountController(
            IAccountRepository accountRepository,
            ICustomerRepository customerRepository
        )
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        /// <returns>A collection of all accounts.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        /// <summary>
        /// Gets an account by its account number (id).
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <returns>The account with the specified account number.</returns>
        [HttpGet("{id}", Name = nameof(GetAccountByAccountNumberAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Account>> GetAccountByAccountNumberAsync(Guid id)
        {
            var account = await _accountRepository.GetAccountByAccountNumberAsync(id);
            if (account is null)
            {
                return NotFound("Account not found.");
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(account.CustomerId);
            if (customer is null)
            {
                return NotFound("Customer not found.");
            }
            account.Customer = customer;
            return Ok(account);
        }

        /// <summary>
        /// Adds a new account.
        /// </summary>
        /// <param name="initialBalance">The initial balance of the account.</param>
        /// <param name="accountDTO">The account data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost("{initialBalance}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddAccountAsync(
            decimal initialBalance,
            [FromBody] AccountDTO accountDTO
        )
        {
            if (initialBalance < 0 || initialBalance > decimal.MaxValue)
            {
                return BadRequest("Initial balance is invalid.");
            }
            if (accountDTO is null)
            {
                return BadRequest("Account data is null.");
            }
            var account = accountDTO.ToAccount(initialBalance);
            await _accountRepository.AddAccountAsync(account);
            return CreatedAtRoute(
                nameof(GetAccountByAccountNumberAsync),
                new { id = account.Id },
                account
            );
        }

        /// <summary>
        /// Deletes an account by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the account to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAccountAsync(Guid id)
        {
            var accountToDelete = await _accountRepository.GetAccountByAccountNumberAsync(id);
            if (accountToDelete is null)
            {
                return NotFound("Account not found.");
            }
            await _accountRepository.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
