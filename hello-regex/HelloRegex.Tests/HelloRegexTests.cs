using System.Text.RegularExpressions;
using Xunit;

namespace HelloRegex.Tests
{
  public class HelloRegexTests
  {
    [Theory]
    [InlineData("Hello Mike")]
    [InlineData("HELLO LILY")]
    [InlineData("hello kelsey")]
    [InlineData("hello 123456789")]
    public void MatchesHello(string given) => Assert.True(Regex.IsMatch(given, @"[hH][eE][lL]{2}[oO] \w*"));
  }
}
