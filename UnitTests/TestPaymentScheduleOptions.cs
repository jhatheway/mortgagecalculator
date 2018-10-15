using System;
using Xunit;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace UnitTests
{
    public class TestPaymentScheduleOptions
    {
        // A valid schedule option is in the enum defined in Models/PaymentScheduleOptions.
        // They are 12 for monthly, 26 for biweekly and 52 for weekly (payment periods per year)
        // This is mostly to help in validating that a correct enum was received, otherwise we get
        // a default value passed in by ASP.net
        [Fact]
        public void TestValidScheduleOptions()
        {
            Assert.True(ValidatePaymentScheduleOptions.Validate(PaymentScheduleOptions.monthly));
            Assert.True(ValidatePaymentScheduleOptions.Validate(PaymentScheduleOptions.biweekly));
            Assert.True(ValidatePaymentScheduleOptions.Validate(PaymentScheduleOptions.weekly));
        }

        [Fact]
        public void TestInvalidScheduleOptions()
        {
            Assert.False(ValidatePaymentScheduleOptions.Validate(0));
            Assert.False(ValidatePaymentScheduleOptions.Validate((PaymentScheduleOptions)10));
            Assert.False(ValidatePaymentScheduleOptions.Validate((PaymentScheduleOptions)200));
        }
    }
}