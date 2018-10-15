using System;
using Microsoft.Extensions.Configuration;

namespace MortgageCalculator.Logic
{
    public class InterestRate
    {
        private readonly IConfiguration Configuration;
        private decimal _rate;

        public decimal Rate 
        {
            get
            {
                return _rate;
            }

            set
            {
                Validate(value);
                this.Configuration["InterestRate"] = value.ToString();
                this._rate = value;
            }
        }

        // Throws if the interest rate isn't valid (negative or greater than 100%)
        public void Validate(decimal value)
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
