using System.Text.RegularExpressions;

namespace PatternsRegex.Tests;

public class PatternsRegexTests
{
    /*
      problem 16
      binary
    */
    const string leadingZeroesBinary = @"^[01]*$";
    const string noLeadingZeroesBinary = @"^0?$|^1[01]*$"; // @"(^0?$)|(^1[01]*$)" IMHO is easier to read
    
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
    public void leadingBinary_GivenStringMatchesTheExpectedResult(string given, bool leading, bool noLeading)
    {
        Assert.Equal(leading, Regex.IsMatch(given, leadingZeroesBinary));
        Assert.Equal(noLeading, Regex.IsMatch(given, noLeadingZeroesBinary));
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
    {
        Assert.Equal(expected, Regex.IsMatch(given, base10Integers));
    }
}