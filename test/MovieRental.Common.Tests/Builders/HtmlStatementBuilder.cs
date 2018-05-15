using System;
using System.Linq;

namespace MovieRental.Common.Tests.Builders
{
    public class HtmlStatementBuilder : StatementBuilderBase
    {
        private readonly string _htmlStatementTemplate = @"<H1>Rentals for <EM>{0}</EM></H1><P>" + Environment.NewLine + "{1}<P>You owe <EM>{2}</EM></P>On this rental you earned <EM>{3}</EM> frequent renter points <P>";

        internal override string Build()
        {
            var rentalPart = string.Empty;  

            foreach(var expectedRentalValue in _expectedRentalValues)
            {
                rentalPart += $"{_customTitle}: {expectedRentalValue}<BR>{Environment.NewLine}";
            }
            return string.Format(_htmlStatementTemplate, _sampleCustomer, rentalPart, _expectedRentalValues.Sum(), _expectedRentedPoints);
        }
    }
}
