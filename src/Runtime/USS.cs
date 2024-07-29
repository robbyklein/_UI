using System;
using System.Collections.Generic;
using System.Xml;
using UIBuddyTypes;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;

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

    private static void ApplyEnumStyle<T>(VisualElement _, string v, Action<T> set, Dictionary<string, T> map) {
        // First check if value is in mapping
        if (map.TryGetValue(v, out T mappedValue)) {
            set(mappedValue);
        }

        // Second check if its a keyword (auto, none, etc.)
        else if (ApplyIfKeyword(v, k => set((T)(object)k))) {
        }

        // Third log if both fail
        else {
            Logging.StyleValueInvalidWarning(v);
        }
    }

    private static void ApplyColorStyle(VisualElement el, string v, Action<StyleColor> set) {
        try {
            // First check if its a value color string
            Color color = ColorParser.ColorStringToColor(v);
            set(new StyleColor(color));
        } catch {
            // Second check if it was a keyword (auto, none, etc.)
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleColor)(object)k));

            // Third log an error
            if (!wasKeyword) {
                Logging.InvalidColorWarning(el, v);
            }
        }
    }

    private static void ApplyStyleLengthStyle(VisualElement el, string v, Action<StyleLength> set) {
        try {
            // First check if value length string
            StyleLength length = LengthParser.LengthStringToStyleLength(v);
            set(length);
        } catch {
            // Second check if it was a keyword
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleLength)(object)k));

            // Third log an error if not
            if (!wasKeyword) {
                Logging.InvalidLengthWarning(el, v);
            }
        }
    }

    private static void ApplyStyleFloatStyle(VisualElement el, string v, Action<StyleFloat> set) {
        try {
            StyleFloat length = LengthParser.LengthStringToStyleFloat(v);
            set(length);
        } catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set((StyleFloat)(object)k));
            if (!wasKeyword) {
                Logging.InvalidLengthWarning(el, v);
            }
        }
    }

    internal static void ApplyStyleAngleStyle(VisualElement el, string v, Action<StyleRotate> set) {
        try {
            Rotate rotate = AngleParser.AngleStringToRotate(v);
            set(new StyleRotate(rotate));
        } catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set(new StyleRotate((Rotate)(object)k)));
            if (!wasKeyword) {
                Logging.InvalidRotationWarning(el, v);
            }
        }
    }

    internal static void ApplyScaleStyle(VisualElement el, string v, Action<StyleScale> set) {
        try {
            Scale scale = ScaleParser.ScaleStringToScale(v);
            set(new StyleScale(scale));
        } catch {
            bool wasKeyword = ApplyIfKeyword(v, k => set(new StyleScale((Scale)(object)k)));
            if (!wasKeyword) {
                Logging.InvalidScaleWarning(el, v);
            }
        }
    }

    internal static void ApplyStyleInt(VisualElement el, string value, Action<StyleInt> set) {
        if (int.TryParse(value, out int intValue)) {
            set(new StyleInt(intValue));
        } else {
            Logging.InvalidIntValueWarning(el, value);
        }
    }

    /*
     *  Style appliers
     */

    #region Enum styles

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
        Debug.LogWarning("unity-background-scale-mode is deprecated. Use background-* properties instead.");
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
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
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
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
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
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyDirection(VisualElement el, string value, USSDirection direction) {
        try {
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
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
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
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
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
        if (ApplyIfKeyword(value, k => el.style.backgroundImage = k)) {
            return;
        }

        try {
            Texture2D texture = UrlParser.UrlStringToTexture2d(value);
            if (texture != null) {
                el.style.backgroundImage = new StyleBackground(texture);
            } else {
                Logging.InvalidUrlWarning(el, value);
            }
        } catch {
            Logging.InvalidUrlWarning(el, value);
        }
    }

    internal static void ApplyCursor(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.backgroundImage = k)) {
            return;
        }

        try {
            Texture2D cursorTexture = UrlParser.UrlStringToTexture2d(value);

            if (cursorTexture != null) {
                // Set the cursor with the loaded texture
                el.style.cursor = new StyleCursor(new Cursor() {
                    texture = cursorTexture,
                    hotspot = Vector2.zero // You can customize the hotspot as needed
                });
            } else {
                Logging.InvalidUrlWarning(el, value);
            }
        } catch {
            Logging.InvalidUrlWarning(el, value);
        }
    }

    internal static void ApplyBackgroundSize(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.backgroundImage = k)) {
            return;
        }

        try {
            Length[] sizes = LengthParser.LengthStringsToLengths(value);

            if (sizes.Length == 1) {
                el.style.backgroundSize = new BackgroundSize(sizes[0], sizes[0]);
            } else if (sizes.Length == 2) {
                el.style.backgroundSize = new BackgroundSize(sizes[0], sizes[1]);
            } else {
                Logging.InvalidLengthWarning(el, value);
            }
        } catch {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyBackgroundPosition(VisualElement el, string value, USSAxis axis) {
        if (ApplyIfKeyword(value, k => {
                if (axis == USSAxis.X || axis == USSAxis.All) {
                    el.style.backgroundPositionX = new StyleBackgroundPosition(k);
                }

                if (axis == USSAxis.Y || axis == USSAxis.All) {
                    el.style.backgroundPositionY = new StyleBackgroundPosition(k);
                }
            })) {
            return;
        }

        BackgroundPositionInfo[] positions = BackgroundPositionParser.Parse(value);

        foreach (BackgroundPositionInfo position in positions) {
            switch (position.Axis) {
                case USSAxis.X:
                    if (axis == USSAxis.X || axis == USSAxis.All) {
                        el.style.backgroundPositionX = new BackgroundPosition(position.Keyword, position.Offset);
                    }

                    break;
                case USSAxis.Y:
                    if (axis == USSAxis.Y || axis == USSAxis.All) {
                        el.style.backgroundPositionY = new BackgroundPosition(position.Keyword, position.Offset);
                    }

                    break;
                case USSAxis.All:
                    if (axis == USSAxis.All) {
                        el.style.backgroundPositionX = new BackgroundPosition(position.Keyword, position.Offset);
                        el.style.backgroundPositionY = new BackgroundPosition(position.Keyword, position.Offset);
                    }

                    break;
            }
        }
    }

    internal static void ApplyFlex(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => {
                el.style.flexGrow = k;
                el.style.flexShrink = k;
                el.style.flexBasis = k;
            })) {
            return;
        }

        try {
            FlexShorthand flexShorthand = FlexShorthandParser.ParseFlex(value);

            if (flexShorthand.Keyword.HasValue) {
                el.style.flexGrow = new StyleFloat(flexShorthand.Keyword.Value);
                el.style.flexShrink = new StyleFloat(flexShorthand.Keyword.Value);
                el.style.flexBasis = new StyleLength(flexShorthand.Keyword.Value);
            } else {
                el.style.flexGrow = new StyleFloat(flexShorthand.FlexGrow ?? 1);
                el.style.flexShrink = new StyleFloat(flexShorthand.FlexShrink ?? 1);
                el.style.flexBasis =
                    new StyleLength(flexShorthand.FlexBasis ?? new Length(0, LengthUnit.Pixel));
            }
        } catch {
            Logging.InvalidFlexlWarning(el, value);
        }
    }

    internal static void ApplyUnityTextOutline(VisualElement el, string value) {
        // Check for keywords first
        if (ApplyIfKeyword(value, k => {
                el.style.unityTextOutlineColor = k;
                el.style.unityTextOutlineWidth = k;
            })) {
            return;
        }

        try {
            OutlineShorthand outline = OutlineShorthandParser.ParseOutline(value);

            if (outline.Width.HasValue) {
                el.style.unityTextOutlineWidth = new StyleFloat(outline.Width.Value);
            }

            if (outline.Color.HasValue) {
                el.style.unityTextOutlineColor = new StyleColor(outline.Color.Value);
            }
        } catch (ArgumentException) {
            Logging.InvalidLengthWarning(el, value);
        }
    }

    internal static void ApplyTextShadow(VisualElement el, string value) {
        // Check for keywords first
        if (ApplyIfKeyword(value, k => el.style.textShadow = k)) {
            return;
        }

        try {
            // Inline parsing logic for UnityEngine.UIElements.TextShadow
            TextShadow parsedShadow = TextShadowParser.ParseTextShadow(value);
            UnityEngine.UIElements.TextShadow uiTextShadow = new() {
                offset = new Vector2(parsedShadow.OffsetX, parsedShadow.OffsetY),
                blurRadius = parsedShadow.BlurRadius,
                color = parsedShadow.Color
            };

            // Apply the parsed TextShadow value
            el.style.textShadow = new StyleTextShadow(uiTextShadow);
        } catch {
            Logging.InvalidValueWarning(el, value, "text-shadow");
        }
    }


    internal static void ApplyUnityFont(VisualElement el, string value, bool definition) {
        if (ApplyIfKeyword(value, k => el.style.unityFont = k)) {
            return;
        }

        try {
            string fontPath = null;

            if (value.StartsWith("url(") && value.EndsWith(")")) {
                fontPath = value.Substring(4, value.Length - 5).Trim();
            } else if (value.StartsWith("resource(") && value.EndsWith(")")) {
                fontPath = value.Substring(9, value.Length - 10).Trim();
            }

            if (fontPath != null) {
                Font font = Resources.Load<Font>(fontPath);

                if (font == null) {
                    Logging.InvalidValueWarning(el, value, "-unity-font");
                    return;
                }

                if (definition) {
                    el.style.unityFontDefinition = new StyleFontDefinition(font);
                } else {
                    el.style.unityFont = new StyleFont(font);
                    el.style.unityFontDefinition = StyleKeyword.None;
                }
            } else {
                Logging.InvalidValueWarning(el, value, "-unity-font");
            }
        } catch {
            Logging.InvalidValueWarning(el, value, "-unity-font");
        }
    }

    internal static void ApplyTransitionProperty(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.transitionProperty = k)) {
            return;
        }

        // Split by comma and trim each property name
        string[] propertyNames = value.Split(',');

        // Create a list of StylePropertyName
        List<StylePropertyName> properties = new();

        foreach (string propertyName in propertyNames) {
            properties.Add(new StylePropertyName(propertyName.Trim()));
        }

        // Apply the transition properties
        el.style.transitionProperty = new StyleList<StylePropertyName>(properties);
    }

    internal static void ApplyTransitionDuration(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.transitionDuration = k)) {
            return;
        }

        // Split by comma and trim each duration value
        string[] durationValues = value.Split(',');

        // Create a list of TimeValue
        List<TimeValue> durations = new();

        foreach (string durationValue in durationValues) {
            float durationInSeconds = TimeParser.ParseTime(durationValue.Trim());
            durations.Add(new TimeValue(durationInSeconds, TimeUnit.Second));
        }

        // Apply the transition durations
        el.style.transitionDuration = new StyleList<TimeValue>(durations);
    }

    internal static void ApplyTransitionTimingFunction(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.transitionTimingFunction = k)) {
            return;
        }

        // Split by comma and trim each timing function
        string[] timingFunctions = value.Split(',');

        // Create a list of EasingFunction
        List<EasingFunction> easingFunctions = new();

        foreach (string timingFunction in timingFunctions) {
            EasingMode mode = ParseEasingMode(timingFunction.Trim());
            easingFunctions.Add(new EasingFunction(mode));
        }

        // Apply the transition timing functions
        el.style.transitionTimingFunction = new StyleList<EasingFunction>(easingFunctions);
    }

    internal static void ApplyTransitionDelay(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.transitionDelay = k)) {
            return;
        }

        // Split by comma and trim each delay value
        string[] delayValues = value.Split(',');

        // Create a list of TimeValue
        List<TimeValue> delays = new();

        foreach (string delayValue in delayValues) {
            float delayInSeconds = TimeParser.ParseTime(delayValue.Trim());
            delays.Add(new TimeValue(delayInSeconds, TimeUnit.Second));
        }

        // Apply the transition delays
        el.style.transitionDelay = new StyleList<TimeValue>(delays);
    }

    internal static void ApplyTransition(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => {
                el.style.transitionProperty = k;
                el.style.transitionDuration = k;
                el.style.transitionTimingFunction = k;
                el.style.transitionDelay = k;
            })) {
            return;
        }

        // Split multiple transitions separated by commas
        string[] transitions = value.Split(',');

        // Temporary lists to hold values for all transitions
        List<StylePropertyName> properties = new();
        List<TimeValue> durations = new();
        List<EasingFunction> timingFunctions = new();
        List<TimeValue> delays = new();

        // Parse each transition definition
        foreach (string transition in transitions) {
            // Split components of each transition (property, duration, timing-function, delay, behavior)
            string[] components = transition.Trim().Split(' ');

            if (components.Length >= 1) {
                properties.Add(new StylePropertyName(components[0]));
            }

            if (components.Length >= 2) {
                float durationInSeconds = TimeParser.ParseTime(components[1]);
                durations.Add(new TimeValue(durationInSeconds, TimeUnit.Second));
            }

            if (components.Length >= 3) {
                // Check if the third component is an easing function or delay
                if (IsEasingFunction(components[2])) {
                    EasingMode mode = ParseEasingMode(components[2]);
                    timingFunctions.Add(new EasingFunction(mode));
                } else {
                    float delayInSeconds = TimeParser.ParseTime(components[2]);
                    delays.Add(new TimeValue(delayInSeconds, TimeUnit.Second));
                }
            }

            if (components.Length >= 4) {
                // If the third component was an easing function, the fourth is the delay
                if (timingFunctions.Count == delays.Count) {
                    float delayInSeconds = TimeParser.ParseTime(components[3]);
                    delays.Add(new TimeValue(delayInSeconds, TimeUnit.Second));
                }
            }
        }

        // Apply the parsed values
        el.style.transitionProperty = new StyleList<StylePropertyName>(properties);
        el.style.transitionDuration = new StyleList<TimeValue>(durations);
        el.style.transitionTimingFunction = new StyleList<EasingFunction>(timingFunctions);
        el.style.transitionDelay = new StyleList<TimeValue>(delays);
    }

    private static EasingMode ParseEasingMode(string timingFunction) {
        // Implement parsing logic for timing function keywords
        return timingFunction switch {
            "ease" => EasingMode.Ease,
            "ease-in" => EasingMode.EaseIn,
            "ease-out" => EasingMode.EaseOut,
            "ease-in-out" => EasingMode.EaseInOut,
            "linear" => EasingMode.Linear,
            // Add more mappings as needed
            _ => EasingMode.Linear // Default fallback
        };
    }

    private static bool IsEasingFunction(string value) {
        // List of easing function keywords
        string[] easingFunctions = {
            "ease", "ease-in", "ease-out", "ease-in-out", "linear",
            "ease-in-sine", "ease-out-sine", "ease-in-out-sine",
            "ease-in-cubic", "ease-out-cubic", "ease-in-out-cubic",
            "ease-in-circ", "ease-out-circ", "ease-in-out-circ",
            "ease-in-elastic", "ease-out-elastic", "ease-in-out-elastic",
            "ease-in-back", "ease-out-back", "ease-in-out-back",
            "ease-in-bounce", "ease-out-bounce", "ease-in-out-bounce"
        };
        return Array.Exists(easingFunctions, ef => ef == value);
    }


    internal static void ApplyTransformOrigin(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.transformOrigin = k)) {
            return;
        }

        try {
            TransformOrigin transformOrigin = TransformOriginParser.ParseTransformOrigin(value);
            el.style.transformOrigin = new StyleTransformOrigin(transformOrigin);
        } catch {
            Logging.InvalidValueWarning(el, value, "transform-origin");
        }
    }

    internal static void ApplyTranslate(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.translate = k)) {
            return;
        }

        try {
            Translate translate = TranslateParser.ParseTranslate(value);
            el.style.translate = new StyleTranslate(translate);
        } catch (Exception ex) {
            Debug.LogError($"Error applying translate: {ex.Message}");
        }
    }

    internal static void ApplyBackgroundRepeat(VisualElement el, string value) {
        if (ApplyIfKeyword(value, k => el.style.backgroundRepeat = k)) {
            return;
        }

        try {
            BackgroundRepeat backgroundRepeat = BackgroundRepeatParser.ParseBackgroundRepeat(value);
            el.style.backgroundRepeat = new StyleBackgroundRepeat(backgroundRepeat);
        } catch (Exception ex) {
            Debug.LogError($"Error applying background-repeat: {ex.Message}");
        }
    }

    #endregion
}