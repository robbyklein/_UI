using System.Collections.Generic;
using UIBuddyTypes;
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
        { "all", StyleProperty.All },
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
}