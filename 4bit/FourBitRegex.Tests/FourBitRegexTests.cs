using System.Text.RegularExpressions;

namespace FourBitRegex.Tests;

public class FourBitRegexTests
{
    /*
      problem 37
      binary number with 0000 only occurring once at the end
    */
    const string binary0000OnceAtEnd = @"^(1|(0(1|(0(1|(01))))))*0000$";
    
    [Theory]
    [InlineData("0000", true)]
    [InlineData("10000", true)]
    [InlineData("1010000", true)]
    [InlineData("010000", true)]
    [InlineData("10100011110110000", true)]
    [InlineData("00000", false)]
    [InlineData("000010000", false)]
    [InlineData("0000100", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binary0000OnceAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binary0000OnceAtEnd));

    /*
      problem 38
      binary number with 0000 occurring once at the end and anywhere else
    */
    const string binary0000AtEnd = @"^(1|(0(1|(0(1|0(1|(00*1)))))))*00000*$";
    
    [Theory]
    [InlineData("0000", true)]
    [InlineData("10000", true)]
    [InlineData("1010000", true)]
    [InlineData("010000", true)]
    [InlineData("10100011110110000", true)]
    [InlineData("00000", true)]
    [InlineData("000010000", true)]
    [InlineData("10100111101100001000010000", true)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binary0000AtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binary0000AtEnd));

}