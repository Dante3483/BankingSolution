using BankingSolution.Domain.Entities;
using BankingSolution.Tests.MoqSetups.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.Tests.WebApi.TransactionApi
{
    public class DeleteTransactionAsync
    {
        private readonly TransactionMoqSetup _moqSetup;

        public DeleteTransactionAsync()
        {
            _moqSetup = new();
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldReturnNoContent_WhenTransactionIsDeleted()
        {
            var transaction = _moqSetup.MockTransactionsList.First();

            var result = await _moqSetup.TransactionController.DeleteTransactionAsync(
                transaction.Id
            );

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTransactionAsync_ShouldReturnNotFound_WhenTransactionDoesNotExist()
        {
            var transaction = new Transaction();

            var result = await _moqSetup.TransactionController.DeleteTransactionAsync(
                transaction.Id
            );

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
