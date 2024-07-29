using System;
using UnityEngine;

internal struct TextShadow {
    public float OffsetX { get; }
    public float OffsetY { get; }
    public float BlurRadius { get; }
    public Color Color { get; }

    public TextShadow(float offsetX, float offsetY, float blurRadius, Color color) {
        OffsetX = offsetX;
        OffsetY = offsetY;
        BlurRadius = blurRadius;
        Color = color;
    }
}


internal static class TextShadowParser {
    internal static TextShadow ParseTextShadow(string textShadowString) {
        if (string.IsNullOrEmpty(textShadowString)) {
            throw new ArgumentException("Invalid text-shadow string");
        }

        // Split the string by whitespace
        string[] parts = textShadowString.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        float offsetX = 0f;
        float offsetY = 0f;
        float blurRadius = 0f;
        Color color = Color.black;
        bool offsetXSet = false;
        bool offsetYSet = false;

        foreach (string part in parts) {
            // Try to parse as color
            try {
                color = ColorParser.ColorStringToColor(part);
                continue;
            } catch (ArgumentException) {
            }

            // Try to parse as float value (for offset or blur-radius)
            if (float.TryParse(part.TrimEnd("px".ToCharArray()), out float value)) {
                if (!offsetXSet) {
                    offsetX = value;
                    offsetXSet = true;
                } else if (!offsetYSet) {
                    offsetY = value;
                    offsetYSet = true;
                } else {
                    blurRadius = value;
                }

                continue;
            }

            // If parsing failed, throw an exception
            throw new ArgumentException($"Invalid part in text-shadow string: {part}");
        }

        // At least offset-x and offset-y should be set
        if (!offsetXSet || !offsetYSet) {
            throw new ArgumentException("text-shadow must contain at least offset-x and offset-y.");
        }

        // Return the parsed text shadow
        return new TextShadow(offsetX, offsetY, blurRadius, color);
    }
}