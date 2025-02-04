using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository
        )
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}", Name = nameof(GetTransactionByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Transaction>> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost("Deposit")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddDepositTransactionAsync(
            [FromBody] TransactionDTO transactionDTO
        )
        {
            if (transactionDTO is null)
            {
                return BadRequest();
            }
            var transaction = transactionDTO.ToTransaction(TransactionType.Deposit);
            await _transactionRepository.AddTransactionAsync(transaction);
            await _accountRepository.IncreaseAccountBalanceAsync(
                transaction.AccountId,
                transaction.Amount
            );
            return CreatedAtRoute(
                nameof(GetTransactionByIdAsync),
                new { id = transaction.Id },
                transaction
            );
        }

        [HttpPost("Withdraw")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddWithdrawTransactionAsync(
            [FromBody] TransactionDTO transactionDTO
        )
        {
            if (transactionDTO is null)
            {
                return BadRequest();
            }
            var transaction = transactionDTO.ToTransaction(TransactionType.Withdrawal);
            await _transactionRepository.AddTransactionAsync(transaction);
            await _accountRepository.DecreaseAccountBalanceAsync(
                transaction.AccountId,
                transaction.Amount
            );
            return CreatedAtRoute(
                nameof(GetTransactionByIdAsync),
                new { id = transaction.Id },
                transaction
            );
        }

        [HttpPost("Transfer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddTransferTransactionAsync(
            [FromBody] TransactionDTO transactionDTO
        )
        {
            if (transactionDTO is null)
            {
                return BadRequest();
            }
            if (transactionDTO.DestinationAccountId is null)
            {
                return BadRequest();
            }
            var transaction = transactionDTO.ToTransaction(TransactionType.Transfer);
            await _transactionRepository.AddTransactionAsync(transaction);
            await _accountRepository.DecreaseAccountBalanceAsync(
                transaction.AccountId,
                transaction.Amount
            );
            await _accountRepository.IncreaseAccountBalanceAsync(
                transaction.DestinationAccountId!.Value,
                transaction.Amount
            );
            return CreatedAtRoute(
                nameof(GetTransactionByIdAsync),
                new { id = transaction.Id },
                transaction
            );
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteTransactionAsync(Guid id)
        {
            var transactionToDelete = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transactionToDelete is null)
            {
                return NotFound();
            }
            await _transactionRepository.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
