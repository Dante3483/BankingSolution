namespace BankingSolution.Domain.Enums
{
    public enum TransactionStatus : byte
    {
        None = byte.MaxValue,
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Cancelled = 3,
        Reversed = 4,
    }
}
