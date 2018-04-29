using System.Collections.Generic;
using MovieRental.Common.Models;

namespace MovieRental.Common.Tests.Builders
{
    internal class CustomerBuilder
    {
        private string _name = "John Kowalsky";
        private ICollection<Rental> _rentals = new List<Rental>();

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder WithRental(Rental rental)
        {
            _rentals.Add(rental);
            return this;
        }

        internal Customer Build()
        {
            return new Customer(_name);
        }
    }
}