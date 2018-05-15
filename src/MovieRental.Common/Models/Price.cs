namespace MovieRental.Common.Models
{
    public abstract class Price
    {
        public abstract int PriceCode { get; }

        public abstract double GetCharge(int daysRented);

        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
    }    
}