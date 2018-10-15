using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class ValidateDownPaymentLargeEnough
    {
        private const double DownpaymentIncrement = 500000.00;
        public static bool Validate(double askingPrice, double downPayment)
        {
            double minimumDownPayment = 0.0;
            // First, if the downpayment is less than 20% of the asking price,
            // and the asking price is over $1mil, we have to fail right away,
            // since the house can't get mortgage insurance
            if (askingPrice >= 1000000.00)
            {
                if (downPayment / askingPrice >= 0.20)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Calculate the minimum downpayment: 5% of the first $500k
            if (askingPrice < DownpaymentIncrement)
            {
                minimumDownPayment = askingPrice * 0.05;
            }
            else
            {
                // Any sum above $500k must be 10%
                minimumDownPayment = DownpaymentIncrement * 0.05;
                minimumDownPayment += (askingPrice - DownpaymentIncrement) * 0.1;
            }
                                       
            // If the downpayment is at least as much as the minimum, we are ok
            return downPayment >= minimumDownPayment;
        }
    }
}
