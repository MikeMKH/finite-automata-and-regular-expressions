using System.Text.RegularExpressions;

namespace PatternsRegex.Tests;

public class PatternsRegexTests
{
    /*
      problem 16
      binary
    */
    const string binaryLeadingZeroes = @"^[01]*$";
    const string binaryNoLeadingZeroes = @"^0?$|^1[01]*$"; // @"(^0?$)|(^1[01]*$)" IMHO is easier to read
    
    [Theory]
    [InlineData("", true, true)]
    [InlineData("0", true, true)]
    [InlineData("00", true, false)]
    [InlineData("1", true, true)]
    [InlineData("11", true, true)]
    [InlineData("10", true, true)]
    [InlineData("010", true, false)]
    [InlineData("110", true, true)]
    [InlineData("0110", true, false)]
    [InlineData("0100", true, false)]
    [InlineData("hello", false, false)]
    [InlineData("h3110", false, false)]
    [InlineData("4311o", false, false)]
    public void binary_GivenStringMatchesTheExpectedResult(string given, bool leading, bool noLeading)
    {
        Assert.Equal(leading, Regex.IsMatch(given, binaryLeadingZeroes));
        Assert.Equal(noLeading, Regex.IsMatch(given, binaryNoLeadingZeroes));
    }
    
    /*
      problem 17
      base 10 int
    */
    const string base10Integers = @"^0?$|^[1-9][0-9]*$";
    
    [Theory]
    [InlineData("", true)]
    [InlineData("0", true)]
    [InlineData("01", false)]
    [InlineData("1", true)]
    [InlineData("10", true)]
    [InlineData("1234567890", true)]
    [InlineData("8", true)]
    [InlineData("0123456789", false)]
    [InlineData("hello", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    public void base10Integers_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, base10Integers));
    
    /*
      problem 18
      base 10 decimal
    */
    const string base10Decimal = @"^(0?|([1-9][0-9]*))(\.[0-9]+)?$";
    
    [Theory]
    [InlineData("1234567890", true)]
    [InlineData("0", true)]
    [InlineData("1", true)]
    [InlineData("0.0", true)]
    [InlineData(".0", true)]
    [InlineData("0.1234567890", true)]
    [InlineData("0.0987654321", true)]
    [InlineData("1234567890.0987654321", true)]
    [InlineData("123.456789", true)]
    [InlineData(".", false)]
    [InlineData("1.", false)]
    [InlineData("1234567890.", false)]
    [InlineData("0.", false)]
    [InlineData("hello", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void base10Decimal_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, base10Decimal));
      
    /*
      problem 19
      binary number with exactly three 1s
    */
    
    const string binaryExactlyThee1 = @"^(0*10*){3}$";
    
    [Theory]
    [InlineData("111", true)]
    [InlineData("0101010", true)]
    [InlineData("000010001000010000000000", true)]
    [InlineData("0000110001", true)]
    [InlineData("111000", true)]
    [InlineData("01110", true)]
    [InlineData("011101", false)]
    [InlineData("011102", false)]
    [InlineData("111.", false)]
    [InlineData("11.1", false)]
    [InlineData("hello", false)]
    [InlineData("he1110", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binaryExactlyThee1_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binaryExactlyThee1));
      
    /*
      problem 20
      given alphabet {a,b,c,d}
      find where words contain exactly one c and one d
    */
    
    const string abcdExactlyOnecd = @"^([ab]*c[ab]*d[ab]*)|([ab]*d[ab]*c[ab]*)$";
    
    [Theory]
    [InlineData("abcd", true)]
    [InlineData("abdc", true)]
    [InlineData("adcb", true)]
    [InlineData("cd", true)]
    [InlineData("dc", true)]
    [InlineData("adaca", true)]
    [InlineData("bdbcb", true)]
    [InlineData("badbacab", true)]
    [InlineData("badbacabc", false)]
    [InlineData("c", false)]
    [InlineData("d", false)]
    [InlineData("abd", false)]
    [InlineData("abc", false)]
    [InlineData("hello", false)]
    [InlineData("he110", false)]
    [InlineData("heabcd0", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void abcdExactlyOnecd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, abcdExactlyOnecd));
    
    /*
      problem 21
      given alphabet {a,b,c,d}
      find where words contain exactly two c and two d
    */
    
    const string abcdExactlyTwocd =
      @"^([ab]*c[ab]*c[ab]*d[ab]*d[ab]*)|([ab]*d[ab]*d[ab]*c[ab]*c[ab]*)|([ab]*c[ab]*d[ab]*c[ab]*d[ab]*)|([ab]*d[ab]*c[ab]*d[ab]*c[ab]*)$";
    
    [Theory]
    [InlineData("ccdd", true)]
    [InlineData("ddcc", true)]
    [InlineData("cdcd", true)]
    [InlineData("dcdc", true)]
    [InlineData("bacbacbadbadba", true)]
    [InlineData("aaddccaa", true)]
    [InlineData("bcadcdb", true)]
    [InlineData("adacadaca", true)]
    [InlineData("c", false)]
    [InlineData("d", false)]
    [InlineData("abd", false)]
    [InlineData("abcd", false)]
    [InlineData("abccd", false)]
    [InlineData("abcdd", false)]
    [InlineData("abcddd", false)]
    [InlineData("abcccddd", false)]
    [InlineData("hello", false)]
    [InlineData("he110", false)]
    [InlineData("heabcd0", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void abcdExactlyTwocd_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, abcdExactlyTwocd));
    
    /*
      problem 23
      binary numbers with no runs of three or more 1s
    */
    
    const string binaryNoMoreThanTwo1InRun = @"^(1{0,2}0+)*1{0,2}$";

    [InlineData("", true)]
    [InlineData("0", true)]
    [InlineData("00", true)]
    [InlineData("100", true)]
    [InlineData("001", true)]
    [InlineData("1001", true)]
    [InlineData("110011", true)]
    [InlineData("11010110", true)]
    [InlineData("1010101", true)]
    [InlineData("01000000101000001", true)]
    [InlineData("11011011011", true)]
    [InlineData("111", false)]
    [InlineData("1101", false)]
    [InlineData("111111", false)]
    [InlineData("0111", false)]
    [InlineData("01110", false)]
    [InlineData("hello", false)]
    [InlineData("he110", false)]
    [InlineData("heabcd0", false)]
    [InlineData("h3110", false)]
    [InlineData("4311o", false)]
    [InlineData("4311.o", false)]
    public void binaryNoMoreThanTwo1InRun_GivenStringMatchTheExpectedResult(string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, binaryNoMoreThanTwo1InRun));
    
    /*
      problem 24
      no runs of k or more 1s
    */
  
    Func<int, string> makeBinaryNoRunsOfMoreThan = k =>  $"^(1{{0,{k-1}}}0+)*1{{0,{k-1}}}$";  
    
    [Theory]
    [InlineData(2, "1", true)]
    [InlineData(2, "0100", true)]
    [InlineData(1, "0000", true)]
    [InlineData(5, "11110111", true)]
    [InlineData(4, "11110111", false)]
    [InlineData(1, "hello", false)]
    [InlineData(2, "he110", false)]
    [InlineData(3, "heabcd0", false)]
    [InlineData(4, "h3110", false)]
    [InlineData(5, "4311o", false)]
    [InlineData(6, "4311.o", false)]
    public void makeBinaryNoRunsOfMoreThan_GivenKAndStringMatchTheExpectedResult(int k, string given, bool expected)
      => Assert.Equal(expected, Regex.IsMatch(given, makeBinaryNoRunsOfMoreThan(k)));

}