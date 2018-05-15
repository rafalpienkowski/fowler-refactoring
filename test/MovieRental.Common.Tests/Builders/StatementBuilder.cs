using System;
using System.Linq;

namespace MovieRental.Common.Tests.Builders
{
    public class StatementBuilder : StatementBuilderBase
    {
        private readonly string _statementTemplate = @"Rental Record for {0}" + Environment.NewLine + "{1}Amount owed is {2}" + Environment.NewLine + "You earned {3} frequent renter points";

        internal override string Build()
        {
            var rentalPart = string.Empty;  

            foreach(var expectedRentalValue in _expectedRentalValues)
            {
                rentalPart += $"\t{_customTitle}\t{expectedRentalValue}{Environment.NewLine}";
            }

            return string.Format(_statementTemplate, _sampleCustomer, rentalPart, _expectedRentalValues.Sum(), _expectedRentedPoints);
        }
    }
}
