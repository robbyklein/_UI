using System;
using UnityEngine.UIElements;
using ArgumentException = System.ArgumentException;

internal static class LengthParser {
    internal static Length LengthStringToLength(string lengthString) {
        // Make sure we have a string
        if (string.IsNullOrEmpty(lengthString)) {
            throw new ArgumentException("Invalid length string");
        }

        // Normalize it
        lengthString = lengthString.Trim().ToLower();

        // Check for px
        if (lengthString.EndsWith("px")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 2), out float pxValue)) {
                return new Length(pxValue, LengthUnit.Pixel);
            }
        }

        // Check for %
        else if (lengthString.EndsWith("%")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 1), out float percentValue)) {
                return new Length(percentValue, LengthUnit.Percent);
            }
        }

        // Check for auto
        else if (lengthString == "auto") {
            return Length.Auto();
        }

        throw new ArgumentException($"Invalid length string: {lengthString}");
    }

    internal static StyleLength LengthStringToStyleLength(string lengthString) {
        // Check for keywords
        StyleKeyword? keyword = USS.ValueToStyleKeyword(lengthString);
        if (keyword is { } word) {
            return new StyleLength(word);
        }

        // Convert to a length
        Length length = LengthStringToLength(lengthString);

        // Convert to style length
        return new StyleLength(length);
    }

    internal static StyleFloat LengthStringToStyleFloat(string lengthString) {
        // Check for null or empty string
        if (string.IsNullOrEmpty(lengthString)) {
            throw new ArgumentException("Invalid length string");
        }

        // Check for keywords
        StyleKeyword? keyword = USS.ValueToStyleKeyword(lengthString);
        if (keyword is { } word) {
            return new StyleFloat(word);
        }

        // Normalize the string
        lengthString = lengthString.Trim().ToLower();

        // Check for px unit
        if (lengthString.EndsWith("px")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 2), out float pxValue)) {
                return new StyleFloat(pxValue);
            }
        }

        // Check for unsupported percentage unit
        if (lengthString.EndsWith("%")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 1), out float percentValue)) {
                throw new ArgumentException("_UI does not currently support percentage-based border lengths");
            }
        }

        // Handle unitless value
        if (float.TryParse(lengthString, out float unitlessValue)) {
            return new StyleFloat(unitlessValue);
        }

        // If none of the above conditions are met, throw an exception
        throw new ArgumentException($"Invalid length string: {lengthString}", nameof(lengthString));
    }

    internal static StyleLength[] LengthStringsToStyleLengths(string lengthsString) {
        // Make sure we have a string
        if (string.IsNullOrEmpty(lengthsString)) {
            throw new ArgumentException("Invalid length string");
        }

        // Split into separate values
        string[] lengths = lengthsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Create an array the length of the split string
        StyleLength[] styleLengths = new StyleLength[lengths.Length];

        // Add each style length
        for (int i = 0; i < lengths.Length; i++) {
            styleLengths[i] = LengthStringToStyleLength(lengths[i]);
        }

        return styleLengths;
    }

    internal static StyleFloat[] LengthStringsToStyleFloats(string lengthsString) {
        // Make sure we have a string
        if (string.IsNullOrEmpty(lengthsString)) {
            throw new ArgumentException("Invalid length string");
        }

        // Split into separate values
        string[] lengths = lengthsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Create an array the length of the split string
        StyleFloat[] styleFloats = new StyleFloat[lengths.Length];

        // Add each length
        for (int i = 0; i < lengths.Length; i++) {
            styleFloats[i] = LengthStringToStyleFloat(lengths[i]);
        }

        return styleFloats;
    }

    internal static Length[] LengthStringsToLengths(string lengthsString) {
        // Make sure we have a string
        if (string.IsNullOrEmpty(lengthsString)) {
            throw new ArgumentException("Invalid length string");
        }

        // Split into separate values
        string[] lengths = lengthsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Create an array the length of the split string
        Length[] lengthsArray = new Length[lengths.Length];

        // Add each length
        for (int i = 0; i < lengths.Length; i++) {
            lengthsArray[i] = LengthStringToLength(lengths[i]);
        }

        return lengthsArray;
    }
}