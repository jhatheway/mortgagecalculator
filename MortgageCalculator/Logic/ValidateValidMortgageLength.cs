using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class ValidateValidMortgageLength
    {
        public static bool Validate(int amortizationPeriod, PaymentScheduleOptions paymentSchedule)
        {
            double years = amortizationPeriod / (double)paymentSchedule;
            return (years >= 5.0 && years <= 25.0);
        }
    }
}