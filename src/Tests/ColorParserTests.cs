using System;
using NUnit.Framework;
using UnityEngine;

public class ColorParserTests {
    [TestCase("red", 1f, 0f, 0f, 1f, TestName = "NamedColor")]
    [TestCase("#ff0000", 1f, 0f, 0f, 1f, TestName = "HexColor")]
    [TestCase("rgb(255, 0, 0)", 1f, 0f, 0f, 1f, TestName = "RGBColor")]
    [TestCase("rgba(255, 0, 0, 0.5)", 1f, 0f, 0f, 0.5f, TestName = "RGBAColor")]
    [TestCase("hsl(0, 100%, 50%)", 1f, 0f, 0f, 1f, TestName = "HSLColor")]
    public void ValidInputs(string input, float expectedR, float expectedG, float expectedB,
        float expectedA) {
        Color result = ColorParser.ColorStringToColor(input);
        Assert.AreEqual(expectedR, result.r, 0.01f);
        Assert.AreEqual(expectedG, result.g, 0.01f);
        Assert.AreEqual(expectedB, result.b, 0.01f);
        Assert.AreEqual(expectedA, result.a, 0.01f);
    }

    [TestCase(null, TestName = "NullColorString")]
    [TestCase("", TestName = "EmptyColorString")]
    [TestCase("invalid", TestName = "InvalidColorString")]
    public void InvalidInputs(string input) {
        Assert.Throws<ArgumentException>(() => ColorParser.ColorStringToColor(input));
    }
}