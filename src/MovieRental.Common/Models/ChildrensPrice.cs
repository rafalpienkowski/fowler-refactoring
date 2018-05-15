namespace MovieRental.Common.Models
{
    public class ChildrensPrice : Price
    {
        public override int PriceCode => Movie.CHILDRENS;
    }
}