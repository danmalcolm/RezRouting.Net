using System;
using FluentAssertions;
using RezRouting.Configuration.Options;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.Tests.Configuration.Options
{
    public class UrlPathSettingsTests
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
            string result = settings.FormatDirectoryName(name);
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
            string path = settings.FormatDirectoryName("PurchaseOrders&*^*&");
            path.Should().Be("purchaseorders");
        }
 
    }
}