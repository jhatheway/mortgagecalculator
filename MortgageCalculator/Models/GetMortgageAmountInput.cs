using System;

namespace MortgageCalculator.Models
{
    public class GetMortgageAmountInput
    {
        public double PaymentAmount;
        public double DownPayment;
        public PaymentScheduleOptions PaymentSchedule;
        public int AmortizationPeriod;
    }
}
