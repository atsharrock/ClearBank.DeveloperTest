using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Helpers
{
    public static class AccountHelper
    {
        public static Account CreateBacsAccount()
        {
            return new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
            };
        }

        public static Account CreateChapsAccount(AccountStatus accountStatus)
        {
            return new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = accountStatus
            };
        }

        public static Account CreateFasterPaymentsAccount(decimal balance)
        {
            return new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = balance
            };
        }
    }
}
