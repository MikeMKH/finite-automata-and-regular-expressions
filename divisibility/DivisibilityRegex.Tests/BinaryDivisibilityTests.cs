using System.Text.RegularExpressions;

namespace DivisibilityRegex.Tests;

public class BinaryDivisibilityTests
{
    string divisibleBy2 = @"0*1*0+";
    
    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(10)]
    [InlineData(357110)]
    public void EvenValuesAreDivisibleBy2(int number) => divisibleBy2.AssertIsMatch(number); 
    
    [Theory]
    [InlineData(3)]
    public void OddValuesAreNotDivisibleBy2(int number) => divisibleBy2.AssertAreNotMatch(number);
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