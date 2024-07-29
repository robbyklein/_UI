using System;
using NUnit.Framework;
using UIBuddyTypes;
using UnityEngine.UIElements;

internal class BackgroundPositionParserTests {
    [TestCase("left", BackgroundPositionKeyword.Left, 0f, USSAxis.X, 1, TestName = "Left")]
    [TestCase("center", BackgroundPositionKeyword.Center, 0f, USSAxis.All, 1, TestName = "Center")]
    [TestCase("right", BackgroundPositionKeyword.Right, 0f, USSAxis.X, 1, TestName = "Right")]
    [TestCase("top", BackgroundPositionKeyword.Top, 0f, USSAxis.Y, 1, TestName = "Top")]
    [TestCase("bottom", BackgroundPositionKeyword.Bottom, 0f, USSAxis.Y, 1, TestName = "Bottom")]
    [TestCase("left 50%", BackgroundPositionKeyword.Left, 50f, USSAxis.X, 1, TestName = "LeftWithOffset")]
    [TestCase("top 10px", BackgroundPositionKeyword.Top, 10f, USSAxis.Y, 1, TestName = "TopWithOffset")]
    [TestCase("center right", BackgroundPositionKeyword.Center, 0f, USSAxis.Y, 2, BackgroundPositionKeyword.Right, 0f,
        USSAxis.X, TestName = "CenterRight")]
    [TestCase("left center", BackgroundPositionKeyword.Left, 0f, USSAxis.X, 2, BackgroundPositionKeyword.Center, 0f,
        USSAxis.Y, TestName = "LeftCenter")]
    [TestCase("top 10px right 10px", BackgroundPositionKeyword.Top, 10f, USSAxis.Y, 2, BackgroundPositionKeyword.Right,
        10f, USSAxis.X, TestName = "TopRightWithOffsets")]
    [TestCase("left 10px top 20px", BackgroundPositionKeyword.Left, 10f, USSAxis.X, 2, BackgroundPositionKeyword.Top,
        20f, USSAxis.Y, TestName = "LeftTopWithOffsets")]
    [TestCase("left top 20px", BackgroundPositionKeyword.Left, 0f, USSAxis.X, 2, BackgroundPositionKeyword.Top,
        20f, USSAxis.Y, TestName = "LeftTopWithOffset")]
    [TestCase("right 10px top", BackgroundPositionKeyword.Right, 10f, USSAxis.X, 2, BackgroundPositionKeyword.Top,
        0f, USSAxis.Y, TestName = "RightTopWithOffset")]
    public void ValidInputs(string input, BackgroundPositionKeyword expectedKeyword1, float expectedOffset1,
        USSAxis expectedAxis1, int expectedCount,
        BackgroundPositionKeyword expectedKeyword2 = BackgroundPositionKeyword.Left,
        float expectedOffset2 = 0f, USSAxis expectedAxis2 = USSAxis.X) {
        BackgroundPositionInfo[] result = BackgroundPositionParser.Parse(input);
        Assert.NotNull(result);
        Assert.AreEqual(expectedCount, result.Length);

        if (expectedCount == 1) {
            BackgroundPositionInfo positionInfo = result[0];
            Assert.AreEqual(expectedKeyword1, positionInfo.Keyword);
            Assert.AreEqual(expectedOffset1, positionInfo.Offset.value);
            Assert.AreEqual(expectedAxis1, positionInfo.Axis);
        } else if (expectedCount == 2) {
            // Validate the first position
            BackgroundPositionInfo positionInfo1 = result[0];
            Assert.AreEqual(expectedKeyword1, positionInfo1.Keyword);
            Assert.AreEqual(expectedOffset1, positionInfo1.Offset.value);
            Assert.AreEqual(expectedAxis1, positionInfo1.Axis);

            // Validate the second position
            BackgroundPositionInfo positionInfo2 = result[1];
            Assert.AreEqual(expectedKeyword2, positionInfo2.Keyword);
            Assert.AreEqual(expectedOffset2, positionInfo2.Offset.value);
            Assert.AreEqual(expectedAxis2, positionInfo2.Axis);
        }
    }

    [TestCase("invalid", TestName = "InvalidKeyword")]
    [TestCase("left invalid", TestName = "InvalidOffset")]
    [TestCase("left center right", TestName = "TooManyKeywords")]
    public void InvalidInputs(string input) {
        Assert.Throws<ArgumentException>(() => BackgroundPositionParser.Parse(input));
    }
}