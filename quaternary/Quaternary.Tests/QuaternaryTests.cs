using System.Text.RegularExpressions;

namespace Quaternary.Tests;

public class QuaternaryTests
{
    /*
      problem 59
      quaternary number with 01 only occurring once at the end
    */
    const string quaternary01OnceAtEnd = @"^([123]|(00*[23]))*00*1$";
    
    [Theory]
    [InlineData("01", true)]
    [InlineData("1230001", true)]
    [InlineData("1230032101", true)]
    [InlineData("11111000000222220000003333301", true)]
    [InlineData("32132101", true)]
    [InlineData("0101", false)]
    [InlineData("000010001", false)]
    [InlineData("00001001", false)]
    [InlineData("00", false)]
    [InlineData("02", false)]
    [InlineData("03", false)]
    [InlineData("11", false)]
    [InlineData("12", false)]
    [InlineData("13", false)]
    [InlineData("21", false)]
    [InlineData("22", false)]
    [InlineData("23", false)]
    [InlineData("30", false)]
    [InlineData("31", false)]
    [InlineData("32", false)]
    [InlineData("33", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void quaternary01OnceAtEnd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, quaternary01OnceAtEnd));
}