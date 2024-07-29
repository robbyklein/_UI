using System;
using NUnit.Framework;
using UnityEngine;

public class OutlineShorthandParserTests {
    [Test]
    public void ValidLengthAndColorInOrder() {
        string input = "2px #FF0000";

        OutlineShorthand result = OutlineShorthandParser.ParseOutline(input);

        Assert.AreEqual(2f, result.Width);
        Assert.AreEqual(new Color(1, 0, 0, 1), result.Color); // #FF0000
    }

    [Test]
    public void ValidColorAndLengthInOrder() {
        string input = "#FF0000 2px";

        OutlineShorthand result = OutlineShorthandParser.ParseOutline(input);

        Assert.AreEqual(2f, result.Width);
        Assert.AreEqual(new Color(1, 0, 0, 1), result.Color); // #FF0000
    }

    [Test]
    public void OnlyLength() {
        string input = "5px";

        OutlineShorthand result = OutlineShorthandParser.ParseOutline(input);

        Assert.AreEqual(5f, result.Width);
        Assert.IsNull(result.Color);
    }

    [Test]
    public void OnlyColor() {
        string input = "#00FF00";

        OutlineShorthand result = OutlineShorthandParser.ParseOutline(input);

        Assert.IsNull(result.Width);
        Assert.AreEqual(new Color(0, 1, 0, 1), result.Color); // #00FF00
    }

    [Test]
    public void EmptyString() {
        string input = "";

        Assert.Throws<ArgumentException>(() => OutlineShorthandParser.ParseOutline(input));
    }

    [Test]
    public void InvalidPart() {
        string input = "invalid 2px";

        Assert.Throws<ArgumentException>(() => OutlineShorthandParser.ParseOutline(input));
    }
}