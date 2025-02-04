namespace BankingSolution.Domain.Enums
{
    public enum TransactionStatus : byte
    {
        None = 0,
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Cancelled = 4,
        Reversed = 5,
    }
}
