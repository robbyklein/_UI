using System.Collections.Generic;
using System.Xml;
using UIBuddyTypes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIBuddyTypes {
    public struct StylePropertyValue {
        public string Value;
        public StyleUnit Unit;
    }
}

internal class USS {
    internal static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
        string ussString = attr.Value;
        if (ussString == null) {
            return;
        }

        Dictionary<StyleProperty, string> styles = ParseUSS(ussString);

        foreach (KeyValuePair<StyleProperty, string> kvp in styles) {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
    }


    internal static Dictionary<StyleProperty, string> ParseUSS(string ussString) {
        Dictionary<StyleProperty, string> styles = new();

        // Split into properties
        string[] properties = ussString.Split(";");

        // Loop them
        foreach (string property in properties) {
            // Trim extra white space
            string trimmedProperty = property.Trim();

            // Make sure we actually have a property to work with
            if (string.IsNullOrEmpty(trimmedProperty)) {
                continue;
            }

            // Split by : giving us a key and value
            string[] keyValue = trimmedProperty.Split(new[] { ':' }, 2);

            //  it's not exactly two somethings invalid
            if (keyValue.Length != 2) {
                // TODO: log an error here
                continue;
            }

            // Check key
            string key = keyValue[0].Trim();

            if (!Maps.StyleProperties.TryGetValue(key, out StyleProperty styleProperty)) {
                Logging.StylePropertyInvalidWarning(key);
                continue;
            }

            // Check value
            string value = keyValue[1].Trim();

            if (value == "") {
                Logging.StylePropertyInvalidWarning(key);
                continue;
            }

            styles[styleProperty] = value;
        }

        return styles;
    }

    /*
     *  Style appliers
     */

    internal static void ApplyAlignContent(VisualElement el, string value) {
        switch (value) {
            case "center":
                el.style.alignContent = Align.Center;
                break;
            case "auto":
                el.style.alignContent = Align.Auto;
                break;
            case "stretch":
                el.style.alignContent = Align.Stretch;
                break;
            case "flex-end":
                el.style.alignContent = Align.FlexEnd;
                break;
            case "flex-start":
                el.style.alignContent = Align.FlexStart;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyAlignItems(VisualElement el, string value) {
        switch (value) {
            case "center":
                el.style.alignItems = Align.Center;
                break;
            case "auto":
                el.style.alignItems = Align.Auto;
                break;
            case "stretch":
                el.style.alignItems = Align.Stretch;
                break;
            case "flex-end":
                el.style.alignItems = Align.FlexEnd;
                break;
            case "flex-start":
                el.style.alignItems = Align.FlexStart;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyAlignSelf(VisualElement el, string value) {
        switch (value) {
            case "center":
                el.style.alignSelf = Align.Center;
                break;
            case "auto":
                el.style.alignSelf = Align.Auto;
                break;
            case "stretch":
                el.style.alignSelf = Align.Stretch;
                break;
            case "flex-end":
                el.style.alignSelf = Align.FlexEnd;
                break;
            case "flex-start":
                el.style.alignSelf = Align.FlexStart;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyFlexDirection(VisualElement el, string value) {
        switch (value) {
            case "row":
                el.style.flexDirection = FlexDirection.Row;
                break;
            case "column":
                el.style.flexDirection = FlexDirection.Column;
                break;
            case "column-reverse":
                el.style.flexDirection = FlexDirection.ColumnReverse;
                break;
            case "row-reverse":
                el.style.flexDirection = FlexDirection.RowReverse;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyDisplay(VisualElement el, string value) {
        switch (value) {
            case "flex":
                el.style.display = DisplayStyle.Flex;
                break;
            case "none":
                el.style.display = DisplayStyle.None;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyFlexWrap(VisualElement el, string value) {
        switch (value) {
            case "wrap":
                el.style.flexWrap = Wrap.Wrap;
                break;
            case "nowrap":
                el.style.flexWrap = Wrap.NoWrap;
                break;
            case "wrap-reverse":
                el.style.flexWrap = Wrap.WrapReverse;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }

    internal static void ApplyJustifyContent(VisualElement el, string value) {
        switch (value) {
            case "flex-start":
                el.style.justifyContent = Justify.FlexStart;
                break;
            case "flex-end":
                el.style.justifyContent = Justify.FlexEnd;
                break;
            case "center":
                el.style.justifyContent = Justify.Center;
                break;
            case "space-around":
                el.style.justifyContent = Justify.SpaceAround;
                break;
            case "space-between":
                el.style.justifyContent = Justify.SpaceBetween;
                break;
            default:
                Logging.StyleValueInvalidWarning(value);
                break;
        }
    }
}