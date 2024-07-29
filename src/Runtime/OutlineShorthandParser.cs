using System;
using UnityEngine;
using UnityEngine.UIElements;

internal struct OutlineShorthand {
    public float? Width { get; }
    public Color? Color { get; }

    public OutlineShorthand(float? width, Color? color) {
        Width = width;
        Color = color;
    }
}

internal static class OutlineShorthandParser {
    internal static OutlineShorthand ParseOutline(string outlineString) {
        // Ensure the input is not null or empty
        if (string.IsNullOrEmpty(outlineString)) {
            throw new ArgumentException("Invalid outline string");
        }

        // Split the string by whitespace
        string[] parts = outlineString.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Initialize nullable fields
        float? width = null;
        Color? color = null;

        foreach (string part in parts) {
            // Try to parse as length and convert to float
            try {
                Length length = LengthParser.LengthStringToLength(part);
                width = length.value;
                continue;
            } catch (ArgumentException) {
            }

            // Try to parse as color
            try {
                color = ColorParser.ColorStringToColor(part);
                continue;
            } catch (ArgumentException) {
            }

            // If neither parsing succeeded, throw an exception
            throw new ArgumentException($"Invalid part in outline string: {part}");
        }

        // At least one of the values should be present
        if (width == null && color == null) {
            throw new ArgumentException("Outline must contain at least a length or color.");
        }

        // Return the parsed outline
        return new OutlineShorthand(width, color);
    }
}