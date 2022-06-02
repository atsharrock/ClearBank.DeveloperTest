using ClearBank.DeveloperTest.Data.Factories;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            var accountDataStore = DataStoreFactory.CreateDataStore(dataStoreType);
            
            Account account;
            account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var makePaymentResult = new MakePaymentResult
            {
                Success = ValidateAccount(request, account)
            };

            if (!makePaymentResult.Success) return makePaymentResult;

            account.Balance -= request.Amount;
            accountDataStore.UpdateAccount(account);

            return makePaymentResult;
        }

        private static bool ValidateAccount(MakePaymentRequest paymentRequest, Account account)
        {
            var accountValidator = new AccountValidator(paymentRequest);
            var accountValidatorResult = accountValidator.Validate(account);
            return accountValidatorResult.IsValid;
        }
    }
}
