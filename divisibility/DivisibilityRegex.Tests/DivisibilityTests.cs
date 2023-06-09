using System.Text.RegularExpressions;

namespace DivisibilityRegex.Tests;

public class DivisibilityTests
{
    /*
      problem 1
      r' = (2r + b) mod 2
        b 0 1
      r -----
      0 | 0 1
      1 | 0 1
    */
    const string binaryDivisibleBy2 = @"^[01]*0$";
    
    /*
      problem 2
      r' = (10r + d) mod 2
        d 0 1 2 3 4 5 6 7 8 9
      r ---------------------
      0 | 0 1 0 1 0 1 0 1 0 1
      1 | 0 1 0 1 0 1 0 1 0 1
    */
    const string decimalDivisibleBy2 = @"^([02468]|([13579]+[02468]))*$";
    
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(10)]
    [InlineData(357110)]
    [InlineData(2468)]
    [InlineData(8642)]
    public void EvenValuesAreDivisibleBy2(int number)
    {
      binaryDivisibleBy2.AssertBinaryIsMatch(number);
      decimalDivisibleBy2.AssertDecimalIsMatch(number);
    }
    
    [Theory]
    [InlineData(3)]
    [InlineData(9)]
    [InlineData(31)]
    [InlineData(13579)]
    [InlineData(97531)]
    public void OddValuesAreNotDivisibleBy2(int number)
    {
      binaryDivisibleBy2.AssertBinaryAreNotMatch(number);
      decimalDivisibleBy2.AssertDecimalAreNotMatch(number);
    }
}

public static class UtilityExtensions
{
    public static string ToBinaryString(this int value) => Convert.ToString(value, 2);
    public static Regex ToRegex(this string value) => new Regex(value);
    public static void AssertBinaryIsMatch(this string regex, int number)
      => regex.AssertIsMatch(n => n.ToBinaryString(), number);
    public static void AssertBinaryAreNotMatch(this string regex, int number)
      => regex.AssertAreNotMatch(n => n.ToBinaryString(), number);
    public static void AssertDecimalIsMatch(this string regex, int number)
      => regex.AssertIsMatch(n => n.ToString(), number);
    public static void AssertDecimalAreNotMatch(this string regex, int number)
      => regex.AssertAreNotMatch(n => n.ToString(), number);
    public static void AssertIsMatch(this string regex, Func<int, string> to, int number)
      => Assert.True(regex.ToRegex().IsMatch(to(number)));
    public static void AssertAreNotMatch(this string regex, Func<int, string> to, int number)
      => Assert.False(regex.ToRegex().IsMatch(to(number)));
}