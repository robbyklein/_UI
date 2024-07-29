using UnityEngine;
using UnityEngine.UIElements;

internal static class TransformOriginParser {
    internal static TransformOrigin ParseTransformOrigin(string value) {
        Length x = Length.Percent(50);
        Length y = Length.Percent(50);

        string[] components = value.Trim().ToLower().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (components.Length == 1) {
            if (TryParseLengthOrKeyword(components[0], out x)) {
                y = x;
            } else {
                throw new System.ArgumentException($"Invalid transform-origin value: {components[0]}");
            }
        } else if (components.Length == 2) {
            if (!TryParseLengthOrKeyword(components[0], out x) ||
                !TryParseLengthOrKeyword(components[1], out y)) {
                throw new System.ArgumentException($"Invalid transform-origin values: {string.Join(" ", components)}");
            }
        } else {
            throw new System.ArgumentException($"Invalid transform-origin syntax: {value}");
        }

        return new TransformOrigin(x, y);
    }

    private static bool TryParseLengthOrKeyword(string component, out Length length) {
        length = default;

        if (TryParseKeyword(component, out length)) {
            return true;
        }

        try {
            length = LengthParser.LengthStringToLength(component);
            return true;
        } catch (System.ArgumentException) {
            return false;
        }
    }

    private static bool TryParseKeyword(string component, out Length length) {
        length = default;

        switch (component) {
            case "center":
                length = Length.Percent(50);
                return true;
            case "left":
            case "top":
                length = Length.Percent(0);
                return true;
            case "right":
            case "bottom":
                length = Length.Percent(100);
                return true;
        }

        return false;
    }
}