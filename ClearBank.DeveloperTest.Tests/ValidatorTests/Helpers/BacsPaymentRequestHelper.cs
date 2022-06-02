using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests.Helpers
{
    public static class BacsPaymentRequestHelper
    {
        public static MakePaymentRequest CreateValidBacsPaymentRequest()
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
            };
        }

        public static MakePaymentRequest CreateInValidBacsPaymentRequest(PaymentScheme paymentScheme)
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = paymentScheme,
            };
        }
    }
}
