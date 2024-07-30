using System;
using System.Text.RegularExpressions;
using _UITypes;
using UnityEngine;

internal static class ColorParser {
    private struct ColorInfo {
        public ColorType Type;
        public string[] Value;
    }

    internal static Color ColorStringToColor(string colorString) {
        ColorInfo info = DetermineColorType(colorString);

        switch (info.Type) {
            case ColorType.Named:
                if (Maps.USSColors.TryGetValue(colorString.ToLower(), out string hex)) {
                    return ColorFromHex(hex);
                }

                break;
            case ColorType.Hex:
                return ColorFromHex(colorString);
            case ColorType.HSL:
                float h = float.Parse(info.Value[0]);
                float s = float.Parse(info.Value[1]) / 100f;
                float l = float.Parse(info.Value[2]) / 100f;
                return ColorFromHsl(h, s, l);
            case ColorType.RGB:
                byte r = byte.Parse(info.Value[0]);
                byte g = byte.Parse(info.Value[1]);
                byte b = byte.Parse(info.Value[2]);
                return new Color32(r, g, b, 255);
            case ColorType.RGBA:
                r = byte.Parse(info.Value[0]);
                g = byte.Parse(info.Value[1]);
                b = byte.Parse(info.Value[2]);
                byte a = (byte)(float.Parse(info.Value[3]) * 255);
                return new Color32(r, g, b, a);
        }

        throw new ArgumentException("Invalid color format.");
    }

    private static ColorInfo DetermineColorType(string colorString) {
        if (string.IsNullOrEmpty(colorString)) {
            throw new ArgumentException("Color string cannot be null or empty.");
        }

        // Check for Named Colors (e.g., "red")
        if (IsNamedColor(colorString)) {
            return new ColorInfo() {
                Type = ColorType.Named,
                Value = new[] { colorString }
            };
        }

        // Check for Hexadecimal Notation (e.g., "#123123")
        if (Regex.IsMatch(colorString, @"^#(?:[0-9a-fA-F]{3}){1,2}$")) {
            return new ColorInfo() {
                Type = ColorType.Hex,
                Value = new[] { colorString }
            };
        }

        // Check for RGB Notation (e.g., "rgb(255, 0, 0)")
        Match rgbMatch = Regex.Match(colorString, @"^rgb\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\)$");
        if (rgbMatch.Success) {
            return new ColorInfo() {
                Type = ColorType.RGB,
                Value = new[] {
                    rgbMatch.Groups[1].Value,
                    rgbMatch.Groups[2].Value,
                    rgbMatch.Groups[3].Value
                }
            };
        }

        // Check for RGBA Notation (e.g., "rgba(255, 0, 0, 1)")
        Match rgbaMatch = Regex.Match(colorString, @"^rgba\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3}),\s*(0|1|0?\.\d+)\)$");
        if (rgbaMatch.Success) {
            return new ColorInfo() {
                Type = ColorType.RGBA,
                Value = new[] {
                    rgbaMatch.Groups[1].Value,
                    rgbaMatch.Groups[2].Value,
                    rgbaMatch.Groups[3].Value,
                    rgbaMatch.Groups[4].Value
                }
            };
        }

        // Check for HSL Notation (e.g., "hsl(0, 100%, 50%)")
        Match hslMatch = Regex.Match(colorString, @"^hsl\((\d{1,3}),\s*(\d{1,3})%,\s*(\d{1,3})%\)$");
        if (hslMatch.Success) {
            return new ColorInfo() {
                Type = ColorType.HSL,
                Value = new[] {
                    hslMatch.Groups[1].Value,
                    hslMatch.Groups[2].Value,
                    hslMatch.Groups[3].Value
                }
            };
        }

        throw new ArgumentException("Invalid color format.");
    }

    private static bool IsNamedColor(string colorString) {
        return Maps.USSColors.TryGetValue(colorString.ToLower(), out _);
    }

    private static Color ColorFromHex(string hex) {
        hex = hex.Replace("#", "");
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        return new Color32(r, g, b, 255);
    }

    private static Color ColorFromHsl(float h, float s, float l) {
        float r = l;
        float g = l;
        float b = l;
        float v = l <= 0.5f ? l * (1f + s) : l + s - l * s;

        if (v > 0) {
            float m = l + l - v;
            float sv = (v - m) / v;
            h *= 6f;
            int sextant = (int)h;
            float fract = h - sextant;
            float vsf = v * sv * fract;
            float mid1 = m + vsf;
            float mid2 = v - vsf;

            switch (sextant) {
                case 0:
                    r = v;
                    g = mid1;
                    b = m;
                    break;
                case 1:
                    r = mid2;
                    g = v;
                    b = m;
                    break;
                case 2:
                    r = m;
                    g = v;
                    b = mid1;
                    break;
                case 3:
                    r = m;
                    g = mid2;
                    b = v;
                    break;
                case 4:
                    r = mid1;
                    g = m;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = m;
                    b = mid2;
                    break;
            }
        }

        return new Color(r, g, b);
    }
}