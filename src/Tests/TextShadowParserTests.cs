using System;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class TextShadowParserTests {
    [Test]
    public void ValidFullDefinition() {
        string input = "1px 1px 2px black";

        TextShadow result = TextShadowParser.ParseTextShadow(input);

        Assert.AreEqual(1f, result.OffsetX);
        Assert.AreEqual(1f, result.OffsetY);
        Assert.AreEqual(2f, result.BlurRadius);
        Assert.AreEqual(Color.black, result.Color);
    }

    [Test]
    public void ColorFirst() {
        string input = "#ff0000 1px 0 10px";

        TextShadow result = TextShadowParser.ParseTextShadow(input);

        Assert.AreEqual(1f, result.OffsetX);
        Assert.AreEqual(0f, result.OffsetY);
        Assert.AreEqual(10f, result.BlurRadius);
        Assert.AreEqual(new Color(1f, 0f, 0f, 1f), result.Color); // #ff0000 as RGB(255, 0, 0)
    }

    [Test]
    public void OffsetAndColor() {
        string input = "5px 5px #558abb";

        TextShadow result = TextShadowParser.ParseTextShadow(input);

        Assert.AreEqual(5f, result.OffsetX);
        Assert.AreEqual(5f, result.OffsetY);
        Assert.AreEqual(0f, result.BlurRadius);
        Assert.AreEqual(new Color(0.33333334f, 0.5411765f, 0.73333335f, 1f), result.Color); // #558abb
    }

    [Test]
    public void ColorAndOffset() {
        string input = "white 2px 5px";

        TextShadow result = TextShadowParser.ParseTextShadow(input);

        Assert.AreEqual(2f, result.OffsetX);
        Assert.AreEqual(5f, result.OffsetY);
        Assert.AreEqual(0f, result.BlurRadius);
        Assert.AreEqual(Color.white, result.Color);
    }

    [Test]
    public void OnlyOffsets() {
        string input = "5px 10px";

        TextShadow result = TextShadowParser.ParseTextShadow(input);

        Assert.AreEqual(5f, result.OffsetX);
        Assert.AreEqual(10f, result.OffsetY);
        Assert.AreEqual(0f, result.BlurRadius);
        Assert.AreEqual(Color.black, result.Color);
    }

    [Test]
    public void InvalidString() {
        string input = "invalid 1px 1px";

        Assert.Throws<ArgumentException>(() => TextShadowParser.ParseTextShadow(input));
    }

    [Test]
    public void EmptyString() {
        string input = "";

        Assert.Throws<ArgumentException>(() => TextShadowParser.ParseTextShadow(input));
    }
}