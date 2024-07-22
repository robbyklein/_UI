using System;
using System.Collections.Generic;
using System.Xml;
using UIBuddyTypes;
using UnityEngine;
using UnityEngine.UIElements;


internal class USS {
    internal static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
        string ussString = attr.Value;
        if (ussString == null) {
            return;
        }

        Dictionary<StyleProperty, string> styles = ParseUSS(ussString);

        foreach (KeyValuePair<StyleProperty, string> kvp in styles) {
            UIBuddy.Style(el, kvp.Key, kvp.Value);
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

    internal static void ApplyBackgroundColor(VisualElement el, string value) {
        try {
            Color color = ColorParser.ColorStringToColor(value);
            el.style.backgroundColor = new StyleColor(color);
        }
        catch {
            Logging.InvalidColorWarning(el, value);
        }
    }

    internal static void ApplyBorderColor(VisualElement el, string value, USSBorderSide side) {
        try {
            Color color = ColorParser.ColorStringToColor(value);

            switch (side) {
                case USSBorderSide.Bottom:
                    el.style.borderBottomColor = new StyleColor(color);
                    break;
                case USSBorderSide.Top:
                    el.style.borderTopColor = new StyleColor(color);
                    break;
                case USSBorderSide.Left:
                    el.style.borderLeftColor = new StyleColor(color);
                    break;
                case USSBorderSide.Right:
                    el.style.borderRightColor = new StyleColor(color);
                    break;
                case USSBorderSide.All:
                    el.style.borderBottomColor = new StyleColor(color);
                    el.style.borderTopColor = new StyleColor(color);
                    el.style.borderLeftColor = new StyleColor(color);
                    el.style.borderRightColor = new StyleColor(color);
                    break;
            }
        }
        catch {
            Logging.InvalidColorWarning(el, value);
        }
    }

    internal static void ApplyColor(VisualElement el, string value) {
        try {
            Color color = ColorParser.ColorStringToColor(value);
            el.style.color = new StyleColor(color);
        }
        catch {
            Logging.InvalidColorWarning(el, value);
        }
    }

    internal static void ApplyUnityBackgroundImageTintColor(VisualElement el, string value) {
        try {
            Color color = ColorParser.ColorStringToColor(value);
            el.style.unityBackgroundImageTintColor = new StyleColor(color);
        }
        catch {
            Logging.InvalidColorWarning(el, value);
        }
    }

    internal static void ApplyUnityTextOutlineColor(VisualElement el, string value) {
        try {
            Color color = ColorParser.ColorStringToColor(value);
            el.style.unityTextOutlineColor = new StyleColor(color);
        }
        catch {
            Logging.InvalidColorWarning(el, value);
        }
    }

    internal static void ApplyPadding(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (direction) {
                case USSDirection.Top:
                    el.style.paddingTop = lengths[0];
                    break;
                case USSDirection.Right:
                    el.style.paddingRight = lengths[0];
                    break;
                case USSDirection.Bottom:
                    el.style.paddingBottom = lengths[0];
                    break;
                case USSDirection.Left:
                    el.style.paddingLeft = lengths[0];
                    break;
                case USSDirection.All:
                    switch (lengths.Length) {
                        case 4:
                            el.style.paddingTop = lengths[0];
                            el.style.paddingRight = lengths[1];
                            el.style.paddingBottom = lengths[2];
                            el.style.paddingLeft = lengths[3];
                            break;
                        case 3:
                            el.style.paddingTop = lengths[0];
                            el.style.paddingRight = lengths[1];
                            el.style.paddingBottom = lengths[2];
                            el.style.paddingLeft = lengths[1];
                            break;
                        case 2:
                            el.style.paddingTop = lengths[0];
                            el.style.paddingRight = lengths[1];
                            el.style.paddingBottom = lengths[0];
                            el.style.paddingLeft = lengths[1];
                            break;
                        case 1:
                            el.style.paddingTop = lengths[0];
                            el.style.paddingRight = lengths[0];
                            el.style.paddingBottom = lengths[0];
                            el.style.paddingLeft = lengths[0];
                            break;
                    }

                    break;
            }
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyMargin(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (direction) {
                case USSDirection.Top:
                    el.style.marginTop = lengths[0];
                    break;
                case USSDirection.Right:
                    el.style.marginRight = lengths[0];
                    break;
                case USSDirection.Bottom:
                    el.style.marginBottom = lengths[0];
                    break;
                case USSDirection.Left:
                    el.style.marginLeft = lengths[0];
                    break;
                case USSDirection.All:
                    switch (lengths.Length) {
                        case 4:
                            el.style.marginTop = lengths[0];
                            el.style.marginRight = lengths[1];
                            el.style.marginBottom = lengths[2];
                            el.style.marginLeft = lengths[3];
                            break;
                        case 3:
                            el.style.marginTop = lengths[0];
                            el.style.marginRight = lengths[1];
                            el.style.marginBottom = lengths[2];
                            el.style.marginLeft = lengths[1];
                            break;
                        case 2:
                            el.style.marginTop = lengths[0];
                            el.style.marginRight = lengths[1];
                            el.style.marginBottom = lengths[0];
                            el.style.marginLeft = lengths[1];
                            break;
                        case 1:
                            el.style.marginTop = lengths[0];
                            el.style.marginRight = lengths[0];
                            el.style.marginBottom = lengths[0];
                            el.style.marginLeft = lengths[0];
                            break;
                    }

                    break;
            }
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyBorderWidth(VisualElement el, string value, USSDirection direction) {
        try {
            StyleFloat[] lengths = LengthParser.LengthStringsToStyleFloats(value);

            switch (direction) {
                case USSDirection.Top:
                    el.style.borderTopWidth = lengths[0];
                    break;
                case USSDirection.Right:
                    el.style.borderRightWidth = lengths[0];
                    break;
                case USSDirection.Bottom:
                    el.style.borderBottomWidth = lengths[0];
                    break;
                case USSDirection.Left:
                    el.style.borderLeftWidth = lengths[0];
                    break;
                case USSDirection.All:
                    switch (lengths.Length) {
                        case 4:
                            el.style.borderTopWidth = lengths[0];
                            el.style.borderRightWidth = lengths[1];
                            el.style.borderBottomWidth = lengths[2];
                            el.style.borderLeftWidth = lengths[3];
                            break;
                        case 3:
                            el.style.borderTopWidth = lengths[0];
                            el.style.borderRightWidth = lengths[1];
                            el.style.borderBottomWidth = lengths[2];
                            el.style.borderLeftWidth = lengths[1];
                            break;
                        case 2:
                            el.style.borderTopWidth = lengths[0];
                            el.style.borderRightWidth = lengths[1];
                            el.style.borderBottomWidth = lengths[0];
                            el.style.borderLeftWidth = lengths[1];
                            break;
                        case 1:
                            el.style.borderTopWidth = lengths[0];
                            el.style.borderRightWidth = lengths[0];
                            el.style.borderBottomWidth = lengths[0];
                            el.style.borderLeftWidth = lengths[0];
                            break;
                    }

                    break;
            }
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyDirection(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);

            switch (direction) {
                case USSDirection.Top:
                    el.style.top = length;
                    break;
                case USSDirection.Right:
                    el.style.right = length;
                    break;
                case USSDirection.Bottom:
                    el.style.bottom = length;
                    break;
                case USSDirection.Left:
                    el.style.left = length;
                    break;
            }
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyWidth(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.width = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyMaxWidth(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.maxWidth = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyMinWidth(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.minWidth = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyHeight(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.height = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyMaxHeight(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.maxHeight = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyMinHeight(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.minHeight = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }


    internal static void ApplyBorderRadius(VisualElement el, string value, USSCorner corner) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (corner) {
                case USSCorner.TopLeft:
                    el.style.borderTopLeftRadius = lengths[0];
                    break;
                case USSCorner.TopRight:
                    el.style.borderTopRightRadius = lengths[0];
                    break;
                case USSCorner.BottomLeft:
                    el.style.borderBottomLeftRadius = lengths[0];
                    break;
                case USSCorner.BottomRight:
                    el.style.borderBottomRightRadius = lengths[0];
                    break;
                case USSCorner.All:
                    switch (lengths.Length) {
                        case 4:
                            el.style.borderTopLeftRadius = lengths[0];
                            el.style.borderTopRightRadius = lengths[1];
                            el.style.borderBottomRightRadius = lengths[2];
                            el.style.borderBottomLeftRadius = lengths[3];
                            break;
                        case 3:
                            el.style.borderTopLeftRadius = lengths[0];
                            el.style.borderTopRightRadius = lengths[1];
                            el.style.borderBottomLeftRadius = lengths[1];
                            el.style.borderBottomRightRadius = lengths[2];
                            break;
                        case 2:
                            el.style.borderTopLeftRadius = lengths[0];
                            el.style.borderBottomRightRadius = lengths[0];
                            el.style.borderTopRightRadius = lengths[1];
                            el.style.borderBottomLeftRadius = lengths[1];
                            break;
                        case 1:
                            el.style.borderTopLeftRadius = lengths[0];
                            el.style.borderBottomRightRadius = lengths[0];
                            el.style.borderTopRightRadius = lengths[0];
                            el.style.borderBottomLeftRadius = lengths[0];
                            break;
                    }

                    break;
            }
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyFontSize(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.fontSize = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyLetterSpacing(VisualElement el, string value) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);
            el.style.letterSpacing = length;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyOpacity(VisualElement el, string value) {
        try {
            StyleFloat amount = LengthParser.LengthStringToStyleFloat(value);
            el.style.opacity = amount;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyWordSpacing(VisualElement el, string value) {
        try {
            StyleLength amount = LengthParser.LengthStringToStyleLength(value);
            el.style.wordSpacing = amount;
        }
        catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }
}