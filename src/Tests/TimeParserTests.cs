using NUnit.Framework;
using System;

[TestFixture]
public class TimeParserTests {
    [Test]
    public void ValidSeconds() {
        string input = "2s";
        float expected = 2f;

        float result = TimeParser.ParseTime(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ValidMilliseconds() {
        string input = "500ms";
        float expected = 0.5f;

        float result = TimeParser.ParseTime(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ValidMillisecondsInteger() {
        string input = "1500ms";
        float expected = 1.5f;

        float result = TimeParser.ParseTime(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void InvalidFormat() {
        string input = "2x";

        ArgumentException ex = Assert.Throws<ArgumentException>(() => TimeParser.ParseTime(input));
        Assert.That(ex.Message, Does.StartWith("Invalid time format"));
    }

    [Test]
    public void EmptyString() {
        string input = "";

        ArgumentException ex = Assert.Throws<ArgumentException>(() => TimeParser.ParseTime(input));
        Assert.That(ex.Message, Is.EqualTo("Time string cannot be null or empty."));
    }

    [Test]
    public void NullString() {
        string input = null;

        ArgumentException ex = Assert.Throws<ArgumentException>(() => TimeParser.ParseTime(input));
        Assert.That(ex.Message, Is.EqualTo("Time string cannot be null or empty."));
    }

    [Test]
    public void ZeroMilliseconds() {
        string input = "0ms";
        float expected = 0f;

        float result = TimeParser.ParseTime(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ZeroSeconds() {
        string input = "0s";
        float expected = 0f;

        float result = TimeParser.ParseTime(input);

        Assert.AreEqual(expected, result);
    }
}