using System;
using NUnit.Framework;
using UnityEngine.UIElements;

public class LengthParserTests {
    [TestCase("10px", 10f, LengthUnit.Pixel, TestName = "10px")]
    [TestCase("50%", 50f, LengthUnit.Percent, TestName = "50Percent")]
    [TestCase("auto", 0f, 2, TestName = "Auto")]
    public void LengthStringToLengthValid(string input, float expectedValue, LengthUnit expectedUnit) {
        Length result = LengthParser.LengthStringToLength(input);

        if (input == "auto") {
            Assert.IsTrue(result == Length.Auto());
        } else {
            Assert.AreEqual(expectedValue, result.value);
            Assert.AreEqual(expectedUnit, result.unit);
        }
    }

    [TestCase(null, TestName = "Null")]
    [TestCase("", TestName = "Empty")]
    [TestCase("invalid", TestName = "Invalid")]
    public void LengthStringToLengthInvalid(string input) {
        Assert.Throws<ArgumentException>(() => LengthParser.LengthStringToLength(input));
    }

    [TestCase("10px", 10f, LengthUnit.Pixel, TestName = "10px")]
    [TestCase("50%", 50f, LengthUnit.Percent, TestName = "50Percent")]
    [TestCase("auto", 0f, StyleKeyword.Auto, TestName = "Auto")]
    public void LengthStringToStyleLength(string input, float expectedValue, object expectedUnit) {
        StyleLength result = LengthParser.LengthStringToStyleLength(input);

        if (expectedUnit is StyleKeyword keyword) {
            Assert.AreEqual(keyword, result.keyword);
        } else {
            Assert.AreEqual(expectedValue, result.value.value);
            Assert.AreEqual(expectedUnit, result.value.unit);
        }
    }


    [TestCase("initial", StyleKeyword.Initial, TestName = "Initial")]
    [TestCase("none", StyleKeyword.None, TestName = "None")]
    public void LengthStringToStyleLengthKeywords(string input, StyleKeyword expectedKeyword) {
        StyleLength result = LengthParser.LengthStringToStyleLength(input);
        Assert.AreEqual(expectedKeyword, result.keyword);
    }

    [TestCase("10px", 10f, TestName = "10px")]
    public void LengthStringToStyleFloat(string input, float expectedValue) {
        StyleFloat result = LengthParser.LengthStringToStyleFloat(input);
        Assert.AreEqual(expectedValue, result.value);
    }

    [TestCase("initial", StyleKeyword.Initial, TestName = "Initial")]
    [TestCase("none", StyleKeyword.None, TestName = "None")]
    public void LengthStringToStyleFloatKeywords(string input, StyleKeyword expectedKeyword) {
        StyleFloat result = LengthParser.LengthStringToStyleFloat(input);
        Assert.AreEqual(expectedKeyword, result.keyword);
    }

    [TestCase("10px 20px", new[] { 10f, 20f }, new[] { LengthUnit.Pixel, LengthUnit.Pixel }, TestName = "10px_20px")]
    [TestCase("50% auto", new[] { 50f, 0f }, new[] { LengthUnit.Percent, (LengthUnit)(-1) },
        TestName = "50Percent_Auto")]
    public void LengthStringsToLengths(string input, float[] expectedValues, LengthUnit[] expectedUnits) {
        Length[] results = LengthParser.LengthStringsToLengths(input);
        Assert.AreEqual(expectedValues.Length, results.Length);

        for (int i = 0; i < expectedValues.Length; i++) {
            if (expectedUnits[i] == (LengthUnit)(-1)) {
                Assert.IsTrue(results[i] == Length.Auto());
            } else {
                Assert.AreEqual(expectedValues[i], results[i].value);
                Assert.AreEqual(expectedUnits[i], results[i].unit);
            }
        }
    }

    [TestCase("10px 20px", new[] { 10f, 20f }, TestName = "10px_20px")]
    public void LengthStringsToStyleFloats(string input, float[] expectedValues) {
        StyleFloat[] results = LengthParser.LengthStringsToStyleFloats(input);
        Assert.AreEqual(expectedValues.Length, results.Length);

        //yup
        for (int i = 0; i < expectedValues.Length; i++) {
            Assert.AreEqual(expectedValues[i], results[i].value);
        }
    }
}