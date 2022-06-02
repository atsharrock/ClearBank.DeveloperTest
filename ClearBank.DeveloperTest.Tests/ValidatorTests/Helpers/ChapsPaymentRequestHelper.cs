using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests.Helpers
{
    public static class ChapsPaymentRequestHelper
    {
        public static MakePaymentRequest CreateValidChapsPaymentRequest()
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
            };
        }

        public static MakePaymentRequest CreateInValidChapsPaymentRequest()
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
            };
        }
    }
}
