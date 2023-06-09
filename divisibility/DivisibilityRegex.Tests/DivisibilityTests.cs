using System.Text.RegularExpressions;

namespace DivisibilityRegex.Tests;

public class DivisibilityTests
{
    /*
      r' = (2r + b) mod 2
      0 | 0 1
      1 | 0 1
    */
    string binaryDivisibleBy2 = @"^[01]*0$"; // problem 1
    
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(10)]
    [InlineData(357110)]
    public void EvenValuesAreDivisibleBy2(int number) => binaryDivisibleBy2.AssertIsMatch(number); 
    
    [Theory]
    [InlineData(3)]
    [InlineData(9)]
    [InlineData(31)]
    public void OddValuesAreNotDivisibleBy2(int number) => binaryDivisibleBy2.AssertAreNotMatch(number);
}

public static class UtilityExtensions
{
    public static string ToBinaryString(this int value) => Convert.ToString(value, 2);
    public static Regex ToRegex(this string value) => new Regex(value);
    public static void AssertIsMatch(this string regex, int number)
      => Assert.True(regex.ToRegex().IsMatch(number.ToBinaryString()));
    public static void AssertAreNotMatch(this string regex, int number)
      => Assert.False(regex.ToRegex().IsMatch(number.ToBinaryString()));
}