using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class ValidateDownPaymentLargeEnough
    {
        private const decimal DownpaymentIncrement = 500000.00m;
        public static bool Validate(decimal askingPrice, decimal downPayment)
        {
            decimal minimumDownPayment = 0.0m;
            // First, if the downpayment is less than 20% of the asking price,
            // and the asking price is over $1mil, we have to fail right away,
            // since the house can't get mortgage insurance
            if (askingPrice >= 1000000.00m && (downPayment / askingPrice <= 0.20m))
            {
                return false;
            }

            // Calculate the minimum downpayment: 5% of the first $500k
            if (askingPrice < DownpaymentIncrement)
            {
                minimumDownPayment = askingPrice * 0.05m;
            }
            else
            {
                // Any sum above $500k must be 10%
                minimumDownPayment = DownpaymentIncrement * 0.05m;
                minimumDownPayment += (askingPrice - DownpaymentIncrement) * 0.1m;
            }
                                       
            // If the downpayment is at least as much as the minimum, we are ok
            return downPayment >= minimumDownPayment;
        }
    }
}
