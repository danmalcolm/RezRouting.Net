using System;
using FluentAssertions;
using RezRouting.Configuration;
using Xunit;
using Xunit.Extensions;

namespace RezRouting.Tests.Configuration
{
    public class DefaultResourcePathFormatterTests
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
            var settings = new ResourcePathSettings(caseStyle, separator);
            var formatter = new DefaultResourcePathFormatter(settings);
            string result = formatter.GetResourcePath(name);
            result.Should().Be(expected);
        }

        [Fact]
        public void ShouldThrowIfInitialisingSettingsWithInvalidSeparator()
        {
            Action act = () => new ResourcePathSettings(wordSeparator: "/");
            act.ShouldThrow<ArgumentException>()
                .And.ParamName.Should().Be("wordSeparator");
        }

        [Fact]
        public void ShouldRemoveInvalidCharacters()
        {
            var settings = new ResourcePathSettings();
            var formatter = new DefaultResourcePathFormatter(settings);
            string path = formatter.GetResourcePath("PurchaseOrders&*^*&");
            path.Should().Be("purchaseorders");
        }
    }
}