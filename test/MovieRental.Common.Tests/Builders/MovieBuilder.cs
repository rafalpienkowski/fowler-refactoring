using MovieRental.Common.Models;

namespace MovieRental.Common.Tests.Builders
{
    internal class MovieBuilder
    {
        private string _title = "Example title";
        private readonly int _priceCode = 0;

        internal MovieBuilder(int priceCode)
        {
            _priceCode = priceCode;
        }

        public MovieBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }
        public Movie Build()
        {
            return new Movie(_title, _priceCode);
        }
    }
}