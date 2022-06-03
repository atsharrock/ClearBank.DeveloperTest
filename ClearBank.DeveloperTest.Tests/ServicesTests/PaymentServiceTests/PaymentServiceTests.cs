using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClearBank.DeveloperTest.Tests.ServicesTests.PaymentServiceTests
{
    [TestClass]
    public class PaymentServiceTests
    {
        Mock<IDataStore> _dataStoreMock = new Mock<IDataStore>();

        [TestMethod]
        public void MakePayment_AccountIsNull_ReturnsResult_False()
        {
            var paymentService = new PaymentService(_dataStoreMock.Object);
            var makePaymentRequest = new MakePaymentRequest();

            var result = paymentService.MakePayment(makePaymentRequest);
            result.Should().BeOfType<MakePaymentResult>();
            result.Success.Should().BeFalse();
        }
    }
}