using System;
using MortgageCalculator.Models;

namespace MortgageCalculator.Logic
{
    public class ValidatePaymentScheduleOptions
    {
        public static bool Validate(PaymentScheduleOptions value)
        {
            Type enumType = value.GetType();
            bool valid = Enum.IsDefined(enumType, value);
            return valid;
        }
    }
}
