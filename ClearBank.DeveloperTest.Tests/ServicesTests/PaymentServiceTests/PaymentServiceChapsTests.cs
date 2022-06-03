using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Tests.Helpers;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClearBank.DeveloperTest.Tests.ServicesTests.PaymentServiceTests
{
    [TestClass]
    public class PaymentServiceChapsTests
    {
        private Mock<IDataStore> _dataStoreMock = new Mock<IDataStore>();

        public PaymentServiceChapsTests()
        {
            _dataStoreMock.Setup(s => s.GetAccount(It.IsAny<string>()))
                .Returns(AccountHelper.CreateChapsAccount(AccountStatus.Live));
        }

        [TestMethod]
        public void ChapsPayment_PaymentSchemeAllowed_ReturnsResult_True()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = ChapsPaymentRequestHelper.CreateValidChapsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void ChapsPayment_PaymentSchemeNotAllowed_ReturnsResult_False()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = ChapsPaymentRequestHelper.CreateInValidChapsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }

        [TestMethod]
        public void ChapsPayment_AccountDisabled_ReturnsResult_False()
        {
            _dataStoreMock.Setup(s => s.GetAccount(It.IsAny<string>()))
                .Returns(AccountHelper.CreateChapsAccount(AccountStatus.Disabled));

            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = ChapsPaymentRequestHelper.CreateValidChapsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }
    }
}
