using System;

namespace MovieRental.Common.Models
{
    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public string Title { get; private set; } 
        public int PriceCode { get; set; }

        public Movie(string title, int priceCode)
        {
            Title = title;
            PriceCode = priceCode;
        }

        public double GetCharge(int daysRented)
        {
            double result = 0;
            switch (PriceCode)
            {
                case Movie.REGULAR:
                {
                    result += 2;
                    if (daysRented > 2)
                    {
                        result += (daysRented - 2) * 1.5;
                    }
                    break;
                }
                case Movie.NEW_RELEASE:
                {
                    result += daysRented * 3;
                    break;
                }
                case Movie.CHILDRENS:
                {
                    result += 1.5;
                    if (daysRented > 3)
                    {
                        result += (daysRented - 3) * 1.5;
                    }
                    break;
                }
            }

            return result;
        }
    }
}
