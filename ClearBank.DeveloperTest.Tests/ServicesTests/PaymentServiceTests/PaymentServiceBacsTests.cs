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
    public class PaymentServiceBacsTests
    {
        private Mock<IDataStore> _dataStoreMock = new Mock<IDataStore>();

        public PaymentServiceBacsTests()
        {
            _dataStoreMock.Setup(s => s.GetAccount(It.IsAny<string>()))
                .Returns(AccountHelper.CreateBacsAccount());
        }

        [TestMethod]
        public void BacsPayment_PaymentSchemeAllowed_ReturnsResult_True()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);

            var makePaymentRequest = BacsPaymentRequestHelper.CreateValidBacsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void BacsPayment_PaymentSchemeNotAllowed_ReturnsResult_False()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);
            var makePaymentRequest = BacsPaymentRequestHelper.CreateInValidBacsPaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }
    }
}
