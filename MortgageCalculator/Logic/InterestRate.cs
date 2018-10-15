using System;
using Microsoft.Extensions.Configuration;

namespace MortgageCalculator.Logic
{
    public class InterestRate
    {
        private readonly IConfiguration Configuration;
        private decimal _rate;

        // Normally I would use an automatic getter/setter, but I need custom behaviour
        // on the setter.. so unfortunately that means I need to define both
        public decimal Rate 
        {
            get
            {
                return _rate;
            }

            set
            {
                // Validate the value, and if its OK we also set it back to our config file
                Validate(value);
                this.Configuration["InterestRate"] = value.ToString();
                this._rate = value;
            }
        }

        // Throws if the interest rate isn't valid (negative or greater than 100%)
        private void Validate(decimal value)
        {
            if (value < 0.0m || value > 1.0m)
            {
                throw new Exception("Invalid Interest Rate supplied (can't be less than 0 (0.0) or greater than 100% (1.0)");
            }
        }

        public InterestRate(IConfiguration config)
        {
            this.Configuration = config;
            this._rate = this.Configuration.GetValue<decimal>("InterestRate");
        }
    }
}
