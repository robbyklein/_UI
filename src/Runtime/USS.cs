using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using UIBuddyTypes;
using UnityEngine;
using UnityEngine.UIElements;

internal class USS {
    internal static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
        string ussString = attr.Value;
        if (ussString == null) return;

        Dictionary<StyleProperty, string> styles = ParseUSS(ussString);

        foreach (KeyValuePair<StyleProperty, string> kvp in styles) UIBuddy.Style(el, kvp.Key, kvp.Value);
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
            if (string.IsNullOrEmpty(trimmedProperty)) continue;

            // Split by : giving us a key and value
            string[] keyValue = trimmedProperty.Split(new[] { ':' }, 2);

            //  it's not exactly two somethings invalid
            if (keyValue.Length != 2)
                // TODO: log an error here
            {
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

    internal static StyleKeyword? ValueToStyleKeyword(string value, bool excludeAuto = false) {
        switch (value) {
            case "auto":
                return StyleKeyword.Auto;
            case "initial":
                return StyleKeyword.Initial;
            case "none":
                return StyleKeyword.None;
            case "null":
                return StyleKeyword.Null;
            case "undefined":
                return StyleKeyword.Undefined;
            default:
                return null;
        }
    }

    internal static bool ApplyIfKeyword(string value, Action<StyleKeyword> applyAction) {
        StyleKeyword? keyword = ValueToStyleKeyword(value);
        if (keyword is { } validKeyword) {
            applyAction(validKeyword);
            return true;
        }

        return false;
    }

    private static void ApplyEnumStyle<T>(VisualElement el, string v, Action<T> set, Dictionary<string, T> map) {
        // First check if value is in mapping
        if (map.TryGetValue(v, out T mappedValue)) { set(mappedValue); }

        // Second check if its a keyword (auto, none, etc.)
        else if (ApplyIfKeyword(v, k => set((T)(object)k))) { return; }

        // Third log if both fail
        else { Logging.StyleValueInvalidWarning(v); }
    }

    private static void ApplyColorStyle(VisualElement el, string v, Action<StyleColor> set) {
        try {
            // First check if its a value color string
            Color color = ColorParser.ColorStringToColor(v);
            set(new StyleColor(color));
        }
        catch {
            // Second check if it was a keyword (auto, none, etc.)
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleColor)(object)k));

            // Third log an error
            if (!wasKeyword) Logging.InvalidColorWarning(el, v);
        }
    }

    private static void ApplyStyleLengthStyle(VisualElement el, string v, Action<StyleLength> set) {
        try {
            // First check if value length string
            StyleLength length = LengthParser.LengthStringToStyleLength(v);
            set(length);
        }
        catch {
            // Second check if it was a keyword
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleLength)(object)k));

            // Third log an error if not
            if (!wasKeyword) Logging.InvalidLengthWarning(el, v);
        }
    }

    private static void ApplyStyleFloatStyle(VisualElement el, string v, Action<StyleFloat> set) {
        try {
            StyleFloat length = LengthParser.LengthStringToStyleFloat(v);
            set(length);
        }
        catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleFloat)(object)k));
            if (!wasKeyword) Logging.InvalidLengthWarning(el, v);
        }
    }

    internal static void ApplyStyleAngleStyle(VisualElement el, string v, Action<StyleRotate> set) {
        try {
            Rotate rotate = AngleParser.AngleStringToRotate(v);
            set(new StyleRotate(rotate));
        }
        catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set(new StyleRotate((Rotate)(object)k)));
            if (!wasKeyword) Logging.InvalidRotationWarning(el, v);
        }
    }

    internal static void ApplyScaleStyle(VisualElement el, string v, Action<StyleScale> set) {
        try {
            Scale scale = ScaleParser.ScaleStringToScale(v);
            set(new StyleScale(scale));
        }
        catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set(new StyleScale((Scale)(object)k)));
            if (!wasKeyword) Logging.InvalidScaleWarning(el, v);
        }
    }

    internal static void ApplyStyleInt(VisualElement el, string value, Action<StyleInt> set) {
        if (int.TryParse(value, out int intValue)) { set(new StyleInt(intValue)); }
        else { Logging.InvalidIntValueWarning(el, value); }
    }


    /*
     *  Style appliers
     */

    #region Enum styles

    internal static void ApplyBackgroundRepeat(VisualElement el, string value) {
        // el.style.backgroundRepeat = "";
    }

    internal static void ApplyAlignContent(VisualElement el, string value) {
        Dictionary<string, Align> valueMap = new() {
            { "center", Align.Center },
            { "auto", Align.Auto },
            { "stretch", Align.Stretch },
            { "flex-end", Align.FlexEnd },
            { "flex-start", Align.FlexStart }
        };

        ApplyEnumStyle(el, value, v => el.style.alignContent = v, valueMap);
    }

    internal static void ApplyAlignItems(VisualElement el, string value) {
        Dictionary<string, Align> valueMap = new() {
            { "center", Align.Center },
            { "auto", Align.Auto },
            { "stretch", Align.Stretch },
            { "flex-end", Align.FlexEnd },
            { "flex-start", Align.FlexStart }
        };

        ApplyEnumStyle(el, value, v => el.style.alignItems = v, valueMap);
    }

    internal static void ApplyAlignSelf(VisualElement el, string value) {
        Dictionary<string, Align> valueMap = new() {
            { "center", Align.Center },
            { "auto", Align.Auto },
            { "stretch", Align.Stretch },
            { "flex-end", Align.FlexEnd },
            { "flex-start", Align.FlexStart }
        };

        ApplyEnumStyle(el, value, v => el.style.alignSelf = v, valueMap);
    }

    internal static void ApplyFlexDirection(VisualElement el, string value) {
        Dictionary<string, FlexDirection> valueMap = new() {
            { "row", FlexDirection.Row },
            { "column", FlexDirection.Column },
            { "column-reverse", FlexDirection.ColumnReverse },
            { "row-reverse", FlexDirection.RowReverse }
        };

        ApplyEnumStyle(el, value, v => el.style.flexDirection = v, valueMap);
    }

    internal static void ApplyDisplay(VisualElement el, string value) {
        Dictionary<string, DisplayStyle> valueMap = new() {
            { "flex", DisplayStyle.Flex },
            { "none", DisplayStyle.None }
        };

        ApplyEnumStyle(el, value, v => el.style.display = v, valueMap);
    }

    internal static void ApplyFlexWrap(VisualElement el, string value) {
        Dictionary<string, Wrap> valueMap = new() {
            { "wrap", Wrap.Wrap },
            { "nowrap", Wrap.NoWrap },
            { "wrap-reverse", Wrap.WrapReverse }
        };

        ApplyEnumStyle(el, value, v => el.style.flexWrap = v, valueMap);
    }

    internal static void ApplyJustifyContent(VisualElement el, string value) {
        Dictionary<string, Justify> valueMap = new() {
            { "flex-start", Justify.FlexStart },
            { "flex-end", Justify.FlexEnd },
            { "center", Justify.Center },
            { "space-around", Justify.SpaceAround },
            { "space-between", Justify.SpaceBetween }
        };

        ApplyEnumStyle(el, value, v => el.style.justifyContent = v, valueMap);
    }

    internal static void ApplyOverflow(VisualElement el, string value) {
        Dictionary<string, Overflow> valueMap = new() {
            { "hidden", Overflow.Hidden },
            { "visible", Overflow.Visible }
        };

        ApplyEnumStyle(el, value, v => el.style.overflow = v, valueMap);
    }

    internal static void ApplyPosition(VisualElement el, string value) {
        Dictionary<string, Position> valueMap = new() {
            { "absolute", Position.Absolute },
            { "relative", Position.Relative }
        };

        ApplyEnumStyle(el, value, v => el.style.position = v, valueMap);
    }

    internal static void ApplyTextOverflow(VisualElement el, string value) {
        Dictionary<string, TextOverflow> valueMap = new() {
            { "clip", TextOverflow.Clip },
            { "ellipsis", TextOverflow.Ellipsis }
        };

        ApplyEnumStyle(el, value, v => el.style.textOverflow = v, valueMap);
    }

    internal static void ApplyUnityBackgroundScaleMode(VisualElement el, string value) {
        Dictionary<string, ScaleMode> valueMap = new() {
            { "scale-to-fit", ScaleMode.ScaleToFit },
            { "scale-and-crop", ScaleMode.ScaleAndCrop },
            { "stretch-to-fill", ScaleMode.StretchToFill }
        };

        ApplyEnumStyle(el, value, v => el.style.unityBackgroundScaleMode = v, valueMap);
    }

    internal static void ApplyUnityFontStyle(VisualElement el, string value) {
        Dictionary<string, FontStyle> valueMap = new() {
            { "normal", FontStyle.Normal },
            { "italic", FontStyle.Italic },
            { "bold", FontStyle.Bold },
            { "bold-and-italic", FontStyle.BoldAndItalic }
        };

        ApplyEnumStyle(el, value, v => el.style.unityFontStyleAndWeight = v, valueMap);
    }

    internal static void ApplyUnityTextAlign(VisualElement el, string value) {
        Dictionary<string, TextAnchor> valueMap = new() {
            { "upper-left", TextAnchor.UpperLeft },
            { "middle-left", TextAnchor.MiddleLeft },
            { "lower-left", TextAnchor.LowerLeft },
            { "upper-center", TextAnchor.UpperCenter },
            { "middle-center", TextAnchor.MiddleCenter },
            { "lower-center", TextAnchor.LowerCenter },
            { "upper-right", TextAnchor.UpperRight },
            { "middle-right", TextAnchor.MiddleRight },
            { "lower-right", TextAnchor.LowerRight }
        };

        ApplyEnumStyle(el, value, v => el.style.unityTextAlign = v, valueMap);
    }

    internal static void ApplyUnityTextOverflowPosition(VisualElement el, string value) {
        Dictionary<string, TextOverflowPosition> valueMap = new() {
            { "start", TextOverflowPosition.Start },
            { "middle", TextOverflowPosition.Middle },
            { "end", TextOverflowPosition.End }
        };

        ApplyEnumStyle(el, value, v => el.style.unityTextOverflowPosition = v, valueMap);
    }

    internal static void ApplyWhiteSpace(VisualElement el, string value) {
        Dictionary<string, WhiteSpace> valueMap = new() {
            { "normal", WhiteSpace.Normal },
            { "nowrap", WhiteSpace.NoWrap }
        };

        ApplyEnumStyle(el, value, v => el.style.whiteSpace = v, valueMap);
    }

    internal static void ApplyVisibility(VisualElement el, string value) {
        Dictionary<string, Visibility> valueMap = new() {
            { "visible", Visibility.Visible },
            { "hidden", Visibility.Hidden }
        };

        ApplyEnumStyle(el, value, v => el.style.visibility = v, valueMap);
    }

    internal static void ApplyUnityOverflowClipBox(VisualElement el, string value) {
        Dictionary<string, OverflowClipBox> valueMap = new() {
            { "padding-box", OverflowClipBox.PaddingBox },
            { "content-box", OverflowClipBox.ContentBox }
        };

        ApplyEnumStyle(el, value, v => el.style.unityOverflowClipBox = v, valueMap);
    }

    #endregion

    #region Color styles

    internal static void ApplyBackgroundColor(VisualElement el, string value) {
        ApplyColorStyle(el, value, c => el.style.backgroundColor = c);
    }

    internal static void ApplyBorderColor(VisualElement el, string value, USSBorderSide side) {
        ApplyColorStyle(el, value, color => {
            switch (side) {
                case USSBorderSide.Bottom:
                    el.style.borderBottomColor = color;
                    break;
                case USSBorderSide.Top:
                    el.style.borderTopColor = color;
                    break;
                case USSBorderSide.Left:
                    el.style.borderLeftColor = color;
                    break;
                case USSBorderSide.Right:
                    el.style.borderRightColor = color;
                    break;
                case USSBorderSide.All:
                    el.style.borderBottomColor = color;
                    el.style.borderTopColor = color;
                    el.style.borderLeftColor = color;
                    el.style.borderRightColor = color;
                    break;
            }
        });
    }

    internal static void ApplyColor(VisualElement el, string value) {
        ApplyColorStyle(el, value, color => el.style.color = color);
    }

    internal static void ApplyUnityBackgroundImageTintColor(VisualElement el, string value) {
        ApplyColorStyle(el, value, color => el.style.unityBackgroundImageTintColor = color);
    }

    internal static void ApplyUnityTextOutlineColor(VisualElement el, string value) {
        ApplyColorStyle(el, value, color => el.style.unityTextOutlineColor = color);
    }

    #endregion

    #region Length styles

    internal static void ApplyPadding(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (direction) {
                case USSDirection.Top:
                    ApplyStyleLengthStyle(el, value, length => el.style.paddingTop = length);
                    break;
                case USSDirection.Right:
                    ApplyStyleLengthStyle(el, value, length => el.style.paddingRight = length);
                    break;
                case USSDirection.Bottom:
                    ApplyStyleLengthStyle(el, value, length => el.style.paddingBottom = length);
                    break;
                case USSDirection.Left:
                    ApplyStyleLengthStyle(el, value, length => el.style.paddingLeft = length);
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
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    internal static void ApplyMargin(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (direction) {
                case USSDirection.Top:
                    ApplyStyleLengthStyle(el, value, length => el.style.marginTop = length);
                    break;
                case USSDirection.Right:
                    ApplyStyleLengthStyle(el, value, length => el.style.marginRight = length);
                    break;
                case USSDirection.Bottom:
                    ApplyStyleLengthStyle(el, value, length => el.style.marginBottom = length);
                    break;
                case USSDirection.Left:
                    ApplyStyleLengthStyle(el, value, length => el.style.marginLeft = length);
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
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    internal static void ApplyBorderWidth(VisualElement el, string value, USSDirection direction) {
        try {
            StyleFloat[] lengths = LengthParser.LengthStringsToStyleFloats(value);

            switch (direction) {
                case USSDirection.Top:
                    ApplyStyleFloatStyle(el, value, length => el.style.borderTopWidth = length);
                    break;
                case USSDirection.Right:
                    ApplyStyleFloatStyle(el, value, length => el.style.borderRightWidth = length);
                    break;
                case USSDirection.Bottom:
                    ApplyStyleFloatStyle(el, value, length => el.style.borderBottomWidth = length);
                    break;
                case USSDirection.Left:
                    ApplyStyleFloatStyle(el, value, length => el.style.borderLeftWidth = length);
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
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    internal static void ApplyDirection(VisualElement el, string value, USSDirection direction) {
        try {
            StyleLength length = LengthParser.LengthStringToStyleLength(value);

            switch (direction) {
                case USSDirection.Top:
                    ApplyStyleLengthStyle(el, value, length => el.style.top = length);
                    break;
                case USSDirection.Right:
                    ApplyStyleLengthStyle(el, value, length => el.style.right = length);
                    break;
                case USSDirection.Bottom:
                    ApplyStyleLengthStyle(el, value, length => el.style.bottom = length);
                    break;
                case USSDirection.Left:
                    ApplyStyleLengthStyle(el, value, length => el.style.left = length);
                    break;
            }
        }
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    internal static void ApplyWidth(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.width = length);
    }

    internal static void ApplyMaxWidth(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.maxWidth = length);
    }

    internal static void ApplyMinWidth(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.minWidth = length);
    }

    internal static void ApplyHeight(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.height = length);
    }

    internal static void ApplyMaxHeight(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.maxHeight = length);
    }

    internal static void ApplyMinHeight(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.minHeight = length);
    }

    internal static void ApplyBorderRadius(VisualElement el, string value, USSCorner corner) {
        try {
            StyleLength[] lengths = LengthParser.LengthStringsToStyleLengths(value);

            switch (corner) {
                case USSCorner.TopLeft:
                    ApplyStyleLengthStyle(el, value, length => el.style.borderTopLeftRadius = length);
                    break;
                case USSCorner.TopRight:
                    ApplyStyleLengthStyle(el, value, length => el.style.borderTopRightRadius = length);
                    break;
                case USSCorner.BottomLeft:
                    ApplyStyleLengthStyle(el, value, length => el.style.borderBottomLeftRadius = length);
                    break;
                case USSCorner.BottomRight:
                    ApplyStyleLengthStyle(el, value, length => el.style.borderBottomRightRadius = length);
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
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    internal static void ApplyFontSize(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.fontSize = length);
    }

    internal static void ApplyLetterSpacing(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.letterSpacing = length);
    }

    internal static void ApplyOpacity(VisualElement el, string value) {
        ApplyStyleFloatStyle(el, value, length => el.style.opacity = length);
    }

    internal static void ApplyWordSpacing(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.wordSpacing = length);
    }

    internal static void ApplyFlexBasis(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.flexBasis = length);
    }

    internal static void ApplyFlexGrow(VisualElement el, string value) {
        ApplyStyleFloatStyle(el, value, length => el.style.flexGrow = length);
    }

    internal static void ApplyFlexShrink(VisualElement el, string value) {
        ApplyStyleFloatStyle(el, value, length => el.style.flexShrink = length);
    }

    internal static void ApplyUnityTextOutlineWidth(VisualElement el, string value) {
        ApplyStyleFloatStyle(el, value, length => el.style.unityTextOutlineWidth = length);
    }

    internal static void ApplyUnitySliceScale(VisualElement el, string value) {
        ApplyStyleFloatStyle(el, value, length => el.style.unitySliceScale = length);
    }

    internal static void ApplyUnityParagraphSpacing(VisualElement el, string value) {
        ApplyStyleLengthStyle(el, value, length => el.style.unityParagraphSpacing = length);
    }

    #endregion

    #region StyleInt styles

    internal static void ApplyUnitySliceLeft(VisualElement el, string value) {
        ApplyStyleInt(el, value, v => el.style.unitySliceLeft = v);
    }

    internal static void ApplyUnitySliceRight(VisualElement el, string value) {
        ApplyStyleInt(el, value, v => el.style.unitySliceRight = v);
    }

    internal static void ApplyUnitySliceTop(VisualElement el, string value) {
        ApplyStyleInt(el, value, v => el.style.unitySliceTop = v);
    }

    internal static void ApplyUnitySliceBottom(VisualElement el, string value) {
        ApplyStyleInt(el, value, v => el.style.unitySliceBottom = v);
    }

    #endregion

    #region Other styles

    internal static void ApplyRotate(VisualElement el, string value) {
        ApplyStyleAngleStyle(el, value, length => el.style.rotate = length);
    }

    internal static void ApplyScale(VisualElement el, string value) {
        ApplyScaleStyle(el, value, length => el.style.scale = length);
    }

    internal static void ApplyBackgroundImage(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.backgroundImage = new StyleBackground((Texture2D)(object)null))) {
            return;
        }

        try {
            Texture2D texture = UrlParser.UrlStringToTexture2d(value);
            if (texture != null) { el.style.backgroundImage = new StyleBackground(texture); }
        }
        catch { Logging.InvalidUrlWarning(el, value); }
    }


    internal static void ApplyBackgroundSize(VisualElement el, string value) {
        try {
            Length[] sizes = LengthParser.LengthStringsToLengths(value);

            if (sizes.Length == 1) { el.style.backgroundSize = new BackgroundSize(sizes[0], sizes[0]); }
            else if (sizes.Length == 2) { el.style.backgroundSize = new BackgroundSize(sizes[0], sizes[1]); }
            else { Logging.InvalidLengthWarning(el, value); }
        }
        catch { Logging.InvalidLengthWarning(el, value); }
    }

    #endregion
}