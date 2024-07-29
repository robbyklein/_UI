using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleParserTests {
    [Test]
    public void UniformScale() {
        string input = "2";
        Vector3 expected = new(2, 2, 1);

        Scale result = ScaleParser.ScaleStringToScale(input);

        Assert.AreEqual(expected, result.value, "Uniform scale should be correctly parsed.");
    }

    [Test]
    public void Percentage() {
        string input = "200%";
        Vector3 expected = new(2, 2, 1);

        Scale result = ScaleParser.ScaleStringToScale(input);

        Assert.AreEqual(expected, result.value, "Uniform percentage scale should be correctly parsed.");
    }

    [Test]
    public void NonUniformScale() {
        string input = "2 3";
        Vector3 expected = new(2, 3, 1);

        Scale result = ScaleParser.ScaleStringToScale(input);

        Assert.AreEqual(expected, result.value, "Non-uniform scale should be correctly parsed.");
    }

    [Test]
    public void NonUniformScalePercentage() {
        string input = "200% 300%";
        Vector3 expected = new(2, 3, 1);

        Scale result = ScaleParser.ScaleStringToScale(input);

        Assert.AreEqual(expected, result.value, "Non-uniform percentage scale should be correctly parsed.");
    }

    [Test]
    public void InvalidFormat() {
        string input = "invalid scale";

        Assert.Throws<FormatException>(() => ScaleParser.ScaleStringToScale(input),
            "Invalid format should throw FormatException.");
    }

    [Test]
    public void EmptyString() {
        string input = "";

        Assert.Throws<FormatException>(() => ScaleParser.ScaleStringToScale(input),
            "Empty string should throw FormatException.");
    }
}