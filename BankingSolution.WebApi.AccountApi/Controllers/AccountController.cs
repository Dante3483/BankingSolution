using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.AccountApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsAsync()
        {
            var accounts = await _repository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}", Name = nameof(GetAccountByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Account>> GetAccountByIdAsync(Guid id)
        {
            var account = await _repository.GetAccountByIdAsync(id);
            if (account is null)
            {
                return NotFound();
            }
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
            await _repository.AddAccountAsync(account);
            return CreatedAtRoute(nameof(GetAccountByIdAsync), new { id = account.Id }, account);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAccountAsync(Guid id)
        {
            var accountToDelete = await _repository.GetAccountByIdAsync(id);
            if (accountToDelete is null)
            {
                return NotFound();
            }
            await _repository.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
