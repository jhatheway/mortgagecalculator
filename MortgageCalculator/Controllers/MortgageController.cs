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
        //    decimal NewRate - the new rate to be used
        // Output: 
        //    decimal OldRate - the old rate
        //    decimal NewRate - the new rate that was just set
        [HttpPatch("interest-rate")]
        public ActionResult<PatchInterestRateResult> PatchInterestRate([FromBody] PatchInterestRateInput input)
        {
            var rate = new InterestRate(this.Configuration);
            var oldrate = rate.Rate;
            rate.Rate = input.NewRate;
            return new PatchInterestRateResult { OldRate = oldrate, NewRate = rate.Rate };
        }

        // Purpose: Calculates the recurring payment for a mortgage, given a number of inputs
        //
        // Verb/URI: GET /payment-amount
        // Input: 
        //    decimal AskingPrice - the price of the home
        //    decimal DownPayment - the downpayment being made 
        //    enum    PaymentSchedule - one of "monthly", "biweekly", "weekly"
        //    int     Amortization - loan amortization period, in years    
        // Output: 
        //    decimal PaymentAmount - the payment amount per scheduled payment
        [HttpGet("payment-amount")]
        public ActionResult<GetPaymentAmountResult> GetPaymentAmount([FromBody] GetPaymentAmountInput input)
        {
            var interestRate = new InterestRate(this.Configuration);
            var paymentCalculator = new PaymentCalculator(input, interestRate.Rate);
            return new GetPaymentAmountResult { PaymentAmount = paymentCalculator.Calculate() };
        }

        // GET /
        [HttpGet]
        public ActionResult<string> Get()
        {
            return new string("Run the script ./tests.sh to show the various endpoints of this program being exercised");
        }
    }
}
