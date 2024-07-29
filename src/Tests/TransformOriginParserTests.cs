using NUnit.Framework;
using UnityEngine.UIElements;

[TestFixture]
public class TransformOriginParserTests {
    [Test]
    public void DefaultCenter() {
        string input = "center";
        TransformOrigin expected = new(Length.Percent(50), Length.Percent(50));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void LeftTop() {
        string input = "left top";
        TransformOrigin expected = new(Length.Percent(0), Length.Percent(0));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void RightBottom() {
        string input = "right bottom";
        TransformOrigin expected = new(Length.Percent(100), Length.Percent(100));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TwoPercentageValues() {
        string input = "0% 100%";
        TransformOrigin expected = new(Length.Percent(0), Length.Percent(100));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TwoPixelValues() {
        string input = "20px 30px";
        TransformOrigin expected = new(new Length(20, LengthUnit.Pixel), new Length(30, LengthUnit.Pixel));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void MixedValues() {
        string input = "10% 50px";
        TransformOrigin expected = new(Length.Percent(10), new Length(50, LengthUnit.Pixel));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void InvalidKeyword() {
        string input = "invalid";

        Assert.Throws<System.ArgumentException>(() => TransformOriginParser.ParseTransformOrigin(input));
    }

    [Test]
    public void InvalidSyntax() {
        string input = "100px 50px 30px";

        Assert.Throws<System.ArgumentException>(() => TransformOriginParser.ParseTransformOrigin(input));
    }

    [Test]
    public void SingleValue() {
        string input = "25%";
        TransformOrigin expected = new(Length.Percent(25), Length.Percent(25));

        TransformOrigin result = TransformOriginParser.ParseTransformOrigin(input);

        Assert.AreEqual(expected, result);
    }
}