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
      
    /*
      problem 30
      binary number with 000 occurring once at the end and anywhere else
    */
    const string binary000AtEnd = @"^(1|(0(1|(0(1|(00*1))))))*0000*$";
    
    [Theory]
    [InlineData("000", true)]
    [InlineData("1000", true)]
    [InlineData("101000", true)]
    [InlineData("01000", true)]
    [InlineData("101001111011000", true)]
    [InlineData("0000", true)]
    [InlineData("0001000", true)]
    [InlineData("10100111101100010001000", true)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binary000AtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binary000AtEnd));

    /*
      problem 31
      binary number with 001 only occurring once at the end
    */
    const string binary001OnceAtEnd = @"^(1|(01))*00*1$";
    
    [Theory]
    [InlineData("001", true)]
    [InlineData("1001", true)]
    [InlineData("101001", true)]
    [InlineData("01001", true)]
    [InlineData("10101111011001", true)]
    [InlineData("0000", false)]
    [InlineData("1111", false)]
    [InlineData("0001000", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binary001OnceAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binary001OnceAtEnd));
}