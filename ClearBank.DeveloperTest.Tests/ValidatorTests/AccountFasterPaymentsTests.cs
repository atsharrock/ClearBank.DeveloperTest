using ClearBank.DeveloperTest.Tests.Helpers;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests
{
    [TestClass]
    public class AccountFasterPaymentsTests
    {

        [TestMethod]
        public void FasterPayments_Account_IsNull_IsNotValid()
        {
            var validPaymentRequest = BacsPaymentRequestHelper.CreateValidBacsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate((Account)null);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Account cannot be null");
        }

        [TestMethod]
        public void FasterPayments_PaymentSchemeAllowed_IsValid()
        {
            var account = AccountHelper.CreateFasterPaymentsAccount(10);
            var validPaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(0);

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_PaymentSchemeNotAllowed_IsNotValid()
        {
            var account = AccountHelper.CreateFasterPaymentsAccount(10);
            var validPaymentRequest = FasterPaymentsRequestHelper.CreateInValidFasterPaymentsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Payment Scheme is not allowed.");
        }

        [TestMethod]
        public void FasterPayments_BalanceAboveRequestAmount_IsValid()
        {
            var account = AccountHelper.CreateFasterPaymentsAccount(10);
            var validPaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(2);

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_BalanceEqualToRequestAmount_IsValid()
        {
            var account = AccountHelper.CreateFasterPaymentsAccount(10);
            var validPaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(10);

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_BalanceLowerThanRequestAmount_IsNotValid()
        {
            var account = AccountHelper.CreateFasterPaymentsAccount(10);
            var validPaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(10.01m);

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Balance must be bigger or equal to the requested amount.");
        }
    }
}
