using System;
using UnityEngine;
using UnityEngine.UIElements;

internal static class ScaleParser {
    internal static Scale ScaleStringToScale(string value) {
        value = value.Trim();

        string[] components = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (components.Length == 1) {
            float uniformScale = ParseScaleComponent(components[0]);
            return new Scale(new Vector3(uniformScale, uniformScale, 1f));
        }

        if (components.Length == 2) {
            float scaleX = ParseScaleComponent(components[0]);
            float scaleY = ParseScaleComponent(components[1]);
            return new Scale(new Vector3(scaleX, scaleY, 1f));
        }

        throw new FormatException("Invalid scale format");
    }

    private static float ParseScaleComponent(string component) {
        component = component.Trim();
        if (component.EndsWith("%")) {
            float percentValue = float.Parse(component.Replace("%", ""));
            return percentValue / 100.0f;
        }
        else return float.Parse(component);
    }
}