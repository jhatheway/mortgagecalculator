using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class MortgageAmountCalculator
    {
        private double PaymentAmount;
        private double DownPayment;
        private PaymentScheduleOptions PaymentSchedule;
        private int AmortizationPeriod;
        private double InterestRate;

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

            // Make sure the mortgage period is valid (not less than 5 years, not more than 25)
            if (!ValidateValidMortgageLength.Validate(this.AmortizationPeriod, this.PaymentSchedule))
            {
                throw new Exception("Invalid AmortizationPeriod - Must be more than 5 years, less than 25");
            }
        }

        public MortgageAmountCalculator(GetMortgageAmountInput input, double interestRate)
        {
            this.PaymentAmount = input.PaymentAmount;
            this.DownPayment = input.DownPayment;
            this.PaymentSchedule = input.PaymentSchedule;
            this.AmortizationPeriod = input.AmortizationPeriod;
            this.InterestRate = interestRate;

            // Make sure the incoming parameters make sense
            this.Validate();
        }

        public double Calculate()
        {
            // The formula for calculating mortgage is:
            // P = L[c(1 + c)^n]/[(1 + c)^n - 1]
            // where l = loan principal
            //       c = interest rate expressed in the period for n (ie. annualized reduced to a week period)
            //       n = number of payments
            // this time we need to solve for l, so dividing both sides by [c(1 + c)^n]/[(1 + c)^n - 1]
            // will yield the maximum principal
            double p = this.PaymentAmount;
            double c = this.InterestRate / ((double)this.PaymentSchedule);
            double n = this.AmortizationPeriod;

            // First calculate the (1+c)^n part
            double m = Math.Pow((1 + c), n);
            // Calculate the rest of [c(1 + c)^n]/[(1 + c)^n - 1]
            double divisor = ((c * m) / (m - 1));
            // Divide our payment by the divisor to solve for the loan principal
            double result = p / divisor;
            result += this.DownPayment;
            return result;
        }
    }
}
