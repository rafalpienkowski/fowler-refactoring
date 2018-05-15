using System;
using System.Collections.Generic;

namespace MovieRental.Common.Tests.Builders
{
    public abstract class StatementBuilderBase
    {
        protected string _sampleCustomer = "John Kowalsky";
        protected List<double> _expectedRentalValues = new List<double>();
        protected int _expectedRentedPoints = 0;
        protected string _customTitle = "Example title";

        public StatementBuilderBase WithCustomer(string customer)
        {
            _sampleCustomer = customer;
            return this;
        }

        public StatementBuilderBase WithExpectedRentedPoints(int expectedRentedPoints)
        {
            _expectedRentedPoints = expectedRentedPoints;
            return this;
        }

        public StatementBuilderBase WithCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }

        internal StatementBuilderBase WithExpectedRentalValue(double expectedRentalValue)
        {
            _expectedRentalValues.Add(expectedRentalValue);
            return this;
        }

        internal abstract string Build();
    }
}
