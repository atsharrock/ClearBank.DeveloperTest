using ClearBank.DeveloperTest.Tests.Helpers;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests
{
    [TestClass]
    public class AccountChapsPaymentTests
    {

        [TestMethod]
        public void ChapsPayment_Account_IsNull_IsNotValid()
        {
            var validPaymentRequest = BacsPaymentRequestHelper.CreateValidBacsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate((Account)null);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Account cannot be null");
        }

        [TestMethod]
        public void ChapsPayment_PaymentSchemeAllowed_IsValid()
        {
            var account = AccountHelper.CreateChapsAccount(AccountStatus.Live);
            var validPaymentRequest = ChapsPaymentRequestHelper.CreateValidChapsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void ChapsPayment_PaymentSchemeNotAllowed_IsNotValid()
        {
            var account = AccountHelper.CreateChapsAccount(AccountStatus.Live);
            var validPaymentRequest = ChapsPaymentRequestHelper.CreateInValidChapsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Payment Scheme is not allowed.");
        }

        [TestMethod]
        public void ChapsPayment_AccountDisabled_IsNotValid()
        {
            var account = AccountHelper.CreateChapsAccount(AccountStatus.Disabled);
            var validPaymentRequest = ChapsPaymentRequestHelper.CreateValidChapsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("The account status must be 'Live'.");
        }
    }
}