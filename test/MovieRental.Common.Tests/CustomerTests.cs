using System;
using Xunit;
using MovieRental.Common.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using MovieRental.Common.Tests.Builders;
using System.Linq.Expressions;

namespace MovieRental.Common.Tests
{

    public class CustomerTests
    {
        private static readonly Func<Customer,string> _statementCall = (c) => { return c.Statement(); };
        private static readonly Func<Customer,string> _htmlStatementCall = (c) => { return c.HtmlStatement(); };
        private static string _customCustomer = "Alice Novak";

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] {
                new CustomerBuilder().Build(), 
                _statementCall,
                new StatementBuilder().Build(),
                "Simple customer without rentals statement"
            };

            yield return new object[] {                
                new CustomerBuilder().Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().Build(),
                "Simple customer without rentals html staement"
            };

            yield return new object[] {
                new CustomerBuilder().WithName(_customCustomer).Build(),
                _statementCall,
                new StatementBuilder().WithCustomer(_customCustomer).Build(),
                "Custom customer without rentals statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithName(_customCustomer).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithCustomer(_customCustomer).Build(),
                "Custom customer without rentals html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(1,3).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(9).WithExpectedRentedPoints(2).Build(),
                "Simple customer with new release movies rented for 3 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(1,3).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(9).WithExpectedRentedPoints(2).Build(),
                "Simple customer with new release movise rented for 3 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,2).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(2).WithExpectedRentedPoints(1).Build(),
                "Simple customer with normal movies rented for 2 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,2).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(2).WithExpectedRentedPoints(1).Build(),
                "Simple customer with normal movies rented for 2 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(2,7).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(7.5).WithExpectedRentedPoints(1).Build(),
                "Simple customer with new childern movies rented for 7 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(2,7).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(7.5).WithExpectedRentedPoints(1).Build(),
                "Simple customer with new childern movies rented for 7 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(1,3).WithRental(1,3).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(9).WithExpectedRentalValue(9).WithExpectedRentedPoints(4).Build(),
                "Simple customer with two new realese movies rented for 3 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(1,3).WithRental(1,3).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(9).WithExpectedRentalValue(9).WithExpectedRentedPoints(4).Build(),
                "Simple customer with two new realese movies rented for 3 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,2).WithRental(0,2).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(2).WithExpectedRentalValue(2).WithExpectedRentedPoints(2).Build(),
                "Simple customer with two notmal movies rented for 2 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,2).WithRental(0,2).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(2).WithExpectedRentalValue(2).WithExpectedRentedPoints(2).Build(),
                "Simple customer with two normal movies rented for 2 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(2,7).WithRental(2,7).WithRental(2,7).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(7.5).WithExpectedRentalValue(7.5).WithExpectedRentalValue(7.5).WithExpectedRentedPoints(3).Build(),
                "Simple customer with tree children movies rented for 7 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(2,7).WithRental(2,7).WithRental(2,7).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(7.5).WithExpectedRentalValue(7.5).WithExpectedRentalValue(7.5).WithExpectedRentedPoints(3).Build(),
                "Simple customer with tree children movies rented for 7 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,4).WithRental(1,4).WithRental(2,4).Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(5).WithExpectedRentalValue(12).WithExpectedRentalValue(3).WithExpectedRentedPoints(4).Build(),
                "Simple customer with normal, new release and children movie rented for 4 days statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,4).WithRental(1,4).WithRental(2,4).Build(),
                _htmlStatementCall,
                new HtmlStatementBuilder().WithExpectedRentalValue(5).WithExpectedRentalValue(12).WithExpectedRentalValue(3).WithExpectedRentedPoints(4).Build(),
                "Simple customer with normal, new release and children movie rented for 4 days html statement"
            };

            yield return new object[] {
                new CustomerBuilder().WithRental(0,3, "Custom title").Build(),
                _statementCall,
                new StatementBuilder().WithExpectedRentalValue(3.5).WithExpectedRentedPoints(1).WithCustomTitle("Custom title").Build(),
                "Simple customer with one movie with custom title statement"
            };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Customer_GenericData(Customer customer, Func<Customer,string> customerCall, string expectedResult, string errorMessage)
        {
            // act
            var result = customerCall(customer);

            // assert
            result.Should().BeEquivalentTo(expectedResult, errorMessage);
        }
    }
}
