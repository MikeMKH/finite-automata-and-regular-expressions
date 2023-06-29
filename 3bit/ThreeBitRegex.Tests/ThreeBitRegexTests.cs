using System.Text.RegularExpressions;

namespace ThreeBitRegex.Tests;

public class ThreeBitRegexTests
{
    /*
      problem 29
      binary number with 000 only occurring once at the end
    */
    const string binary000OnceAtEnd = @"^(1|(0(0?1)))*000$";
    
    [Theory]
    [InlineData("000", true)]
    [InlineData("1000", true)]
    [InlineData("101000", true)]
    [InlineData("01000", true)]
    [InlineData("101001111011000", true)]
    [InlineData("0000", false)]
    [InlineData("0001000", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binary000OnceAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binary000OnceAtEnd));
    
}