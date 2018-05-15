namespace MovieRental.Common.Models
{
    public class NewReleasePrice : Price
    {
        public override int PriceCode => Movie.NEW_RELEASE;

        public override double GetCharge(int daysRented)
        {
            return daysRented * 3;
        }
    }
}