using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore _dataStore;

        public PaymentService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account;
            account = _dataStore.GetAccount(request.DebtorAccountNumber);

            var makePaymentResult = new MakePaymentResult
            {
                Success = ValidateAccount(request, account)
            };

            if (!makePaymentResult.Success) return makePaymentResult;

            account.Balance -= request.Amount;
            _dataStore.UpdateAccount(account);

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
