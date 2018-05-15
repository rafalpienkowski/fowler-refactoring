using System;

namespace MovieRental.Common.Models
{
    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public string Title { get; private set; } 
        public int PriceCode { 
            get
            {
                return price.PriceCode;
            } 
            set
            {
                switch(value)
                {
                    case REGULAR:
                        price = new RegularPrice();
                        break;
                    case NEW_RELEASE:
                        price = new NewReleasePrice();
                        break;
                    case CHILDRENS:
                        price = new ChildrensPrice();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid price code: {value}");
                }
            }
        }

        private Price price;

        public Movie(string title, int priceCode)
        {
            Title = title;
            PriceCode = priceCode;
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return price.GetFrequentRenterPoints(daysRented);
        }

        public double GetCharge(int daysRented)
        {
            return price.GetCharge(daysRented);
        }
    }
}
