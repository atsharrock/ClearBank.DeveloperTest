using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClearBank.DeveloperTest.Tests.ServicesTests
{
    [TestClass]
    public class PaymentServiceTests
    {
        [TestMethod]
        public void MakePayment_ReturnsMakePaymentResult()
        {
            var mockedDataStore = new Mock<IDataStore>();
            var paymentService = new PaymentService(mockedDataStore.Object);
            var makePaymentRequest = new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 10,
            };

            paymentService.MakePayment(makePaymentRequest);

        }
    }
}
