using System.Text.RegularExpressions;

namespace Circular.Tests;

public class CircularTests
{
    /*
      problem 65
      circular string over the alphabet {0,1} that do not contain 00
    */
    const string circularString00NotContain = @"^0?1(1|(01))*0?$";
    
    [Theory]
    [InlineData("01", true)]
    [InlineData("11", true)]
    [InlineData("10", true)]
    [InlineData("1010101010101010101", true)]
    [InlineData("01110111011110", true)]
    [InlineData("01001", false)]
    [InlineData("001", false)]
    [InlineData("1100", false)]
    [InlineData("00", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void circularString00NotContain_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, circularString00NotContain));

}