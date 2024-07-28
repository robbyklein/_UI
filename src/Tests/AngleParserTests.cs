using System;
using NUnit.Framework;
using UnityEngine.UIElements;

public class AngleParserTests {
    [TestCase("45deg", 45f, TestName = "AngleStringToRotate_ValidDegrees")]
    [TestCase("200grad", 180f, TestName = "AngleStringToRotate_ValidGradians")]
    [TestCase("1.57rad", (float)(1.57 * (180 / Math.PI)),
        TestName = "AngleStringToRotate_ValidRadians")] // Convert radians to degrees
    [TestCase("0.5turn", 180f, TestName = "AngleStringToRotate_ValidTurns")]
    [TestCase(" 90deg ", 90f, TestName = "AngleStringToRotate_ValidDegreesWithSpaces")]
    public void TestAngleStringToRotate_Valid(string input, float expectedDegrees) {
        Rotate result = AngleParser.AngleStringToRotate(input);
        Assert.NotNull(result);

        float actualDegrees = result.angle.ToDegrees();
        Assert.AreEqual(expectedDegrees, actualDegrees, 0.01f);
    }

    [TestCase("invalid", TestName = "AngleStringToRotate_InvalidFormat")]
    [TestCase("123", TestName = "AngleStringToRotate_NoUnit")]
    [TestCase("45degree", TestName = "AngleStringToRotate_InvalidUnit")]
    public void TestAngleStringToRotate_Invalid(string input) {
        Assert.Throws<FormatException>(() => AngleParser.AngleStringToRotate(input));
    }
}