using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;


internal static class Logging {
    private static string GetElementName(VisualElement element) {
        return string.IsNullOrEmpty(element.name) ? "Unnamed element" : element.name;
    }

    private static void Log(string text) {
        #if UNITY_EDITOR
        Debug.LogWarning(text);
        #endif
    }

    internal static void AttributeNameWarning(VisualElement element, XmlAttribute attribute) {
        #if UNITY_EDITOR
        Log($"Invalid attribute name on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
        #endif
    }

    internal static void AttributeValueWarning(VisualElement element, XmlAttribute attribute) {
        Log($"Invalid attribute value on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    internal static void AttributeUnsupportedWarning(VisualElement element, XmlAttribute attribute) {
        Log($"Unsupported attribute on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    internal static void StylePropertyInvalidWarning(string property) {
        Log($"Unknown uss property: {property}");
    }

    internal static void StyleValueInvalidWarning(string property) {
        Log($"Unsupported uss value: {property}");
    }

    internal static void InvalidColorWarning(VisualElement element, string colorString) {
        Log($"Invalid color value on {GetElementName(element)}:  {colorString}");
    }

    internal static void InvalidLengthWarning(VisualElement element, string lengthString) {
        Log($"Invalid length value on {GetElementName(element)}:  {lengthString}");
    }

    internal static void InvalidRotationWarning(VisualElement element, string rotateString) {
        Log($"Invalid rotation value on {GetElementName(element)}:  {rotateString}");
    }

    internal static void InvalidScaleWarning(VisualElement element, string valueString) {
        Log($"Invalid scale value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidIntValueWarning(VisualElement element, string valueString) {
        Log($"Invalid integer value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidUrlWarning(VisualElement element, string valueString) {
        Log($"Invalid image url value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidFlexlWarning(VisualElement element, string valueString) {
        Log($"Invalid flex value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidValueWarning(VisualElement element, string valueString, string property) {
        Log($"Invalid {property} value on {GetElementName(element)}:  {valueString}");
    }
}