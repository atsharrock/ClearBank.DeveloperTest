using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Tests.Helpers
{
    public static class FasterPaymentsRequestHelper
    {
        public static MakePaymentRequest CreateValidFasterPaymentsPaymentRequest(Decimal amount)
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
                Amount = amount
            };
        }

        public static MakePaymentRequest CreateInValidFasterPaymentsPaymentRequest()
        {
            return new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
            };
        }
    }
}
