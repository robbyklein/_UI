using System;
using NUnit.Framework;
using UnityEngine.UIElements;

public class AngleParserTests {
    [TestCase("45deg", 45f, TestName = "ValidDegrees")]
    [TestCase("200grad", 180f, TestName = "ValidGradians")]
    [TestCase("1.57rad", (float)(1.57 * (180 / Math.PI)), TestName = "ValidRadians")]
    [TestCase("0.5turn", 180f, TestName = "ValidTurns")]
    [TestCase(" 90deg ", 90f, TestName = "ValidDegreesWithSpaces")]
    public void ValidInputs(string input, float expectedDegrees) {
        Rotate result = AngleParser.AngleStringToRotate(input);
        Assert.NotNull(result);

        float actualDegrees = result.angle.ToDegrees();
        Assert.AreEqual(expectedDegrees, actualDegrees, 0.01f);
    }

    [TestCase("invalid", TestName = "InvalidFormat")]
    [TestCase("123", TestName = "NoUnit")]
    [TestCase("45degree", TestName = "InvalidUnit")]
    public void InvalidInputs(string input) {
        Assert.Throws<FormatException>(() => AngleParser.AngleStringToRotate(input));
    }
}