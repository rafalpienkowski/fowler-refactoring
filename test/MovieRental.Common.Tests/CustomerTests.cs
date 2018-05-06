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

    public abstract class StatementBuilderBase
    {
        protected string _sampleCustomer = "John Kowalsky";
        internal abstract string Build();
    }

    public class StatementBuilder : StatementBuilderBase
    {
        private readonly string _statementTemplate = @"Rental Record for {0}" + Environment.NewLine + "{1}Amount owed is {2}" + Environment.NewLine + "You earned {3} frequent renter points";

        internal override string Build()
        {
            return string.Format(_statementTemplate, _sampleCustomer, string.Empty, 0, 0);
        }
    }

    public class HtmlStatementBuilder : StatementBuilderBase
    {
        private readonly string _htmlStatementTemplate = @"<H1>Rentals for <EM>{0}</EM></H1><P>" + Environment.NewLine + "{1}<P>You owe <EM>{2}</EM></P>On this rental you earned <EM>{3}</EM> frequent renter points <P>";

        internal override string Build()
        {
            return string.Format(_htmlStatementTemplate, _sampleCustomer, string.Empty, 0, 0);
        }
    }

    public class CustomerTests
    {
        private static readonly Func<Customer,string> _statementCall = (c) => { return c.Statement(); };
        private static readonly Func<Customer,string> _htmlStatementCall = (c) => { return c.Statement(); };

        private readonly string _sampleCustomer = "John Kowalsky";
        private readonly string _statementTemplate = @"Rental Record for {0}" + Environment.NewLine + "{1}Amount owed is {2}" + Environment.NewLine + "You earned {3} frequent renter points";


        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { 
                new Func<string> =() => { return new StatementBuilder().Build(); }, 
                new CustomerBuilder().Build(), 
                _statementCall 
            };

            //yield return new object[] { new HtmlStatementBuilder().Build(), new CustomerBuilder().Build(), _htmlStatementCall };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Statement_SampleCustomerWithoutRentalsData(Expression<Func<string>> expectedResultExpr, Expression<Func<Customer>> customerBuilderExpr, Func<Customer,string> customerCall)
        {
            // arrange
            var expectedResultFunc = expectedResultExpr.Compile();
            var expectedResult = expectedResultFunc();
            var custromerFunc = customerBuilderExpr.Compile();
            var customer = custromerFunc();

            // act
            var result = customerCall(customer);

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Statement_CustomerWithGivenNameWithoutRentals()
        {
            // arrange
            var customerName = "Alice Novak";
            var expectedResult = string.Format(_statementTemplate, customerName, string.Empty, 0, 0);
            var custromer = new CustomerBuilder()
                    .WithName(customerName)
                    .Build();

            // act
            var result = custromer.Statement();

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData(1, 3, 9, 2)] // New release movie rented for 3 days
        [InlineData(0, 2, 2, 1)] // Normal movie rented for 2 days
        [InlineData(2, 7, 7.5, 1)] // Children movie rented for 7 days
        public void Statement_SampleCustomerWithOneMovieRented(int priceCode, int daysRented, double expectedRentalValue, int expectedRenterPoints)
        {
            // arrange
            var rentalPart = $"\tExample title	{expectedRentalValue}{Environment.NewLine}";
            var expectedResult = string.Format(_statementTemplate, _sampleCustomer, rentalPart, expectedRentalValue, expectedRenterPoints);
            var customer = new CustomerBuilder().Build();
            AddRentalToCustomer(customer, priceCode, daysRented);

            // act
            var result = customer.Statement();

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Statement_SampleCustomerWithOneMovieWithCustomTitileRented()
        {
            // arrange
            var priceCode = 0;
            var daysRented = 3;
            var expectedRentalValue = 3.5;
            var customTitle = "Custom title";
            var rentalPart = $"\t{customTitle}\t{expectedRentalValue}{Environment.NewLine}";
            var expectedResult = string.Format(_statementTemplate, _sampleCustomer, rentalPart, expectedRentalValue, 1);
            var customer = new CustomerBuilder().Build();
            var movie = new MovieBuilder(priceCode).WithTitle(customTitle).Build();
            var rental = new Rental(movie, daysRented);
            customer.AddRental(rental);

            // act
            var result = customer.Statement();

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData(new[] { 1, 1 }, 3 , new double[] { 9, 9 }, 4)] // Two new release movie rented for 3 days
        [InlineData(new [] { 0, 0 }, 2, new double[] { 2, 2 }, 2)] // Two normal movie rented for 2 days
        [InlineData(new[] { 2, 2, 2 }, 7, new double[] { 7.5, 7.5, 7.5 }, 3)] // Three children movie rented for 7 days
        [InlineData(new[] { 0, 1, 2 }, 4, new double[] { 5, 12, 3 }, 4)] // Normal, new release and children movie rented for 4 days
        public void Statement_SampleCustomerWithMultipleMoviesRenter(int[] priceCodes, int daysRented, double[] expectedRentalValues, int expectedRenterPoints)
        {
            var rentalPart = "";
            foreach(var expectedRentalValue in expectedRentalValues)
            {
                rentalPart += $"\tExample title	{expectedRentalValue}{Environment.NewLine}";
            }
            var expectedResult = string.Format(_statementTemplate, _sampleCustomer, rentalPart, expectedRentalValues.Sum(), expectedRenterPoints);
            var customer = new CustomerBuilder().Build();
            foreach(var priceCode in priceCodes)
            {
                AddRentalToCustomer(customer, priceCode, daysRented);
            }           

            // act
            var result = customer.Statement();

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    
        private void AddRentalToCustomer(Customer customer, int priceCode, int daysRented)
        {
            var movie = new MovieBuilder(priceCode).Build();
            var rental = new Rental(movie, daysRented);
            customer.AddRental(rental);
        }

    }
}
