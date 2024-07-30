using System.Collections.Generic;
using _UITypes;
using UnityEngine;
using UnityEngine.UIElements;


internal static class Maps {
    /*
     *  Attribute enum mappings
     */

    internal static readonly Dictionary<string, ScrollView.TouchScrollBehavior> TouchScrollBehavior = new() {
        { "Elastic", ScrollView.TouchScrollBehavior.Elastic },
        { "Unrestricted", ScrollView.TouchScrollBehavior.Unrestricted },
        { "Clamped", ScrollView.TouchScrollBehavior.Clamped }
    };

    internal static readonly Dictionary<string, PickingMode> PickingMode = new() {
        { "Ignore", UnityEngine.UIElements.PickingMode.Ignore },
        { "Position", UnityEngine.UIElements.PickingMode.Position }
    };

    internal static readonly Dictionary<string, UsageHints> UsageHints = new() {
        { "DynamicTransform", UnityEngine.UIElements.UsageHints.DynamicTransform },
        { "GroupTransform", UnityEngine.UIElements.UsageHints.GroupTransform },
        { "MaskContainer", UnityEngine.UIElements.UsageHints.MaskContainer },
        { "DynamicColor", UnityEngine.UIElements.UsageHints.DynamicColor },
        { "None", UnityEngine.UIElements.UsageHints.None }
    };

    internal static readonly Dictionary<string, ScrollerVisibility> VerticalScrollerVisibility = new() {
        { "Hidden", ScrollerVisibility.Hidden },
        { "AlwaysVisible", ScrollerVisibility.AlwaysVisible },
        { "Auto", ScrollerVisibility.Auto }
    };

    internal static readonly Dictionary<string, ScrollerVisibility> HorizontalScrollerVisibility = new() {
        { "Hidden", ScrollerVisibility.Hidden },
        { "AlwaysVisible", ScrollerVisibility.AlwaysVisible },
        { "Auto", ScrollerVisibility.Auto }
    };

    internal static readonly Dictionary<string, ScrollView.NestedInteractionKind> NestedInteractionKind = new() {
        { "ForwardScrolling", ScrollView.NestedInteractionKind.ForwardScrolling },
        { "StopScrolling", ScrollView.NestedInteractionKind.StopScrolling },
        { "Default", ScrollView.NestedInteractionKind.Default }
    };

    internal static readonly Dictionary<string, ScrollViewMode> ScrollViewMode = new() {
        { "Horizontal", UnityEngine.UIElements.ScrollViewMode.Horizontal },
        { "VerticalAndHorizontal", UnityEngine.UIElements.ScrollViewMode.VerticalAndHorizontal },
        { "Vertical", UnityEngine.UIElements.ScrollViewMode.Vertical }
    };

    internal static readonly Dictionary<string, CollectionVirtualizationMethod> CollectionVirtualizationMethod =
        new() {
            { "DynamicHeight", UnityEngine.UIElements.CollectionVirtualizationMethod.DynamicHeight },
            { "FixedHeight", UnityEngine.UIElements.CollectionVirtualizationMethod.FixedHeight }
        };

    internal static readonly Dictionary<string, SelectionType> SelectionType = new() {
        { "Multiple", UnityEngine.UIElements.SelectionType.Multiple },
        { "Single", UnityEngine.UIElements.SelectionType.Single },
        { "None", UnityEngine.UIElements.SelectionType.None }
    };

    internal static readonly Dictionary<string, AlternatingRowBackground> AlternatingRowBackground = new() {
        { "All", UnityEngine.UIElements.AlternatingRowBackground.All },
        { "None", UnityEngine.UIElements.AlternatingRowBackground.None },
        { "ContentOnly", UnityEngine.UIElements.AlternatingRowBackground.ContentOnly }
    };

    internal static readonly Dictionary<string, ListViewReorderMode> ListViewReorderMode = new() {
        { "Simple", UnityEngine.UIElements.ListViewReorderMode.Simple },
        { "Animated", UnityEngine.UIElements.ListViewReorderMode.Animated }
    };

    internal static readonly Dictionary<string, SliderDirection> SliderDirection = new() {
        { "Vertical", UnityEngine.UIElements.SliderDirection.Vertical },
        { "Horizontal", UnityEngine.UIElements.SliderDirection.Horizontal }
    };

    internal static readonly Dictionary<string, TouchScreenKeyboardType> TouchScreenKeyboardType = new() {
        { "Default", UnityEngine.TouchScreenKeyboardType.Default },
        { "ASCIICapable", UnityEngine.TouchScreenKeyboardType.ASCIICapable },
        { "NumbersAndPunctuation", UnityEngine.TouchScreenKeyboardType.NumbersAndPunctuation },
        { "URL", UnityEngine.TouchScreenKeyboardType.URL },
        { "NumberPad", UnityEngine.TouchScreenKeyboardType.NumberPad },
        { "PhonePad", UnityEngine.TouchScreenKeyboardType.PhonePad },
        { "NamePhonePad", UnityEngine.TouchScreenKeyboardType.NamePhonePad },
        { "EmailAddress", UnityEngine.TouchScreenKeyboardType.EmailAddress },
        { "Social", UnityEngine.TouchScreenKeyboardType.Social },
        { "Search", UnityEngine.TouchScreenKeyboardType.Search },
        { "DecimalPad", UnityEngine.TouchScreenKeyboardType.DecimalPad },
        { "OneTimeCode", UnityEngine.TouchScreenKeyboardType.OneTimeCode }
    };

    internal static readonly Dictionary<string, StyleProperty> StyleProperties = new() {
        { "align-content", StyleProperty.AlignContent },
        { "align-items", StyleProperty.AlignItems },
        { "align-self", StyleProperty.AlignSelf },
        { "background-color", StyleProperty.BackgroundColor },
        { "background-image", StyleProperty.BackgroundImage },
        { "background-position", StyleProperty.BackgroundPosition },
        { "background-position-x", StyleProperty.BackgroundPositionX },
        { "background-position-y", StyleProperty.BackgroundPositionY },
        { "background-repeat", StyleProperty.BackgroundRepeat },
        { "background-size", StyleProperty.BackgroundSize },
        { "border-bottom-color", StyleProperty.BorderBottomColor },
        { "border-bottom-left-radius", StyleProperty.BorderBottomLeftRadius },
        { "border-bottom-right-radius", StyleProperty.BorderBottomRightRadius },
        { "border-bottom-width", StyleProperty.BorderBottomWidth },
        { "border-color", StyleProperty.BorderColor },
        { "border-left-color", StyleProperty.BorderLeftColor },
        { "border-left-width", StyleProperty.BorderLeftWidth },
        { "border-radius", StyleProperty.BorderRadius },
        { "border-right-color", StyleProperty.BorderRightColor },
        { "border-right-width", StyleProperty.BorderRightWidth },
        { "border-top-color", StyleProperty.BorderTopColor },
        { "border-top-left-radius", StyleProperty.BorderTopLeftRadius },
        { "border-top-right-radius", StyleProperty.BorderTopRightRadius },
        { "border-top-width", StyleProperty.BorderTopWidth },
        { "border-width", StyleProperty.BorderWidth },
        { "bottom", StyleProperty.Bottom },
        { "color", StyleProperty.Color },
        { "cursor", StyleProperty.Cursor },
        { "display", StyleProperty.Display },
        { "flex", StyleProperty.Flex },
        { "flex-basis", StyleProperty.FlexBasis },
        { "flex-direction", StyleProperty.FlexDirection },
        { "flex-grow", StyleProperty.FlexGrow },
        { "flex-shrink", StyleProperty.FlexShrink },
        { "flex-wrap", StyleProperty.FlexWrap },
        { "font-size", StyleProperty.FontSize },
        { "height", StyleProperty.Height },
        { "justify-content", StyleProperty.JustifyContent },
        { "left", StyleProperty.Left },
        { "letter-spacing", StyleProperty.LetterSpacing },
        { "margin", StyleProperty.Margin },
        { "margin-bottom", StyleProperty.MarginBottom },
        { "margin-left", StyleProperty.MarginLeft },
        { "margin-right", StyleProperty.MarginRight },
        { "margin-top", StyleProperty.MarginTop },
        { "max-height", StyleProperty.MaxHeight },
        { "max-width", StyleProperty.MaxWidth },
        { "min-height", StyleProperty.MinHeight },
        { "min-width", StyleProperty.MinWidth },
        { "opacity", StyleProperty.Opacity },
        { "overflow", StyleProperty.Overflow },
        { "padding", StyleProperty.Padding },
        { "padding-bottom", StyleProperty.PaddingBottom },
        { "padding-left", StyleProperty.PaddingLeft },
        { "padding-right", StyleProperty.PaddingRight },
        { "padding-top", StyleProperty.PaddingTop },
        { "position", StyleProperty.Position },
        { "right", StyleProperty.Right },
        { "rotate", StyleProperty.Rotate },
        { "scale", StyleProperty.Scale },
        { "text-overflow", StyleProperty.TextOverflow },
        { "text-shadow", StyleProperty.TextShadow },
        { "top", StyleProperty.Top },
        { "transform-origin", StyleProperty.TransformOrigin },
        { "transition", StyleProperty.Transition },
        { "transition-delay", StyleProperty.TransitionDelay },
        { "transition-duration", StyleProperty.TransitionDuration },
        { "transition-property", StyleProperty.TransitionProperty },
        { "transition-timing-function", StyleProperty.TransitionTimingFunction },
        { "translate", StyleProperty.Translate },
        { "-unity-background-image-tint-color", StyleProperty.UnityBackgroundImageTintColor },
        { "-unity-background-scale-mode", StyleProperty.UnityBackgroundScaleMode },
        { "-unity-font", StyleProperty.UnityFont },
        { "-unity-font-definition", StyleProperty.UnityFontDefinition },
        { "-unity-font-style", StyleProperty.UnityFontStyle },
        { "-unity-overflow-clip-box", StyleProperty.UnityOverflowClipBox },
        { "-unity-paragraph-spacing", StyleProperty.UnityParagraphSpacing },
        { "-unity-slice-bottom", StyleProperty.UnitySliceBottom },
        { "-unity-slice-left", StyleProperty.UnitySliceLeft },
        { "-unity-slice-right", StyleProperty.UnitySliceRight },
        { "-unity-slice-scale", StyleProperty.UnitySliceScale },
        { "-unity-slice-top", StyleProperty.UnitySliceTop },
        { "-unity-text-align", StyleProperty.UnityTextAlign },
        { "-unity-text-outline", StyleProperty.UnityTextOutline },
        { "-unity-text-outline-color", StyleProperty.UnityTextOutlineColor },
        { "-unity-text-outline-width", StyleProperty.UnityTextOutlineWidth },
        { "-unity-text-overflow-position", StyleProperty.UnityTextOverflowPosition },
        { "visibility", StyleProperty.Visibility },
        { "white-space", StyleProperty.WhiteSpace },
        { "width", StyleProperty.Width },
        { "word-spacing", StyleProperty.WordSpacing }
    };

    internal static readonly Dictionary<string, string> USSColors = new() {
        { "aliceblue", "#F0F8FF" },
        { "antiquewhite", "#FAEBD7" },
        { "aqua", "#00FFFF" },
        { "aquamarine", "#7FFFD4" },
        { "azure", "#F0FFFF" },
        { "beige", "#F5F5DC" },
        { "bisque", "#FFE4C4" },
        { "black", "#000000" },
        { "blanchedalmond", "#FFEBCD" },
        { "blue", "#0000FF" },
        { "blueviolet", "#8A2BE2" },
        { "brown", "#A52A2A" },
        { "burlywood", "#DEB887" },
        { "cadetblue", "#5F9EA0" },
        { "chartreuse", "#7FFF00" },
        { "chocolate", "#D2691E" },
        { "coral", "#FF7F50" },
        { "cornflowerblue", "#6495ED" },
        { "cornsilk", "#FFF8DC" },
        { "crimson", "#DC143C" },
        { "cyan", "#00FFFF" },
        { "darkblue", "#00008B" },
        { "darkcyan", "#008B8B" },
        { "darkgoldenrod", "#B8860B" },
        { "darkgray", "#A9A9A9" },
        { "darkgreen", "#006400" },
        { "darkkhaki", "#BDB76B" },
        { "darkmagenta", "#8B008B" },
        { "darkolivegreen", "#556B2F" },
        { "darkorange", "#FF8C00" },
        { "darkorchid", "#9932CC" },
        { "darkred", "#8B0000" },
        { "darksalmon", "#E9967A" },
        { "darkseagreen", "#8FBC8F" },
        { "darkslateblue", "#483D8B" },
        { "darkslategray", "#2F4F4F" },
        { "darkturquoise", "#00CED1" },
        { "darkviolet", "#9400D3" },
        { "deeppink", "#FF1493" },
        { "deepskyblue", "#00BFFF" },
        { "dimgray", "#696969" },
        { "dodgerblue", "#1E90FF" },
        { "firebrick", "#B22222" },
        { "floralwhite", "#FFFAF0" },
        { "forestgreen", "#228B22" },
        { "fuchsia", "#FF00FF" },
        { "gainsboro", "#DCDCDC" },
        { "ghostwhite", "#F8F8FF" },
        { "gold", "#FFD700" },
        { "goldenrod", "#DAA520" },
        { "gray", "#808080" },
        { "green", "#008000" },
        { "greenyellow", "#ADFF2F" },
        { "honeydew", "#F0FFF0" },
        { "hotpink", "#FF69B4" },
        { "indianred", "#CD5C5C" },
        { "indigo", "#4B0082" },
        { "ivory", "#FFFFF0" },
        { "khaki", "#F0E68C" },
        { "lavender", "#E6E6FA" },
        { "lavenderblush", "#FFF0F5" },
        { "lawngreen", "#7CFC00" },
        { "lemonchiffon", "#FFFACD" },
        { "lightblue", "#ADD8E6" },
        { "lightcoral", "#F08080" },
        { "lightcyan", "#E0FFFF" },
        { "lightgoldenrodyellow", "#FAFAD2" },
        { "lightgray", "#D3D3D3" },
        { "lightgreen", "#90EE90" },
        { "lightpink", "#FFB6C1" },
        { "lightsalmon", "#FFA07A" },
        { "lightseagreen", "#20B2AA" },
        { "lightskyblue", "#87CEFA" },
        { "lightslategray", "#778899" },
        { "lightsteelblue", "#B0C4DE" },
        { "lightyellow", "#FFFFE0" },
        { "lime", "#00FF00" },
        { "limegreen", "#32CD32" },
        { "linen", "#FAF0E6" },
        { "magenta", "#FF00FF" },
        { "maroon", "#800000" },
        { "mediumaquamarine", "#66CDAA" },
        { "mediumblue", "#0000CD" },
        { "mediumorchid", "#BA55D3" },
        { "mediumpurple", "#9370DB" },
        { "mediumseagreen", "#3CB371" },
        { "mediumslateblue", "#7B68EE" },
        { "mediumspringgreen", "#00FA9A" },
        { "mediumturquoise", "#48D1CC" },
        { "mediumvioletred", "#C71585" },
        { "midnightblue", "#191970" },
        { "mintcream", "#F5FFFA" },
        { "mistyrose", "#FFE4E1" },
        { "moccasin", "#FFE4B5" },
        { "navajowhite", "#FFDEAD" },
        { "navy", "#000080" },
        { "oldlace", "#FDF5E6" },
        { "olive", "#808000" },
        { "olivedrab", "#6B8E23" },
        { "orange", "#FFA500" },
        { "orangered", "#FF4500" },
        { "orchid", "#DA70D6" },
        { "palegoldenrod", "#EEE8AA" },
        { "palegreen", "#98FB98" },
        { "paleturquoise", "#AFEEEE" },
        { "palevioletred", "#DB7093" },
        { "papayawhip", "#FFEFD5" },
        { "peachpuff", "#FFDAB9" },
        { "peru", "#CD853F" },
        { "pink", "#FFC0CB" },
        { "plum", "#DDA0DD" },
        { "powderblue", "#B0E0E6" },
        { "purple", "#800080" },
        { "rebeccapurple", "#663399" },
        { "red", "#FF0000" },
        { "rosybrown", "#BC8F8F" },
        { "royalblue", "#4169E1" },
        { "saddlebrown", "#8B4513" },
        { "salmon", "#FA8072" },
        { "sandybrown", "#F4A460" },
        { "seagreen", "#2E8B57" },
        { "seashell", "#FFF5EE" },
        { "sienna", "#A0522D" },
        { "silver", "#C0C0C0" },
        { "skyblue", "#87CEEB" },
        { "slateblue", "#6A5ACD" },
        { "slategray", "#708090" },
        { "snow", "#FFFAFA" },
        { "springgreen", "#00FF7F" },
        { "steelblue", "#4682B4" },
        { "tan", "#D2B48C" },
        { "teal", "#008080" },
        { "thistle", "#D8BFD8" },
        { "tomato", "#FF6347" },
        { "turquoise", "#40E0D0" },
        { "violet", "#EE82EE" },
        { "wheat", "#F5DEB3" },
        { "white", "#FFFFFF" },
        { "whitesmoke", "#F5F5F5" },
        { "yellow", "#FFFF00" },
        { "yellowgreen", "#9ACD32" }
    };
}