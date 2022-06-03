using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Helpers
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

        public static MakePaymentRequest CreateInValidBacsPaymentRequest()
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
            };
        }
    }
}
