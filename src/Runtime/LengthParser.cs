using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal static class LengthParser {
    internal static StyleLength LengthStringToStyleLength(string lengthString) {
        if (string.IsNullOrEmpty(lengthString)) {
            throw new ArgumentException("Length string cannot be null or empty", nameof(lengthString));
        }

        lengthString = lengthString.Trim().ToLower();

        // Check for keywords
        StyleKeyword? keyword = USS.ValueToStyleKeyword(lengthString);
        if (keyword is { } word) {
            return word;
        }

        // Check for px

        if (lengthString.EndsWith("px")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 2), out float pxValue)) {
                return new StyleLength(pxValue);
            }
        }

        // Check for 
        else if (lengthString.EndsWith("%")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 1), out float percentValue)) {
                return new StyleLength(new Length(percentValue, LengthUnit.Percent));
            }
        }

        throw new ArgumentException($"Invalid length string: {lengthString}", nameof(lengthString));
    }

    internal static StyleFloat LengthStringToStyleFloat(string lengthString) {
        if (string.IsNullOrEmpty(lengthString)) {
            throw new ArgumentException("Length string cannot be null or empty", nameof(lengthString));
        }

        // Check for keyords
        StyleKeyword? keyword = USS.ValueToStyleKeyword(lengthString);
        if (keyword is { } word) {
            return word;
        }

        lengthString = lengthString.Trim().ToLower();
        if (lengthString.EndsWith("px")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 2), out float pxValue)) {
                return new StyleFloat(pxValue);
            }
        }
        else if (lengthString.EndsWith("%")) {
            if (float.TryParse(lengthString.Substring(0, lengthString.Length - 1), out float percentValue)) {
                throw new ArgumentException($"UIBuddy does not current support percentage based border lengths");
            }
        }

        throw new ArgumentException($"Invalid length string: {lengthString}", nameof(lengthString));
    }


    internal static StyleLength[] LengthStringsToStyleLengths(string lengthsString) {
        if (string.IsNullOrEmpty(lengthsString)) {
            throw new ArgumentException("Lengths string cannot be null or empty", nameof(lengthsString));
        }

        string[] lengths = lengthsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<StyleLength> styleLengths = new();

        foreach (string length in lengths) {
            styleLengths.Add(LengthStringToStyleLength(length));
        }

        return styleLengths.ToArray();
    }

    internal static StyleFloat[] LengthStringsToStyleFloats(string lengthsString) {
        if (string.IsNullOrEmpty(lengthsString)) {
            throw new ArgumentException("Lengths string cannot be null or empty", nameof(lengthsString));
        }

        string[] lengths = lengthsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<StyleFloat> styleFloats = new();

        foreach (string length in lengths) {
            styleFloats.Add(LengthStringToStyleFloat(length));
        }

        return styleFloats.ToArray();
    }
}