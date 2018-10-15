using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MortgageCalculator.Logic;
using MortgageCalculator.Models;

namespace MortgageCalculator.Controllers
{
    [Route("")]
    [ApiController]
    public class MortgageController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public MortgageController(IConfiguration config)
        {
            this.Configuration = config;
        }

        // Purpose: Replaces the interest rate being used by the given one
        //
        // Verb/URI: PATCH /interest-rate
        // Input: 
        //    double NewRate - the new rate to be used
        // Output: 
        //    double OldRate - the old rate
        //    double NewRate - the new rate that was just set
        [HttpPatch("interest-rate")]
        public ActionResult<PatchInterestRateResult> PatchInterestRate([FromBody] PatchInterestRateInput input)
        {
            var rate = new InterestRate(this.Configuration);
            var oldrate = rate.Rate;
            rate.Rate = input.NewRate;
            return new PatchInterestRateResult { OldRate = oldrate, NewRate = rate.Rate };
        }

        // Purpose: Calculates the recuring payment for a mortgage, given a number of inputs
        //
        // Verb/URI: GET /payment-amount
        // Input: 
        //    double AskingPrice - the price of the home
        //    double DownPayment - the downpayment being made 
        //    enum    PaymentSchedule - one of "monthly", "biweekly", "weekly"
        //    int     AmortizationPeriod - loan amortization period, in years    
        // Output: 
        //    double PaymentAmount - the payment amount per scheduled payment
        [HttpGet("payment-amount")]
        public ActionResult<GetPaymentAmountResult> GetPaymentAmount([FromBody] GetPaymentAmountInput input)
        {
            var interestRate = new InterestRate(this.Configuration);
            var paymentCalculator = new PaymentCalculator(input, interestRate.Rate);
            return new GetPaymentAmountResult { PaymentAmount = paymentCalculator.Calculate() };
        }

        // Purpose: Calculates the maximum mortgage, given a payment and some other inputs
        //
        // Verb/URI: GET /mortgage-amount
        // Input: 
        //    double PaymentAmount - the amount per payment period
        //    double DownPayment - the downpayment being made (optional)
        //    enum    PaymentSchedule - one of "monthly", "biweekly", "weekly"
        //    int     AmortizationPeriod - loan amortization period, in years    
        // Output: 
        //    double MortgageAmount - the maximum mortgage amount for the criteria given
        [HttpGet("mortgage-amount")]
        public ActionResult<GetMortgageAmountResult> GetMortgageAmount([FromBody] GetMortgageAmountInput input)
        {
            var interestRate = new InterestRate(this.Configuration);
            var mortgageAmountCalculator = new MortgageAmountCalculator(input, interestRate.Rate);
            return new GetMortgageAmountResult { PaymentAmount = mortgageAmountCalculator.Calculate() };
        }


        // Purpose: Display some helpful text when you run the project from Visual studio or
        // simply try to go to the first URI you might think of.
        //
        // Verb/URI: GET /
        // Input: N/A
        // Output: some helpful text
        [HttpGet]
        public ActionResult<string> Get()
        {
            return new string("Please see the project's Readme.md to see how to use this");
        }
    }
}
