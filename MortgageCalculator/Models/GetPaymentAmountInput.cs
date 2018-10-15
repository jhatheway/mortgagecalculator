using System;

namespace MortgageCalculator.Models
{
    public class GetPaymentAmountInput
    {
        public decimal AskingPrice;
        public decimal DownPayment;
        public PaymentScheduleOptions PaymentSchedule;
        public int AmortizationPeriod;
    }
}
