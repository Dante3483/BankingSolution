using BankingSolution.Domain.Entities;
using BankingSolution.Domain.Enums;
using BankingSolution.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.WebApi.TransactionApi.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing transactions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="transactionRepository">The transaction repository.</param>
        /// <param name="accountRepository">The account repository.</param>
        public TransactionController(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository
        )
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A collection of all transactions.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        /// <summary>
        /// Gets a transaction by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <returns>The transaction with the specified id.</returns>
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

        /// <summary>
        /// Adds a new deposit transaction.
        /// </summary>
        /// <param name="transactionDTO">The transaction data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost("deposit")]
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

        /// <summary>
        /// Adds a new withdrawal transaction.
        /// </summary>
        /// <param name="transactionDTO">The transaction data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost("withdraw")]
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

        /// <summary>
        /// Adds a new transfer transaction.
        /// </summary>
        /// <param name="transactionDTO">The transaction data transfer object.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost("transfer")]
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

        /// <summary>
        /// Deletes a transaction by its id.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
