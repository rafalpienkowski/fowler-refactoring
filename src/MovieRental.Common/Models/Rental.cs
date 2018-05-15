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
            return Movie.GetCharge(DaysRented);
        }
    }
}
