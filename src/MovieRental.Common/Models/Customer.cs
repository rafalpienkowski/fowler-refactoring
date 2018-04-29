using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.Common.Models
{
    public class Customer
    {
        public string Name { get; private set; }
        private ICollection<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            Name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }    

        public string Statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = $"Rental Record for {Name}{Environment.NewLine}";

            foreach(var rental in _rentals)
            {
                // add frequent renter points
                frequentRenterPoints += rental.FrequentRenterPoints;

                //show figures for this rental
                result += $"\t{rental.Movie.Title}\t{rental.GetCharge()}{Environment.NewLine}";
                totalAmount += rental.GetCharge();
            }

            //add footer lines
            result += $"Amount owed is {totalAmount}{Environment.NewLine}";
            result += $"You earned {frequentRenterPoints} frequent renter points";
        
            return result;
        }
    }
}
