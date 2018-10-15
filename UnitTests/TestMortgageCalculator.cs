using System;
using Xunit;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace UnitTests
{
    public class TestMortgageCalculator
    {
        [Fact]
        public void TestMortgageCalculation()
        {
            GetMortgageAmountInput testInput = new GetMortgageAmountInput
            {
                PaymentAmount = 3137.0251267506815,
                DownPayment = 158000.00,
                PaymentSchedule = PaymentScheduleOptions.monthly,
                AmortizationPeriod = 240
            };

            var paymentCalculator = new MortgageAmountCalculator(testInput, 0.025);
            var mortgageAmount = paymentCalculator.Calculate();
            Assert.Equal<double>(750000.0, mortgageAmount);
        }

        [Fact]
        public void TestAnotherMortgageCalculation()
        {
            GetMortgageAmountInput testInput = new GetMortgageAmountInput
            {
                PaymentAmount = 1292.0161941406197,
                DownPayment = 72000.00,
                PaymentSchedule = PaymentScheduleOptions.monthly,
                AmortizationPeriod = 300
            };

            var paymentCalculator = new MortgageAmountCalculator(testInput, 0.025);
            var mortgageAmount = paymentCalculator.Calculate();
            Assert.Equal<double>(360000.00, mortgageAmount);
        }
    }
}