using System;
using NUnit.Framework;
using UnityEngine.UIElements;

[TestFixture]
public class BackgroundParserTests {
    [Test]
    public void Repeat() {
        string input = "repeat";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.Repeat, UnityEngine.UIElements.Repeat.Repeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void RepeatX() {
        string input = "repeat-x";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.Repeat, UnityEngine.UIElements.Repeat.NoRepeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void RepeatY() {
        string input = "repeat-y";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.NoRepeat, UnityEngine.UIElements.Repeat.Repeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void NoRepeat() {
        string input = "no-repeat";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.NoRepeat, UnityEngine.UIElements.Repeat.NoRepeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void SeparateValues() {
        string input = "repeat no-repeat";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.Repeat, UnityEngine.UIElements.Repeat.NoRepeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void SeparateValues_Reversed() {
        string input = "no-repeat repeat";
        BackgroundRepeat expected = new(UnityEngine.UIElements.Repeat.NoRepeat, UnityEngine.UIElements.Repeat.Repeat);

        BackgroundRepeat result = BackgroundRepeatParser.ParseBackgroundRepeat(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void InvalidValue() {
        string input = "invalid";
        Assert.Throws<ArgumentException>(() => BackgroundRepeatParser.ParseBackgroundRepeat(input));
    }
}