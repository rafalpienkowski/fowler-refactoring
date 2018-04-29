namespace MovieRental.Common.Models
{
    public class Rental
    {
        public Movie Movie { get; private set; }
        public int DaysRented { get; private set; }

        public int FrequentRenterPoints
        {
            get
            {
                var frequentRenterPoints = 1;
                // add bonus for a two day new release rental
                if (Movie.PriceCode == Movie.NEW_RELEASE && DaysRented > 1)
                {
                    frequentRenterPoints++;
                }
                return frequentRenterPoints;
            }
        }

        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }

        public double GetCharge()
        {
            double result = 0;
            switch (Movie.PriceCode)
            {
                case Movie.REGULAR:
                {
                    result += 2;
                    if (DaysRented > 2)
                    {
                        result += (DaysRented - 2) * 1.5;
                    }
                    break;
                }
                case Movie.NEW_RELEASE:
                {
                    result += DaysRented * 3;
                    break;
                }
                case Movie.CHILDRENS:
                {
                    result += 1.5;
                    if (DaysRented > 3)
                    {
                        result += (DaysRented - 3) * 1.5;
                    }
                    break;
                }
            }

            return result;
        }
    }
}
