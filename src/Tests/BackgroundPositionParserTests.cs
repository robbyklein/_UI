using System;
using NUnit.Framework;
using UIBuddyTypes;
using UnityEngine.UIElements;

internal class BackgroundPositionParserTests {
    [TestCase("left", BackgroundPositionKeyword.Left, 0f, USSAxis.X, TestName = "Parse_Left")]
    [TestCase("center", BackgroundPositionKeyword.Center, 0f, USSAxis.All, TestName = "Parse_Center")]
    [TestCase("right", BackgroundPositionKeyword.Right, 0f, USSAxis.X, TestName = "Parse_Right")]
    [TestCase("top", BackgroundPositionKeyword.Top, 0f, USSAxis.Y, TestName = "Parse_Top")]
    [TestCase("bottom", BackgroundPositionKeyword.Bottom, 0f, USSAxis.Y, TestName = "Parse_Bottom")]
    [TestCase("left 50%", BackgroundPositionKeyword.Left, 50f, USSAxis.X, TestName = "Parse_LeftWithOffset")]
    [TestCase("top 10px", BackgroundPositionKeyword.Top, 10f, USSAxis.Y, TestName = "Parse_TopWithOffset")]
    public void TestParse_ValidInputs(string input, BackgroundPositionKeyword expectedKeyword, float expectedOffset,
        USSAxis expectedAxis) {
        BackgroundPositionInfo[] result = BackgroundPositionParser.Parse(input);
        Assert.NotNull(result);
        Assert.AreEqual(1, result.Length);

        BackgroundPositionInfo positionInfo = result[0];
        Assert.AreEqual(expectedKeyword, positionInfo.Keyword);
        Assert.AreEqual(expectedOffset, positionInfo.Offset.value);
        Assert.AreEqual(expectedAxis, positionInfo.Axis);
    }

    [TestCase("invalid", TestName = "Parse_InvalidKeyword")]
    [TestCase("left invalid", TestName = "Parse_InvalidOffset")]
    [TestCase("left center right", TestName = "Parse_TooManyKeywords")]
    public void TestParse_InvalidInputs(string input) {
        Assert.Throws<ArgumentException>(() => BackgroundPositionParser.Parse(input));
    }
}