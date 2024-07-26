using System;
using UnityEngine.UIElements;

internal static class AngleParser {
    internal static Rotate AngleStringToRotate(string value) {
        value = value.Trim();

        if (value.EndsWith("deg")) {
            float degrees = float.Parse(value.Replace("deg", "").Trim());
            return new Rotate(Angle.Degrees(degrees));
        }

        if (value.EndsWith("grad")) {
            float gradians = float.Parse(value.Replace("grad", "").Trim());
            return new Rotate(Angle.Gradians(gradians));
        }

        if (value.EndsWith("rad")) {
            float radians = float.Parse(value.Replace("rad", "").Trim());
            return new Rotate(Angle.Radians(radians));
        }

        if (value.EndsWith("turn")) {
            float turns = float.Parse(value.Replace("turn", "").Trim());
            return new Rotate(Angle.Turns(turns));
        }

        throw new FormatException("Invalid angle format");
    }
}