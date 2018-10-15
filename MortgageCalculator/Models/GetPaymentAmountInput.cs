using System;

namespace MortgageCalculator.Models
{
    public class GetPaymentAmountInput
    {
        public double AskingPrice;
        public double DownPayment;
        public PaymentScheduleOptions PaymentSchedule;
        public int AmortizationPeriod;
    }
}
