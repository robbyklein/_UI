using NUnit.Framework;
using UnityEngine.UIElements;

[TestFixture]
public class TranslateParserTests {
    [Test]
    public void SingleValuePercent() {
        string input = "50%";
        Translate expected = new(Length.Percent(50), Length.Percent(50));

        Translate result = TranslateParser.ParseTranslate(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void SingleValuePixels() {
        string input = "30px";
        Translate expected = new(new Length(30, LengthUnit.Pixel), new Length(30, LengthUnit.Pixel));

        Translate result = TranslateParser.ParseTranslate(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TwoValuesPercent() {
        string input = "20% 80%";
        Translate expected = new(Length.Percent(20), Length.Percent(80));

        Translate result = TranslateParser.ParseTranslate(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TwoValuesPixels() {
        string input = "15px 25px";
        Translate expected = new(new Length(15, LengthUnit.Pixel), new Length(25, LengthUnit.Pixel));

        Translate result = TranslateParser.ParseTranslate(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void MixedValues() {
        string input = "10% 40px";
        Translate expected = new(Length.Percent(10), new Length(40, LengthUnit.Pixel));

        Translate result = TranslateParser.ParseTranslate(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void InvalidKeyword() {
        string input = "invalid";

        Assert.Throws<System.ArgumentException>(() => TranslateParser.ParseTranslate(input));
    }

    [Test]
    public void InvalidSyntax() {
        string input = "10px 20px 30px"; // Invalid because of three values

        Assert.Throws<System.ArgumentException>(() => TranslateParser.ParseTranslate(input));
    }
}