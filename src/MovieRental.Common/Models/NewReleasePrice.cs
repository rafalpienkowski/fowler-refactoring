namespace MovieRental.Common.Models
{
    public class NewReleasePrice : Price
    {
        public override int PriceCode => Movie.NEW_RELEASE;

        public override double GetCharge(int daysRented)
        {
            return daysRented * 3;
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return (daysRented > 1) ? 2 : 1;
        }
    }
}