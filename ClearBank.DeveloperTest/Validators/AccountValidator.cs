using ClearBank.DeveloperTest.Types;
using FluentValidation;
using FluentValidation.Results;

namespace ClearBank.DeveloperTest.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator(MakePaymentRequest paymentRequest)
        {
            When(_ => paymentRequest.PaymentScheme == PaymentScheme.Bacs, () =>
            {
                RuleFor(a => a)
                    .Must(a => a.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                    .WithMessage($"Payment Scheme is not allowed.");
            });

            When(_ => paymentRequest.PaymentScheme == PaymentScheme.FasterPayments, () =>
            {
                RuleFor(a => a)
                    .Must(a => a.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                    .WithMessage("Payment Scheme is not allowed.");

                RuleFor(a => a.Balance).Must(b => b >= paymentRequest.Amount)
                    .WithMessage("Balance must be bigger or equal to the requested amount.");
            });

            When(_ => paymentRequest.PaymentScheme == PaymentScheme.Chaps, () =>
            {
                RuleFor(a => a)
                    .Must(a => a.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                    .WithMessage("Payment Scheme is not allowed.");

                RuleFor(a => a.Status).Must(s => s == AccountStatus.Live)
                    .WithMessage("The account status must be 'Live'.");
            });

        }

        protected override bool PreValidate(ValidationContext<Account> context, ValidationResult result)
        {
            if (context.InstanceToValidate != null) return base.PreValidate(context, result);
            result.Errors.Add(new ValidationFailure("Account", "Account cannot be null"));
            return false;
        }
    }
}
