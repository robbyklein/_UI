using System;
using NUnit.Framework;
using UnityEngine.UIElements;

[TestFixture]
public class FlexShorthandParserTests {
    [Test]
    public void KeywordAutoReturnsCorrectFlexProperties() {
        // Arrange
        string input = "auto";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(StyleKeyword.Auto, result.Keyword);
        Assert.IsNull(result.FlexGrow);
        Assert.IsNull(result.FlexShrink);
        Assert.IsNull(result.FlexBasis);
    }

    [Test]
    public void KeywordInitialReturnsCorrectFlexProperties() {
        // Arrange
        string input = "initial";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(StyleKeyword.Initial, result.Keyword);
        Assert.IsNull(result.FlexGrow);
        Assert.IsNull(result.FlexShrink);
        Assert.IsNull(result.FlexBasis);
    }

    [Test]
    public void KeywordNoneReturnsCorrectFlexProperties() {
        // Arrange
        string input = "none";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(StyleKeyword.None, result.Keyword);
        Assert.IsNull(result.FlexGrow);
        Assert.IsNull(result.FlexShrink);
        Assert.IsNull(result.FlexBasis);
    }

    [Test]
    public void UnitlessNumberReturnsCorrectFlexGrow() {
        // Arrange
        string input = "2";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(2, result.FlexGrow);
        Assert.AreEqual(1, result.FlexShrink); // Default
        Assert.AreEqual(new Length(0, LengthUnit.Pixel), result.FlexBasis); // Default
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void LengthValueReturnsCorrectFlexBasis() {
        // Arrange
        string input = "10%";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(0, result.FlexGrow); // Default when basis is specified
        Assert.AreEqual(1, result.FlexShrink); // Default
        Assert.AreEqual(new Length(10, LengthUnit.Percent), result.FlexBasis); // Assuming conversion
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void PercentageValueReturnsCorrectFlexBasis() {
        // Arrange
        string input = "30%";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(0, result.FlexGrow); // Default when basis is specified
        Assert.AreEqual(1, result.FlexShrink); // Default
        Assert.AreEqual(new Length(30, LengthUnit.Percent), result.FlexBasis);
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void MinContentReturnsCorrectFlexBasis() {
        // Arrange
        string input = "min-content";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(0, result.FlexGrow); // Default when basis is specified
        Assert.AreEqual(1, result.FlexShrink); // Default
        Assert.AreEqual(new Length(0, LengthUnit.Percent), result.FlexBasis); // Assuming conversion
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void FlexGrowAndBasisReturnsCorrectProperties() {
        // Arrange
        string input = "1 30px";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(1, result.FlexGrow);
        Assert.AreEqual(1, result.FlexShrink); // Default
        Assert.AreEqual(new Length(30, LengthUnit.Pixel), result.FlexBasis);
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void FlexGrowAndShrinkReturnsCorrectProperties() {
        // Arrange
        string input = "2 2";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(2, result.FlexGrow);
        Assert.AreEqual(2, result.FlexShrink);
        Assert.AreEqual(new Length(0, LengthUnit.Pixel), result.FlexBasis); // Default
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void FlexGrowShrinkBasisReturnsCorrectProperties() {
        // Arrange
        string input = "2 2 10%";

        // Act
        FlexShorthand result = FlexShorthandParser.ParseFlex(input);

        // Assert
        Assert.AreEqual(2, result.FlexGrow);
        Assert.AreEqual(2, result.FlexShrink);
        Assert.AreEqual(new Length(10, LengthUnit.Percent), result.FlexBasis);
        Assert.IsNull(result.Keyword);
    }

    [Test]
    public void InvalidStringThrowsArgumentException() {
        // Arrange
        string input = "invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => FlexShorthandParser.ParseFlex(input));
    }
}