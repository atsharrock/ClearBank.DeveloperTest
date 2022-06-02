using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.ValidatorTests.Helpers
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
