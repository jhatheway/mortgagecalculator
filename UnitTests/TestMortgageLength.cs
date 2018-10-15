using System;
using Xunit;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace UnitTests
{
    public class TestMortgageLength
    {
        [Fact]
        public void TestValidLengths()
        {
            // Valid lengths of mortgages is >5 years and <25
            PaymentScheduleOptions periodPerYear = PaymentScheduleOptions.monthly;
            Assert.True(ValidateValidMortgageLength.Validate(5 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(15 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(25 * (int)periodPerYear, periodPerYear));

            periodPerYear = PaymentScheduleOptions.weekly;
            Assert.True(ValidateValidMortgageLength.Validate(5 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(15 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(25 * (int)periodPerYear, periodPerYear));

            periodPerYear = PaymentScheduleOptions.biweekly;
            Assert.True(ValidateValidMortgageLength.Validate(5 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(15 * (int)periodPerYear, periodPerYear));
            Assert.True(ValidateValidMortgageLength.Validate(25 * (int)periodPerYear, periodPerYear));
        }

        [Fact]
        public void TestInvalidLengths()
        {
            // Valid lengths of mortgages is >5 years and <25
            PaymentScheduleOptions periodPerYear = PaymentScheduleOptions.monthly;
            Assert.False(ValidateValidMortgageLength.Validate(1 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(4 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(30 * (int)periodPerYear, periodPerYear));

            periodPerYear = PaymentScheduleOptions.weekly;
            Assert.False(ValidateValidMortgageLength.Validate(1 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(4 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(30 * (int)periodPerYear, periodPerYear));

            periodPerYear = PaymentScheduleOptions.biweekly;
            Assert.False(ValidateValidMortgageLength.Validate(1 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(4 * (int)periodPerYear, periodPerYear));
            Assert.False(ValidateValidMortgageLength.Validate(30 * (int)periodPerYear, periodPerYear));
        }
    }
}