using System;
using FluentAssertions;
using RezRouting2.Options;
using Xunit;
using Xunit.Extensions;

namespace RezRouting2.Tests.Options
{
    public class UrlPathFormatterTests
    {
        [Theory]
        [InlineData("PurchaseOrders", CaseStyle.None, "", "PurchaseOrders")]
        [InlineData("PurchaseOrders", CaseStyle.Upper, "", "PURCHASEORDERS")]
        [InlineData("PurchaseOrders", CaseStyle.Lower, "", "purchaseorders")]
        [InlineData("PurchaseOrders", CaseStyle.None, "-", "Purchase-Orders")]
        [InlineData("PurchaseOrders", CaseStyle.Upper, "-", "PURCHASE-ORDERS")]
        [InlineData("PurchaseOrders", CaseStyle.Lower, "-", "purchase-orders")]
        public void ShouldFormatCollectionPathBasedOnSettings(string name, CaseStyle caseStyle, string separator, string expected)
        {
            var settings = new UrlPathSettings(caseStyle, separator);
            var formatter = new UrlPathFormatter(settings);
            string result = formatter.FormatDirectoryName(name);
            result.Should().Be(expected);
        }

        [Fact]
        public void ShouldThrowIfInitialisingSettingsWithInvalidSeparator()
        {
            Action act = () => new UrlPathSettings(wordSeparator: "/");
            act.ShouldThrow<ArgumentException>()
                .And.ParamName.Should().Be("wordSeparator");
        }

        [Fact]
        public void ShouldRemoveInvalidCharacters()
        {
            var settings = new UrlPathSettings();
            var formatter = new UrlPathFormatter(settings);
            string path = formatter.FormatDirectoryName("PurchaseOrders&*^*&");
            path.Should().Be("purchaseorders");
        }
 
    }
}