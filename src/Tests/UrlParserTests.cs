using System;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UrlParserTests {
    [Test]
    public void ExtractUrlOrResourceUrl() {
        // Arrange
        string input = @"url(""https://example.com/image.png"")";
        string expected = "https://example.com/image.png";

        // Act
        string result = UrlParser.ExtractUrlOrResource(input);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ExtractUrlOrResourceResource() {
        // Arrange
        string input = @"resource('example_resource')";
        string expected = "example_resource";

        // Act
        string result = UrlParser.ExtractUrlOrResource(input);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void ConvertToRelativePath() {
        // Arrange
        string input = "project://database/Assets/Textures/image.png";
        string expected = "Textures/image";

        // Act
        string result = UrlParser.ConvertToRelativePath(input);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void PathToTexture2d() {
        // Arrange
        string input = "image2";

        // Act
        Texture2D result = UrlParser.PathToTexture2d(input);

        // Assert
        Assert.IsNotNull(result, "Texture should be loaded successfully.");
    }

    [Test]
    public void LoadResourceTexture() {
        // Arrange
        string input = "image2";

        // Act
        Texture2D result = UrlParser.LoadResourceTexture(input);

        // Assert
        Assert.IsNotNull(result, "Resource texture should be loaded successfully.");
    }

    [Test]
    public void UrlStringToTexture2dResource() {
        // Arrange
        string input = @"resource('image2')";

        // Act
        Texture2D result = UrlParser.UrlStringToTexture2d(input);

        // Assert
        Assert.IsNotNull(result, "Texture should be loaded successfully from resource.");
    }

    [Test]
    public void UrlStringToTexture2dInvalid() {
        // Arrange
        string input = "invalid_string";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => UrlParser.UrlStringToTexture2d(input),
            "Invalid url value should throw ArgumentException.");
    }
}