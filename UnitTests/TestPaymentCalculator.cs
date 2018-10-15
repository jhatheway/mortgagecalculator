using System;
using Xunit;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace UnitTests
{
    public class TestPaymentCalculator
    {
        [Fact]
        public void TestPaymentCalculation()
        {
            GetPaymentAmountInput testInput = new GetPaymentAmountInput
            {
                AskingPrice = 750000.00,
                DownPayment = 158000.00,
                PaymentSchedule = PaymentScheduleOptions.monthly,
                AmortizationPeriod = 240
            };

            var paymentCalculator = new PaymentCalculator(testInput, 0.025);
            var paymentAmount = paymentCalculator.Calculate();
            Assert.Equal<double>(3137.0251267506815, paymentAmount);
        }

        [Fact]
        public void TestAnotherPaymentCalculation()
        {
            GetPaymentAmountInput testInput = new GetPaymentAmountInput
            {
                AskingPrice = 360000.00,
                DownPayment = 72000.00,
                PaymentSchedule = PaymentScheduleOptions.monthly,
                AmortizationPeriod = 300
            };

            var paymentCalculator = new PaymentCalculator(testInput, 0.025);
            var paymentAmount = paymentCalculator.Calculate();
            Assert.Equal<double>(1292.0161941406197, paymentAmount);
        }
    }
}