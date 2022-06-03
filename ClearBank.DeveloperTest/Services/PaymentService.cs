using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentValidation.Results;

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
            var account = _dataStore.GetAccount(request.DebtorAccountNumber);
            var accountValidation = ValidateAccount(request, account);

            var makePaymentResult = new MakePaymentResult
            {
                Success = accountValidation.IsValid
            };

            if (!makePaymentResult.Success) return makePaymentResult;

            account.Balance -= request.Amount;
            _dataStore.UpdateAccount(account);

            return makePaymentResult;
        }

        private static ValidationResult ValidateAccount(MakePaymentRequest paymentRequest, Account account)
        {
            var accountValidator = new AccountValidator(paymentRequest);
            return accountValidator.Validate(account);
        }
    }
}