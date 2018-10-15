using System;
using Xunit;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace UnitTests
{
    public class TestDownPaymentLargeEnough
    {
        // Mortgages over $1mil don't qualify for mortgage insurance, so has to have at least 20% down
        [Fact]
        public void TestCheckForOver1Million()
        {
            Assert.True(ValidateDownPaymentLargeEnough.Validate(1250000.00, 500000.00));
            Assert.False(ValidateDownPaymentLargeEnough.Validate(1250000.00, 5000.00));
            Assert.False(ValidateDownPaymentLargeEnough.Validate(1250000.00, 187500.00));
        }

        // Downpayment has to be at least 5% of $500k and < , and 10% of the amounts above $500k
        [Fact]
        public void TestDownPaymentValidation()
        {
            // Amounts > 500k
            Assert.False(ValidateDownPaymentLargeEnough.Validate(700000.00, 0.00));
            Assert.False(ValidateDownPaymentLargeEnough.Validate(700000.00, 5000.00));
            Assert.True(ValidateDownPaymentLargeEnough.Validate(700000.00, 50001.00));

            // Amounts < 500k
            Assert.False(ValidateDownPaymentLargeEnough.Validate(300000.00, 0.00));
            Assert.False(ValidateDownPaymentLargeEnough.Validate(300000.00, 5000.00));
            Assert.True(ValidateDownPaymentLargeEnough.Validate(300000.00, 20000.00));
        }
    }
}