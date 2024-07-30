using System;
using System.Collections.Generic;
using _UITypes;
using UnityEngine.UIElements;

internal struct BackgroundPositionInfo {
    public BackgroundPositionKeyword Keyword;
    public Length Offset;
    public USSAxis Axis;

    public BackgroundPositionInfo(BackgroundPositionKeyword keyword, Length offset, USSAxis axis) {
        Keyword = keyword;
        Offset = offset;
        Axis = axis;
    }
}

internal static class BackgroundPositionParser {
    public static BackgroundPositionInfo[] Parse(string value) {
        // Split by space
        string[] positions = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Create a list for position infos
        List<BackgroundPositionInfo> backgroundPositions = new();

        // Loop the values
        for (int i = 0; i < positions.Length; i++) {
            // The start of a value must be a keyword
            if (IsKeyword(positions[i], out BackgroundPositionKeyword keyword)) {
                // Now we need to check for an offset
                Length? offset = null;

                if (i + 1 < positions.Length && IsLength(positions[i + 1], out Length length)) {
                    offset = length;
                    i++;
                }

                // We also need to figure out what axist its on
                USSAxis axis = GetAxis(keyword, positions);

                // Finally we can create the position and append to the list
                backgroundPositions.Add(new BackgroundPositionInfo(keyword, offset ?? Length.None(), axis));
            }
            // If it doesnt start with a keyword its invalid
            else {
                throw new ArgumentException($"Invalid background position value: {positions[i]}");
            }
        }

        // Ensure that only one keyword is specified per axis
        if (backgroundPositions.Count > 2) {
            throw new ArgumentException("Too many keywords specified for background position.");
        }

        return backgroundPositions.ToArray();
    }

    private static bool IsKeyword(string value, out BackgroundPositionKeyword keyword) {
        Dictionary<string, BackgroundPositionKeyword> valueMap = new() {
            { "left", BackgroundPositionKeyword.Left },
            { "center", BackgroundPositionKeyword.Center },
            { "middle", BackgroundPositionKeyword.Center },
            { "right", BackgroundPositionKeyword.Right },
            { "top", BackgroundPositionKeyword.Top },
            { "bottom", BackgroundPositionKeyword.Bottom }
        };

        return valueMap.TryGetValue(value.ToLower().Trim(), out keyword);
    }

    private static bool IsLength(string value, out Length length) {
        try {
            length = LengthParser.LengthStringToLength(value);
            return true;
        } catch {
            length = default;
            return false;
        }
    }

    private static USSAxis GetAxis(BackgroundPositionKeyword keyword, string[] positions) {
        // Check if there is another non-center keyword
        foreach (string position in positions) {
            if (IsKeyword(position, out BackgroundPositionKeyword otherKeyword) &&
                otherKeyword != BackgroundPositionKeyword.Center) {
                return keyword switch {
                    BackgroundPositionKeyword.Left => USSAxis.X,
                    BackgroundPositionKeyword.Center => otherKeyword == BackgroundPositionKeyword.Top ||
                                                        otherKeyword == BackgroundPositionKeyword.Bottom
                        ? USSAxis.X
                        : USSAxis.Y,
                    BackgroundPositionKeyword.Right => USSAxis.X,
                    BackgroundPositionKeyword.Top => USSAxis.Y,
                    BackgroundPositionKeyword.Bottom => USSAxis.Y,
                    _ => throw new ArgumentException($"Invalid keyword: {keyword}")
                };
            }
        }

        return USSAxis.All;
    }
}