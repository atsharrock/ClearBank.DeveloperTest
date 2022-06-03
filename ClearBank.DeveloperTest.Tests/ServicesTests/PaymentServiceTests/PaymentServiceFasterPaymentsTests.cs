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
    public class PaymentServiceFasterPaymentsTests
    {
        private Mock<IDataStore> _dataStoreMock = new Mock<IDataStore>();

        public PaymentServiceFasterPaymentsTests()
        {
            _dataStoreMock.Setup(s => s.GetAccount(It.IsAny<string>()))
                .Returns(AccountHelper.CreateFasterPaymentsAccount(10));
        }

        [TestMethod]
        public void FasterPayments_PaymentSchemeAllowed_ReturnsResult_True()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(0);

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_PaymentSchemeNotAllowed_ReturnsResult_False()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = FasterPaymentsRequestHelper.CreateInValidFasterPaymentsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }

        [TestMethod]
        public void FasterPayments_BalanceAboveRequestAmount_ReturnsResult_True()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(2);

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_BalanceEqualToRequestAmount_ReturnsResult_True()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(10);

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void FasterPayments_BalanceLowerThanRequestAmount_ReturnsResult_False()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = FasterPaymentsRequestHelper.CreateValidFasterPaymentsPaymentRequest(10.01m);

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }
    }
}
