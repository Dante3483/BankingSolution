using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;

        public TransactionController(ITransactionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            var transactions = await _repository.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}", Name = nameof(GetTransactionByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Transaction>> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _repository.GetTransactionByIdAsync(id);
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
            var transaction = transactionDTO.ToTransaction();
            transaction.TransactionType = TransactionType.Deposit;
            await _repository.AddTransactionAsync(transaction);
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
            var transaction = transactionDTO.ToTransaction();
            transaction.TransactionType = TransactionType.Withdrawal;
            await _repository.AddTransactionAsync(transaction);
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
            var transaction = transactionDTO.ToTransaction();
            transaction.TransactionType = TransactionType.Transfer;
            await _repository.AddTransactionAsync(transaction);
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
            var transactionToDelete = await _repository.GetTransactionByIdAsync(id);
            if (transactionToDelete is null)
            {
                return NotFound();
            }
            await _repository.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
