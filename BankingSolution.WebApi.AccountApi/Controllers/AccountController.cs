using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountController(
            IAccountRepository repository,
            ICustomerRepository customerRepository
        )
        {
            _accountRepository = repository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountNumber}", Name = nameof(GetAccountByAccountNumberAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Account>> GetAccountByAccountNumberAsync(
            string accountNumber
        )
        {
            var account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);
            if (account is null)
            {
                return NotFound();
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(account.CustomerId);
            if (customer is null)
            {
                return NotFound();
            }
            account.Customer = customer;
            return Ok(account);
        }

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
                return BadRequest();
            }
            if (accountDTO is null)
            {
                return BadRequest();
            }
            var account = accountDTO.ToAccount();
            account.CurrentBalance = initialBalance;
            await _accountRepository.AddAccountAsync(account);
            return CreatedAtRoute(
                nameof(GetAccountByAccountNumberAsync),
                new { accountNumber = account.AccountNumber },
                account
            );
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAccountAsync(Guid id)
        {
            var accountToDelete = await _accountRepository.GetAccountByIdAsync(id);
            if (accountToDelete is null)
            {
                return NotFound();
            }
            await _accountRepository.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
