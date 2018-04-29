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
                double thisAmount = 0;

                //determine amounts for each line
                switch (rental.Movie.PriceCode)
                {
                    case Movie.REGULAR:
                    {
                        thisAmount += 2;
                        if (rental.DaysRented > 2)
                        {
                            thisAmount += (rental.DaysRented - 2) * 1.5;
                        }
                        break;
                    }
                    case Movie.NEW_RELEASE:
                    {
                        thisAmount += rental.DaysRented * 3;
                        break;
                    }
                    case Movie.CHILDRENS:
                    {
                        thisAmount += 1.5;
                        if (rental.DaysRented > 3)
                        {
                            thisAmount += (rental.DaysRented - 3) * 1.5;
                        }
                        break;
                    }
                }

                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if (rental.Movie.PriceCode == Movie.NEW_RELEASE && rental.DaysRented > 1)
                {
                    frequentRenterPoints++;
                }

                //show figures for this rental
                result += $"\t{rental.Movie.Title}\t{thisAmount}{Environment.NewLine}";
                totalAmount += thisAmount;
            }

            //add footer lines
            result += $"Amount owed is {totalAmount}{Environment.NewLine}";
            result += $"You earned {frequentRenterPoints} frequent renter points";
        
            return result;
        }
    }
}
