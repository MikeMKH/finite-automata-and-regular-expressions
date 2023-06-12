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
    const string decimalDivisibleBy2 = @"^([02468]|([13579]+[02468]))+$";
    
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
    
    /*
      problem 3
      r' = (2r + b) mod 4
        b 0 1
      r -----
      0 | 0 1
      1 | 2 3
      2 | 0 1
      3 | 2 3
    */
    const string binaryDivisibleBy4 = @"^(0|(1(1|(01))*)0{2})+$";
    
    [Fact]
    public void ZeroToOneThousandTimes4IsDivisibleBy4()
    {
      for(var i = 0; i <= 1_000; i++)
      {
        binaryDivisibleBy4.AssertBinaryIsMatch(i * 4);
      }
    }
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy4()
    {
      for(var i = 1; i <= 10_000; i++)
      {
        if (i % 4 == 0) binaryDivisibleBy4.AssertBinaryIsMatch(i);
        else binaryDivisibleBy4.AssertBinaryAreNotMatch(i);
      }
    }
    
    /*
      problem 4
      2^n needs n 0s to return back to the final state
    */
    Func<int, string> divisibleBy2nthPower =
      n => @"^0|([01]*0{" + n + @"})+$";
    
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(10)]
    public void UseValuesZeroToTenThousandToVerifyDivisibleBy2nthPower(int n)
    {
      var regex = divisibleBy2nthPower(n);
      var value = Math.Pow(2, n);
      for (var i = 0; i <= 10_000; i++)
      {
        if (i % value == 0) regex.AssertBinaryIsMatch(i);
        else regex.AssertBinaryAreNotMatch(i);
      }
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
      => Assert.True(regex.ToRegex().IsMatch(to(number)), $"/{regex}/ {to(number)}=={number}");
    public static void AssertAreNotMatch(this string regex, Func<int, string> to, int number)
      => Assert.False(regex.ToRegex().IsMatch(to(number)), $"/{regex}/ {to(number)}=={number}");
}