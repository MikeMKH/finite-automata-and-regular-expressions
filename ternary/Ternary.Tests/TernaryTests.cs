using System.Text.RegularExpressions;

namespace Ternary.Tests;

public class TernaryTests
{
    /*
      problem 53
      ternary number with 01 only occurring once at the end
    */
    const string ternary01OnceAtEnd = @"^([12]|(00*2))*00*1$";
    
    [Theory]
    [InlineData("01", true)]
    [InlineData("120001", true)]
    [InlineData("12002101", true)]
    [InlineData("111110000002222201", true)]
    [InlineData("212101", true)]
    [InlineData("0101", false)]
    [InlineData("000010001", false)]
    [InlineData("00001001", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void ternary01OnceAtEndAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, ternary01OnceAtEnd));

}