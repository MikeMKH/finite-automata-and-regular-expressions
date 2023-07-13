using System.Text.RegularExpressions;

namespace Rest.Tests;

public class RestTests
{
    /*
      problem 70
      string of values in [1,6] such that 1 is the first value and rest are either equal to or less than the prior 
    */
    const string string1to6LessThanOrEqualPrevious = @"^11*2[12]*3[1-3]*4[1-4]*5[1-5]*6$";
    
    [Theory]
    [InlineData("123456", true)]
    [InlineData("11223344556", true)]
    [InlineData("1213214321543216", true)]
    [InlineData("1234543216", true)]
    [InlineData("1223334444555556", true)]
    [InlineData("1", false)]
    [InlineData("6", false)]
    [InlineData("16", false)]
    [InlineData("23456", false)]
    [InlineData("1356", false)]
    [InlineData("23456", false)]
    [InlineData("13456", false)]
    [InlineData("12456", false)]
    [InlineData("12356", false)]
    [InlineData("12346", false)]
    [InlineData("12345", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void string1to6LessThanOrEqualPrevious_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, string1to6LessThanOrEqualPrevious));

    /*
      problem 72
      model of betting on coin flip, heads you win $1, two people with $3 each, model what a game could look like
    */
    const string coinFlipLoss = @"((T(TH)*H)|(H(HT)*T))*T(TH)*TT";
    const string coinFlipWin  = @"((T(TH)*H)|(H(HT)*T))*H(HT)*HH";
    const string coinFlipOutcomes = $"^({coinFlipLoss})|({coinFlipWin})$";
    
    [Theory]
    [InlineData("TTT", true)]
    [InlineData("TTHTT", true)]
    [InlineData("THTTHTT", true)]
    [InlineData("HTTHTTHTT", true)]
    [InlineData("HTTHTTHTTHTHTHTHTHT", true)]
    [InlineData("HHH", true)]
    [InlineData("HTHHH", true)]
    [InlineData("HTHTHHH", true)]
    [InlineData("HTHTHTHHH", true)]
    [InlineData("HTHTHTHHTHH", true)]
    [InlineData("HTTTHHHTHTHHTHH", true)]
    // [InlineData("HHHTTTHHTTHTTTT", true)] // not support by model
    [InlineData("1", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void coinFlipOutcomes_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, coinFlipOutcomes));

}