namespace MovieRental.Common.Models
{
    public class NewReleasePrice : Price
    {
        public override int PriceCode => Movie.NEW_RELEASE;
    }
}