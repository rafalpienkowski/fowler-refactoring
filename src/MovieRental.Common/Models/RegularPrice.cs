namespace MovieRental.Common.Models
{
    public class RegularPrice : Price
    {
        public override int PriceCode => Movie.REGULAR;
    }
}