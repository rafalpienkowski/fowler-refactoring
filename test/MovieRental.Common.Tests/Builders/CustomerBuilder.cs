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

        public CustomerBuilder WithRental(int priceCode, int daysRented)
        {
            var movie = new MovieBuilder(priceCode).Build();
            var rental = new Rental(movie, daysRented);
            _rentals.Add(rental);
            return this;
        }

        public CustomerBuilder WithRental(int priceCode, int daysRented, string customTitle)
        {
            var movie = new MovieBuilder(priceCode).WithTitle(customTitle).Build();
            var rental = new Rental(movie, daysRented);
            _rentals.Add(rental);
            return this;
        }

        internal Customer Build()
        {
            var customer = new Customer(_name);
            foreach(var rental in _rentals)
            {
                customer.AddRental(rental);
            }            
            return customer;
        }
    }
}