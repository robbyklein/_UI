using System.Collections.Generic;
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
}