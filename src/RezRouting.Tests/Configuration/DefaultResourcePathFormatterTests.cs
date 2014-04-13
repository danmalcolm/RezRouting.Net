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
        [InlineData(CaseStyle.None, "", "PurchaseOrders", "PurchaseOrders")]
        [InlineData(CaseStyle.Upper, "", "PurchaseOrders", "PURCHASEORDERS")]
        [InlineData(CaseStyle.Lower, "", "PurchaseOrders", "purchaseorders")]
        [InlineData(CaseStyle.None, "-", "PurchaseOrders", "Purchase-Orders")]
        [InlineData(CaseStyle.Upper, "-", "PurchaseOrders", "PURCHASE-ORDERS")]
        [InlineData(CaseStyle.Lower, "-", "PurchaseOrders", "purchase-orders")]
        public void ShouldFormatBasedOnSettings(CaseStyle caseStyle, string separator, string resourceName, string expected)
        {
            var settings = new ResourcePathSettings(caseStyle, separator);
            var formatter = new DefaultResourcePathFormatter(settings);
            string result = formatter.GetResourcePath(resourceName);
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