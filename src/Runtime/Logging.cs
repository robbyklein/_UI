using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;


internal static class Logging {
    private static string GetElementName(VisualElement element) {
        return string.IsNullOrEmpty(element.name) ? "Unnamed element" : element.name;
    }

    internal static void AttributeNameWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute name on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    internal static void AttributeValueWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute value on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    internal static void AttributeUnsupportedWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Unsupported attribute on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    internal static void StylePropertyInvalidWarning(string property) {
        Debug.LogWarning(
            $"Unknown uss property: {property}");
    }

    internal static void StyleValueInvalidWarning(string property) {
        Debug.LogWarning(
            $"Unsupported uss value: {property}");
    }

    internal static void InvalidColorWarning(VisualElement element, string colorString) {
        Debug.LogWarning(
            $"Invalid color value on {GetElementName(element)}:  {colorString}");
    }

    internal static void InvalidLengthWarning(VisualElement element, string lengthString) {
        Debug.LogWarning(
            $"Invalid length value on {GetElementName(element)}:  {lengthString}");
    }

    internal static void InvalidRotationWarning(VisualElement element, string rotateString) {
        Debug.LogWarning(
            $"Invalid rotation value on {GetElementName(element)}:  {rotateString}");
    }

    internal static void InvalidScaleWarning(VisualElement element, string valueString) {
        Debug.LogWarning(
            $"Invalid scale value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidIntValueWarning(VisualElement element, string valueString) {
        Debug.LogWarning(
            $"Invalid integer value on {GetElementName(element)}:  {valueString}");
    }

    internal static void InvalidUrlWarning(VisualElement element, string valueString) {
        Debug.LogWarning(
            $"Invalid image url value on {GetElementName(element)}:  {valueString}");
    }
}