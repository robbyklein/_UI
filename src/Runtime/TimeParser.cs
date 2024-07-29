using System;

internal static class TimeParser {
    public static float ParseTime(string timeString) {
        if (string.IsNullOrEmpty(timeString)) {
            throw new ArgumentException("Time string cannot be null or empty.");
        }

        timeString = timeString.Trim().ToLower();

        if (timeString.EndsWith("ms")) {
            if (float.TryParse(timeString.Substring(0, timeString.Length - 2), out float msValue)) {
                return msValue / 1000f;
            }
        } else if (timeString.EndsWith("s")) {
            if (float.TryParse(timeString.Substring(0, timeString.Length - 1), out float sValue)) {
                return sValue;
            }
        }

        throw new ArgumentException($"Invalid time format: {timeString}");
    }
}