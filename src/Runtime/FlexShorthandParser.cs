using System;
using UnityEngine.UIElements;

internal struct FlexShorthand {
    public float? FlexGrow { get; }
    public float? FlexShrink { get; }
    public Length? FlexBasis { get; }
    public StyleKeyword? Keyword { get; }

    public FlexShorthand(float? flexGrow, float? flexShrink, Length? flexBasis, StyleKeyword? keyword) {
        FlexGrow = flexGrow;
        FlexShrink = flexShrink;
        FlexBasis = flexBasis;
        Keyword = keyword;
    }
}

internal static class FlexShorthandParser {
    internal static FlexShorthand ParseFlex(string flexString) {
        string[] parts = flexString.Trim().ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Handle StyleKeyword values
        if (parts.Length == 1) {
            StyleKeyword? keyword = GetStyleKeyword(parts[0]);
            if (keyword.HasValue) {
                return new FlexShorthand(null, null, null, keyword);
            }
        }

        // Default values
        float flexGrow = 1;
        float flexShrink = 1;
        Length flexBasis = new(0, LengthUnit.Pixel);

        if (parts.Length == 1) {
            // Handle single value cases
            if (TryParseLength(parts[0], out flexBasis)) {
                flexGrow = 0; // Default flex-grow to 0 when flex-basis is specified
            } else if (float.TryParse(parts[0], out flexGrow)) {
                flexBasis = new Length(0, LengthUnit.Pixel);
            } else if (IsFlexBasisKeyword(parts[0], out flexBasis)) {
                flexGrow = 0;
            } else {
                throw new ArgumentException($"Invalid flex value: {flexString}");
            }
        } else if (parts.Length == 2) {
            if (float.TryParse(parts[0], out flexGrow)) {
                if (float.TryParse(parts[1], out flexShrink)) {
                    flexBasis = new Length(0, LengthUnit.Pixel);
                } else if (TryParseLength(parts[1], out flexBasis) || IsFlexBasisKeyword(parts[1], out flexBasis)) {
                    flexShrink = 1; // Default flex-shrink to 1
                } else {
                    throw new ArgumentException($"Invalid flex value: {flexString}");
                }
            } else {
                throw new ArgumentException($"Invalid flex value: {flexString}");
            }
        } else if (parts.Length == 3) {
            if (float.TryParse(parts[0], out flexGrow) &&
                float.TryParse(parts[1], out flexShrink) &&
                (TryParseLength(parts[2], out flexBasis) || IsFlexBasisKeyword(parts[2], out flexBasis))) {
                // All three values are valid
            } else {
                throw new ArgumentException($"Invalid flex value: {flexString}");
            }
        } else {
            throw new ArgumentException($"Invalid flex value: {flexString}");
        }

        return new FlexShorthand(flexGrow, flexShrink, flexBasis, null);
    }

    private static StyleKeyword? GetStyleKeyword(string input) {
        return input switch {
            "auto" => StyleKeyword.Auto,
            "initial" => StyleKeyword.Initial,
            "none" => StyleKeyword.None,
            _ => null
        };
    }

    private static bool TryParseLength(string input, out Length length) {
        try {
            length = LengthParser.LengthStringToLength(input);
            return true;
        } catch (ArgumentException) {
            length = default;
            return false;
        }
    }

    private static bool IsFlexBasisKeyword(string input, out Length length) {
        length = default;

        if (input == "auto") {
            length = Length.Auto();
            return true;
        } else if (input == "min-content") {
            length = new Length(0, LengthUnit.Percent); // Adjust as necessary for Unity
            return true;
        }

        return false;
    }
}