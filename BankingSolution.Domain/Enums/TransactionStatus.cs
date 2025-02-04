namespace BankingSolution.Domain.Enums
{
    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatus : byte
    {
        /// <summary>
        /// The transaction status is not specified.
        /// </summary>
        None = byte.MaxValue,

        /// <summary>
        /// The transaction is pending.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The transaction is completed.
        /// </summary>
        Completed = 1,

        /// <summary>
        /// The transaction has failed.
        /// </summary>
        Failed = 2,

        /// <summary>
        /// The transaction has been cancelled.
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// The transaction has been reversed.
        /// </summary>
        Reversed = 4,
    }
}
