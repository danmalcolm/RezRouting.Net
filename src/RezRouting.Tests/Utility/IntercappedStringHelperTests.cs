using FluentAssertions;
using RezRouting.Utility;
using Xunit.Extensions;

namespace RezRouting.Tests.Utility
{
    public class IntercappedStringHelperTests
    {
        [Theory]
        [InlineData("PurchaseOrders", "Purchase-Orders")]
        [InlineData("purchaseOrders", "purchase-Orders")]
        public void ShouldSeparateWords(string value, string expected)
        {
            string result = IntercappedStringHelper.SeparateWords(value, "-");

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("2Unlimited", "2-Unlimited")]
        [InlineData("The2Unlimited", "The-2-Unlimited")]
        [InlineData("Unlimited2", "Unlimited-2")]
        [InlineData("222Unlimited", "222-Unlimited")]
        [InlineData("The222Unlimited", "The-222-Unlimited")]
        [InlineData("Unlimited222", "Unlimited-222")]
        public void ShouldSeparateNumbers(string value, string expected)
        {
            string result = IntercappedStringHelper.SeparateWords(value, "-");

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("ATeam", "A-Team")]
        [InlineData("TheATeam", "The-A-Team")]
        [InlineData("TeamA", "Team-A")]
        [InlineData("HTMLGuide", "HTML-Guide")]
        [InlineData("TheHTMLGuide", "The-HTML-Guide")]
        [InlineData("TheGuideToHTML", "The-Guide-To-HTML")]
        [InlineData("HTMLGuide5", "HTML-Guide-5")]
        [InlineData("TheHTML5Guide", "The-HTML-5-Guide")]
        [InlineData("TheGuideToHTML5", "The-Guide-To-HTML-5")]
        [InlineData("TheUKAllStars", "The-UK-All-Stars")]
        [InlineData("AllStarsUK", "All-Stars-UK")]
        [InlineData("UKAllStars", "UK-All-Stars")]
        public void ShouldSeparateAcronymsFromWordsOrNumbers(string value, string expected)
        {
            string result = IntercappedStringHelper.SeparateWords(value, "-");

            result.Should().Be(expected);
        } 
    }
}