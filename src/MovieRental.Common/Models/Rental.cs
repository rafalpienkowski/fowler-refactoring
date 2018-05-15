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
                return Movie.GetFrequentRenterPoints(DaysRented);
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
