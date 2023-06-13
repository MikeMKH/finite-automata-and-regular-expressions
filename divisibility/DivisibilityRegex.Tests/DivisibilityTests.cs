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
      binaryDivisibleBy2.AssertBinaryNoMatch(number);
      decimalDivisibleBy2.AssertDecimalNoMatch(number);
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
        else binaryDivisibleBy4.AssertBinaryNoMatch(i);
      }
    }
    
    /*
      problem 4
      2^n needs n 0s to return back to the final state
    */
    Func<int, string> divisibleBy2nthPower = n => @"^0|([01]*0{" + n + @"})+$";
    
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
        else regex.AssertBinaryNoMatch(i);
      }
    }
    
    /*
      problem 5
      x mod 2 = 0 but not x mod 2^n = 0 where n > 1
      value must end with 10
    */
    const string binaryDivisibleBy2ButNoOtherPowerOf2 = @"^(0*11*0)+$";
    
    [Fact]
    public void VerifyDivisibleBy2ButNoOtherPowerOf2()
    {
      for(var i = 3; i <= 10_000; i++)
      {
        if (isDivisibleBy2(i) && !isDivisibleByPowerOf2GreaterThan2(i))
          binaryDivisibleBy2ButNoOtherPowerOf2.AssertBinaryIsMatch(i);
        else binaryDivisibleBy2ButNoOtherPowerOf2.AssertBinaryNoMatch(i);
      }
      
      bool isDivisibleBy2(int x) => x % 2 == 0;
      bool isPowerOf2(int x) => x != 0 && ((x & (x - 1)) == 0);
      bool isDivisibleByPowerOf2GreaterThan2(int x)
      {
        if (x == 4) return true;
        for (var i = 4; i < x; i++)
        {
          if (isPowerOf2(i) && x % i == 0) return true;
        }
        return false;
      }
    }
    
    
    /*
      problem 6
      r' = (2r + b) mod 3
        b 0 1
      r -----
      0 | 0 1
      1 | 2 0
      2 | 1 2
    */
    const string binaryDivisibleBy3 = @"^(0|(1(01*0)*1))*$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy3()
    {
      for(var i = 1; i <= 10_000; i++)
      {
        if (i % 3 == 0) binaryDivisibleBy3.AssertBinaryIsMatch(i);
        else binaryDivisibleBy3.AssertBinaryNoMatch(i);
      }
    }
}

public static class UtilityExtensions
{
    public static string ToBinaryString(this int value) => Convert.ToString(value, 2);
    public static Regex ToRegex(this string value) => new Regex(value);
    public static void AssertBinaryIsMatch(this string regex, int number)
      => regex.AssertIsMatch(n => n.ToBinaryString(), number);
    public static void AssertBinaryNoMatch(this string regex, int number)
      => regex.AssertNoMatch(n => n.ToBinaryString(), number);
    public static void AssertDecimalIsMatch(this string regex, int number)
      => regex.AssertIsMatch(n => n.ToString(), number);
    public static void AssertDecimalNoMatch(this string regex, int number)
      => regex.AssertNoMatch(n => n.ToString(), number);
    public static void AssertIsMatch(this string regex, Func<int, string> to, int number)
      => Assert.True(regex.ToRegex().IsMatch(to(number)), $"/{regex}/ {to(number)}=={number}");
    public static void AssertNoMatch(this string regex, Func<int, string> to, int number)
      => Assert.False(regex.ToRegex().IsMatch(to(number)), $"/{regex}/ {to(number)}=={number}");
}