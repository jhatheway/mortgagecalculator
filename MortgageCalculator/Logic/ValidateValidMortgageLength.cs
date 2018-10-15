using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class ValidateValidMortgageLength
    {
        public static bool Validate(int amortizationPeriod, PaymentScheduleOptions paymentSchedule)
        {
            decimal years = (decimal)amortizationPeriod / (decimal)paymentSchedule;
            return (years >= 5.0m && years <= 20.0m);
        }
    }
}