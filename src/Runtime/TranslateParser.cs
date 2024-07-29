using UnityEngine.UIElements;

internal static class TranslateParser {
    internal static Translate ParseTranslate(string value) {
        Length x = new(0, LengthUnit.Pixel);
        Length y = new(0, LengthUnit.Pixel);

        string[] components = value.Trim().ToLower().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (components.Length == 1) {
            if (TryParseLength(components[0], out x)) {
                y = x;
            } else {
                throw new System.ArgumentException($"Invalid translate value: {components[0]}");
            }
        } else if (components.Length == 2) {
            if (!TryParseLength(components[0], out x) ||
                !TryParseLength(components[1], out y)) {
                throw new System.ArgumentException($"Invalid translate values: {string.Join(" ", components)}");
            }
        } else {
            throw new System.ArgumentException($"Invalid translate syntax: {value}");
        }

        return new Translate(x, y);
    }


    private static bool TryParseLength(string component, out Length length) {
        length = default;

        try {
            length = LengthParser.LengthStringToLength(component);
            return true;
        } catch (System.ArgumentException) {
            return false;
        }
    }
}