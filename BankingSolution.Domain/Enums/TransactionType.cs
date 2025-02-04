namespace BankingSolution.Domain.Enums
{
    public enum TransactionType : byte
    {
        None = byte.MaxValue,
        Deposit = 0,
        Withdrawal = 1,
        Transfer = 2,
    }
}
