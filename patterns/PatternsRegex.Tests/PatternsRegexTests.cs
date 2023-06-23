using System.Text.RegularExpressions;

namespace PatternsRegex.Tests;

public class PatternsRegexTests
{
    /*
      problem 16
      binary
    */
    const string binaryLeadingZeroes = @"^[01]*$";
    const string binaryNoLeadingZeroes = @"^0?$|^1[01]*$"; // @"(^0?$)|(^1[01]*$)" IMHO is easier to read
    
    [Theory]
    [InlineData("", true, true)]
    [InlineData("0", true, true)]
    [InlineData("00", true, false)]
    [InlineData("1", true, true)]
    [InlineData("11", true, true)]
    [InlineData("10", true, true)]
    [InlineData("010", true, false)]
    [InlineData("110", true, true)]
    [InlineData("0110", true, false)]
    [InlineData("0100", true, false)]
    [InlineData("hello", false, false)]
    [InlineData("h3110", false, false)]
    [InlineData("4311o", false, false)]
    public void binary_GivenStringMatchesTheExpectedResult(string given, bool leading, bool noLeading)
    {
        Assert.Equal(leading, Regex.IsMatch(given, binaryLeadingZeroes));
        Assert.Equal(noLeading, Regex.IsMatch(given, binaryNoLeadingZeroes));
    }
    
    /*
      problem 17
      base 10 int
    */
    const string base10Integers = @"^0?$|^[1-9][0-9]*$";
    
    [Theory]
    [InlineData("", true)]
    [InlineData("0", true)]
    [InlineData("01", false)]
    [InlineData("1", true)]
    [InlineData("10", true)]
    [InlineData("1234567890", true)]
    [InlineData("8", true)]
    [InlineData("0123456789", false)]
    [InlineData("hello", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    public void base10Integers_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, base10Integers));
    
    /*
      problem 18
      base 10 decimal
    */
    const string base10Decimal = @"^(0?|([1-9][0-9]*))(\.[0-9]+)?$";
    
    [Theory]
    [InlineData("1234567890", true)]
    [InlineData("0", true)]
    [InlineData("1", true)]
    [InlineData("0.0", true)]
    [InlineData(".0", true)]
    [InlineData("0.1234567890", true)]
    [InlineData("0.0987654321", true)]
    [InlineData("1234567890.0987654321", true)]
    [InlineData("123.456789", true)]
    [InlineData(".", false)]
    [InlineData("1.", false)]
    [InlineData("1234567890.", false)]
    [InlineData("0.", false)]
    [InlineData("hello", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void base10Decimal_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, base10Decimal));
      
    /*
      problem 19
      binary number with exactly three 1s
    */
    
    const string binaryExactlyThee1 = @"^(0*10*){3}$";
    
    [Theory]
    [InlineData("111", true)]
    [InlineData("0101010", true)]
    [InlineData("000010001000010000000000", true)]
    [InlineData("0000110001", true)]
    [InlineData("111000", true)]
    [InlineData("01110", true)]
    [InlineData("011101", false)]
    [InlineData("011102", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binaryExactlyThee1_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binaryExactlyThee1));
}