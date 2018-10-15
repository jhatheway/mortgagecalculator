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

        // PATCH interest-rate
        [HttpPatch("interest-rate")]
        public ActionResult<PatchInterestRateResult> PatchInterestRateInput([FromBody] PatchInterestRateInput input)
        {
            var rate = new InterestRate(this.Configuration);
            var oldrate = rate.Rate;
            rate.Rate = input.NewRate;
            return new PatchInterestRateResult { OldRate = oldrate, NewRate = rate.Rate };
        }

        // GET /
        [HttpGet]
        public ActionResult<string> Get()
        {
            return new string("Run the script ./tests.sh to show the various endpoints of this program being exercised");
        }
    }
}
