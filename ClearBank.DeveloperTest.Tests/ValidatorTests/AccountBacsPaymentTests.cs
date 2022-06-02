using System.Linq;
using ClearBank.DeveloperTest.Tests.ValidatorTests.Helpers;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests
{
    [TestClass]
    public class AccountBacsPaymentTests
    {
        [TestMethod]
        public void BacsPayment_Account_IsNull_IsNotValid()
        {
            var validPaymentRequest = BacsPaymentRequestHelper.CreateValidBacsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate((Account)null);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Account cannot be null");
        }

        [TestMethod]
        public void BacsPayment_PaymentSchemeAllowed_IsValid()
        {
            var account = AccountHelper.CreateBacsAccount();
            var validPaymentRequest = BacsPaymentRequestHelper.CreateValidBacsPaymentRequest();

            var accountValidator = new AccountValidator(validPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void BacsPayment_PaymentSchemeNotAllowed_IsNotValid()
        {
            var account = AccountHelper.CreateBacsAccount();
            var invalidPaymentRequest = BacsPaymentRequestHelper.CreateInValidBacsPaymentRequest(PaymentScheme.FasterPayments);

            var accountValidator = new AccountValidator(invalidPaymentRequest);
            var result = accountValidator.Validate(account);

            result.IsValid.Should().BeFalse();
            result.Errors[0].ErrorMessage.Should().Contain("Payment Scheme is not allowed.");
        }
    }
}
