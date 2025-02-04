namespace BankingSolution.Domain.Enums
{
    /// <summary>
    /// Represents the type of a transaction.
    /// </summary>
    public enum TransactionType : byte
    {
        /// <summary>
        /// The transaction type is not specified.
        /// </summary>
        None = byte.MaxValue,

        /// <summary>
        /// The transaction is a deposit.
        /// </summary>
        Deposit = 0,

        /// <summary>
        /// The transaction is a withdrawal.
        /// </summary>
        Withdrawal = 1,

        /// <summary>
        /// The transaction is a transfer.
        /// </summary>
        Transfer = 2,
    }
}
