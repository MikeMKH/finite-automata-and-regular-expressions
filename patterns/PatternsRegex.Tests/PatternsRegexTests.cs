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
    public void GivenStringMatchesTheExpectedResult(string given, bool leading, bool noLeading)
    {
        Assert.Equal(leading, Regex.IsMatch(given, leadingZeroesBinary));
        Assert.Equal(noLeading, Regex.IsMatch(given, noLeadingZeroesBinary));
    }
}