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
    [InlineData("00", false)]
    [InlineData("02", false)]
    [InlineData("11", false)]
    [InlineData("12", false)]
    [InlineData("21", false)]
    [InlineData("22", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void ternary01OnceAtEndAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, ternary01OnceAtEnd));

    /*
      problem 54
      ternary number with 01 occurring once at the end and anywhere else
    */
    const string ternary01AtEnd = @"^([12]|0(0|(10))*(2|(1[12])))*0(0|(10))*1$";
    
    [Theory]
    [InlineData("01", true)]
    [InlineData("120001", true)]
    [InlineData("12002101", true)]
    [InlineData("111110000002222201", true)]
    [InlineData("212101", true)]
    [InlineData("0101", true)]
    [InlineData("000010001", true)]
    [InlineData("00001001", true)]
    [InlineData("00", false)]
    [InlineData("02", false)]
    [InlineData("11", false)]
    [InlineData("12", false)]
    [InlineData("21", false)]
    [InlineData("22", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void ternary01AtEndAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, ternary01AtEnd));

    /*
      problem 55 - 58
      more of the same, skipping those
    */
}