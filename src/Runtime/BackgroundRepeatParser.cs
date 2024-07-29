using System;
using UnityEngine;
using UnityEngine.UIElements;

internal static class BackgroundRepeatParser {
    internal static BackgroundRepeat ParseBackgroundRepeat(string value) {
        value = value.Trim().ToLower();
        string[] parts = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        Repeat xRepeat = Repeat.Repeat; // Default value
        Repeat yRepeat = Repeat.Repeat; // Default value

        if (parts.Length == 1) {
            switch (parts[0]) {
                case "repeat":
                    xRepeat = Repeat.Repeat;
                    yRepeat = Repeat.Repeat;
                    break;
                case "repeat-x":
                    xRepeat = Repeat.Repeat;
                    yRepeat = Repeat.NoRepeat;
                    break;
                case "repeat-y":
                    xRepeat = Repeat.NoRepeat;
                    yRepeat = Repeat.Repeat;
                    break;
                case "no-repeat":
                    xRepeat = Repeat.NoRepeat;
                    yRepeat = Repeat.NoRepeat;
                    break;
                default:
                    throw new ArgumentException($"Invalid background-repeat value: {parts[0]}");
            }
        } else if (parts.Length == 2) {
            xRepeat = ParseRepeat(parts[0]);
            yRepeat = ParseRepeat(parts[1]);
        } else {
            throw new ArgumentException($"Invalid background-repeat value: {value}");
        }

        return new BackgroundRepeat(xRepeat, yRepeat);
    }

    private static Repeat ParseRepeat(string value) {
        return value switch {
            "repeat" => Repeat.Repeat,
            "no-repeat" => Repeat.NoRepeat,
            _ => throw new ArgumentException($"Invalid repeat value: {value}")
        };
    }
}