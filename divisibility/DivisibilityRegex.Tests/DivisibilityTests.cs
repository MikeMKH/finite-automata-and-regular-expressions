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
      for(var value = 0; value <= 1_000; value++)
      {
        binaryDivisibleBy4.AssertBinaryIsMatch(value * 4);
      }
    }
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy4()
      => binaryDivisibleBy4.VerifyBinaryIsDivisibleBy(4);
    
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
      regex.VerifyBinaryIsDivisibleBy(value);
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
      for(var value = 3; value <= 10_000; value++)
      {
        if (value.isDivisibleBy(2) && !isDivisibleByPowerOf2GreaterThan2(value))
          binaryDivisibleBy2ButNoOtherPowerOf2.AssertBinaryIsMatch(value);
        else binaryDivisibleBy2ButNoOtherPowerOf2.AssertBinaryNoMatch(value);
      }
      
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
      => binaryDivisibleBy3.VerifyBinaryIsDivisibleBy(3);
    
    /*
      problem 7
      r' = (10r + d) mod 3
        d 0 1 2 3 4 5 6 7 8 9
      r ---------------------
      0 | 0 1 2 0 1 2 0 1 2 0
      1 | 1 2 0 1 2 0 1 2 0 1
      2 | 2 0 1 2 0 1 2 0 1 2
    */
    const string decimalDivisibleBy3 =
      @"^([0369]|([258][0369]*[147])|(([147]|([258][0369]*[258]))([0369]|([147][0369]*[258]))*([258]|([147][0369]*[147]))))*$";

    [Fact]
    public void UseValuesOneToTenThousandToVerifyDecimalDivisibleBy3()
      => decimalDivisibleBy3.VerifyDecimalIsDivisibleBy(3);
       
    /*
      problem 8
      x mod 3 = 0 but not x mod 2 = 0
      value must be divisible by 3 but not end with 0
    */
    const string binaryDivisibleBy3ButNotBy2 = @"^0*1(01*0)*1(0*1(01*0)*1)*$";
    
    [Fact]
    public void VerifyDivisibleBy3ButNotBy2()
    {
      for(var value = 3; value <= 10_000; value++)
      {
        if (value.isDivisibleBy(3) && !value.isDivisibleBy(2)) binaryDivisibleBy3ButNotBy2.AssertBinaryIsMatch(value);
        else binaryDivisibleBy3ButNotBy2.AssertBinaryNoMatch(value);
      }
    }
    
    /*
      problem 9
      x mod 3 != 0
      makes 1 and 2 the ending state
      r' = (2r + b) mod 3
        b 0 1
      r -----
      0 | 0 1
      1 | 2 0
      2 | 1 2
    */
    const string binaryNotDivisibleBy3 = @"^0*1((10*1)|(01*0))*(01*)?$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyNotDivisibleBy3()
    {
      for (var value = 0; value <= 10_000; value++)
      {
        if (!value.isDivisibleBy(3)) binaryNotDivisibleBy3.AssertBinaryIsMatch(value);
        else binaryNotDivisibleBy3.AssertBinaryNoMatch(value);
      }
    }
    
    /*
      problem 10
      r' = (2r + b) mod 5
        b 0 1
      r -----
      0 | 0 1
      1 | 2 3
      2 | 4 0
      3 | 1 2
      4 | 3 4
    */
    const string binaryDivisibleBy5 = @"^(0|(1((1|(001*0))(101*0)*0)*)(01|((1|(001*0))(101*0)*11)))*$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy5()
      => binaryDivisibleBy5.VerifyBinaryIsDivisibleBy(5);
      
    /*
      problem 11
      x mod 5 = 0 if the last digit is 0 or 5
    */
    const string decimalDivisibleBy5 = @"^\d*[05]$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDecimalDivisibleBy5()
      => decimalDivisibleBy5.VerifyDecimalIsDivisibleBy(5);
    
    /*
      problem 12
      r' = (2r + b) mod 6
        b 0 1
      r -----
      0 | 0 1
      1 | 2 3
      2 | 4 5
      3 | 0 1
    */
    const string binaryDivisibleBy6 = @"^(0|(1((01*0)|(11))*10))*$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy6()
      => binaryDivisibleBy6.VerifyBinaryIsDivisibleBy(6);
    
    /*
      problem 13
      3*2^n is division by 3 with n zeroes
    */
    
    Func<int, string> binaryDivisibleBy3TimesPowerOf2ToNth =
      n => binaryDivisibleBy3[..^1] + new string('0', n) + "$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy3TimesPowerOf2ToNth()
    {
      for (var n = 0; n <= 10; n++)
      {
        for (var x = 1; x <= 10_000; x++)
        {
          var value = Convert.ToInt32(x * Math.Pow(2, n));
          if (value.isDivisibleBy(3)) binaryDivisibleBy3TimesPowerOf2ToNth(n).AssertBinaryIsMatch(value);
          else binaryDivisibleBy3TimesPowerOf2ToNth(n).AssertBinaryNoMatch(value);
        }
      }
    }
    
    /*
      problem 14
      r' = (2r + b) mod 7
        b 0 1
      r -----
      0 | 0 1
      1 | 2 3
      2 | 4 5
      3 | 6 0
      4 | 1 2
      5 | 3 4
      6 | 5 6
    */
    const string binaryDivisibleBy7 =
      @"^(0|(1(((00(10)*0))|(((01)|((00(10)*11)|(101*0)))((1(10)*11)|(001*0))*(1(10)*0)))*((((01)|((00(10)*11)|(101*0)))((1(10)*11)|(001*0))*01)|(11))))*$";
    
    [Fact]
    public void UseValuesOneToTenThousandToVerifyDivisibleBy7()
      => binaryDivisibleBy7.VerifyBinaryIsDivisibleBy(7);
}

public static class UtilityExtensions
{
    public static bool isDivisibleBy(this int value, int divisor) => value % divisor == 0;
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
    public static void VerifyBinaryIsDivisibleBy(this string regex, double divisor)
      => VerifyIsDivisibleBy(
        regex, divisor, (r, d) => r.AssertBinaryIsMatch(d), (r, d) => r.AssertBinaryNoMatch(d));
    public static void VerifyDecimalIsDivisibleBy(this string regex, double divisor)
      => VerifyIsDivisibleBy(
        regex, divisor, (r, d) => r.AssertDecimalIsMatch(d), (r, d) => r.AssertDecimalNoMatch(d));
    public static void VerifyIsDivisibleBy(
      this string regex, double divisor, Action<string, int> pass, Action<string, int> fail)
    {
      for(var value = 1; value <= 10_000; value++)
      {
        // note divisor needs to be a double so we cannot use isDivisibleBy
        if (value % divisor == 0) pass(regex, value);
        else fail(regex, value);
      }
    }
}