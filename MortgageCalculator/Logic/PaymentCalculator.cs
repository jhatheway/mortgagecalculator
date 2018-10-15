using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class PaymentCalculator
    {
        private decimal AskingPrice;
        private decimal DownPayment;
        private PaymentScheduleOptions PaymentSchedule;
        private int AmortizationPeriod;
        private decimal InterestRate;

        // Makes sure we can actually calculate a payment:
        // - The amortization period enum is valid
        // - The down payment is big enough
        // - The mortgage period is valid
        private void Validate()
        {
            // Make sure the amortization period enum is valid
            if (!ValidatePaymentScheduleOptions.Validate(this.PaymentSchedule))
            {
                throw new Exception("Invalid PaymentSchedule. Valid values are monthly, biweekly, weekly");
            }

            // Make sure the down payment is big enough
            if (!ValidateDownPaymentLargeEnough.Validate(this.AskingPrice, this.DownPayment))
            {
                throw new Exception("Downpayment is insufficent for the AskingPrice.");
            }

            // Make sure the mortgage period is valid (not less than 5 years, not more than 25)
            if (!ValidateValidMortgageLength.Validate(this.AmortizationPeriod, this.PaymentSchedule))
            {
                throw new Exception("Invalid AmortizationPeriod - Must be more than 5 years, less than 25");
            }
        }

        public static decimal CalculateRequiredMortgageInsurance(decimal askingPrice, decimal downPayment)
        {
            // No mortgage insurance on homes >= 1mil
            if (askingPrice >= 1000000.00m)
            {
                return 0.0m;
            }

            decimal downpaymentRatio = downPayment / askingPrice;
            if (downpaymentRatio >= 0.05m && downpaymentRatio < 0.1m)
            {
                return (askingPrice - downPayment) * 0.0315m;
            } 
            else if (downpaymentRatio >= 0.1m && downpaymentRatio < 0.15m)
            {
                return (askingPrice - downPayment) * 0.024m;
            }
            else if (downpaymentRatio >= 0.15m && downpaymentRatio < 0.2m)
            {
                return (askingPrice - downPayment) * 0.018m;
            }

            // Drop here, its >= 20% down (or <5%, but that should have been caught earlier by the validator)
            return 0.0m;
        }

        public PaymentCalculator(GetPaymentAmountInput input, decimal interestRate)
        {
            this.AskingPrice = input.AskingPrice;
            this.DownPayment = input.DownPayment;
            this.PaymentSchedule = input.PaymentSchedule;
            this.AmortizationPeriod = input.AmortizationPeriod;
            this.InterestRate = interestRate;

            // Make sure the incoming parameters make sense
            this.Validate();
        }

        public double Calculate()
        {
            // Calculate if mortgage insurance is needed, and if so, add to the principal
            this.AskingPrice += CalculateRequiredMortgageInsurance(this.AskingPrice, this.DownPayment);

            // The formula for calculating mortgage is:
            // P = L[c(1 + c)^n]/[(1 + c)^n - 1]
            // where l = loan principal
            //       c = interest rate expressed in the period for n (ie. annualized reduced to a week period)
            //       n = number of payments
            double l = ((double)this.AskingPrice) - ((double)this.DownPayment); 
            double c = ((double)this.InterestRate) / ((double)this.PaymentSchedule);
            double n = (double)(this.AmortizationPeriod);

            // First calculate the (1+c)^n part
            double m = Math.Pow((1 + c), n);
            double result = l * ((c * m) / (m - 1));
            return result;
        }
    }
}
