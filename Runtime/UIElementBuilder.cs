using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIElementBuilder {
    /*
     *  Enum mappings
     */

    private static readonly Dictionary<string, ScrollView.TouchScrollBehavior> TouchScrollBehaviorMap = new() {
        { "Elastic", ScrollView.TouchScrollBehavior.Elastic },
        { "Unrestricted", ScrollView.TouchScrollBehavior.Unrestricted },
        { "Clamped", ScrollView.TouchScrollBehavior.Clamped }
    };

    private static readonly Dictionary<string, PickingMode> PickingModeMap = new() {
        { "Ignore", PickingMode.Ignore },
        { "Position", PickingMode.Position }
    };

    private static readonly Dictionary<string, UsageHints> UsageHintsMap = new() {
        { "DynamicTransform", UsageHints.DynamicTransform },
        { "GroupTransform", UsageHints.GroupTransform },
        { "MaskContainer", UsageHints.MaskContainer },
        { "DynamicColor", UsageHints.DynamicColor },
        { "None", UsageHints.None }
    };

    private static readonly Dictionary<string, ScrollerVisibility> VerticalScrollerVisibilityMap = new() {
        { "Hidden", ScrollerVisibility.Hidden },
        { "AlwaysVisible", ScrollerVisibility.AlwaysVisible },
        { "Auto", ScrollerVisibility.Auto }
    };

    private static readonly Dictionary<string, ScrollerVisibility> HorizontalScrollerVisibilityMap = new() {
        { "Hidden", ScrollerVisibility.Hidden },
        { "AlwaysVisible", ScrollerVisibility.AlwaysVisible },
        { "Auto", ScrollerVisibility.Auto }
    };

    private static readonly Dictionary<string, ScrollView.NestedInteractionKind> NestedInteractionKindMap = new() {
        { "ForwardScrolling", ScrollView.NestedInteractionKind.ForwardScrolling },
        { "StopScrolling", ScrollView.NestedInteractionKind.StopScrolling },
        { "Default", ScrollView.NestedInteractionKind.Default }
    };

    private static readonly Dictionary<string, ScrollViewMode> ScrollViewModeMap = new() {
        { "Horizontal", ScrollViewMode.Horizontal },
        { "VerticalAndHorizontal", ScrollViewMode.VerticalAndHorizontal },
        { "Vertical", ScrollViewMode.Vertical }
    };

    private static readonly Dictionary<string, CollectionVirtualizationMethod> CollectionVirtualizationMethodMap = new() {
        { "DynamicHeight", CollectionVirtualizationMethod.DynamicHeight },
        { "FixedHeight", CollectionVirtualizationMethod.FixedHeight }
    };

    private static readonly Dictionary<string, SelectionType> SelectionTypeMap = new() {
        { "Multiple", SelectionType.Multiple },
        { "Single", SelectionType.Single },
        { "None", SelectionType.None }
    };

    private static readonly Dictionary<string, AlternatingRowBackground> AlternatingRowBackgroundMap = new() {
        { "All", AlternatingRowBackground.All },
        { "None", AlternatingRowBackground.None },
        { "ContentOnly", AlternatingRowBackground.ContentOnly }
    };

    private static readonly Dictionary<string, ListViewReorderMode> ListViewReorderModeMap = new() {
        { "Simple", ListViewReorderMode.Simple },
        { "Animated", ListViewReorderMode.Animated }
    };

    /*
     * Public API
     */

    public static T Build<T>(string uiString) where T : VisualElement {
        // Create an xml document
        XmlDocument doc = new();

        // Wrap with UXML element so it has ui namespace
        doc.LoadXml("<ui:UXML xmlns:ui=\"UnityEngine.UIElements\">" + uiString + "</ui:UXML>");

        // Since we wrapped, the first child is root
        XmlNode root = doc.DocumentElement.FirstChild; // Get the first child of the wrapper

        // Return the created element
        return CreateElement<T>(root);
    }

    /*
     * Element Building
     */

    private static T CreateElement<T>(XmlNode node) where T : VisualElement {
        // Step 1: Create base element
        T element = CreateElementBase<T>(node);

        // Step 2: Add attributes
        if (element != null && node.Attributes != null) {
            foreach (XmlAttribute attribute in node.Attributes) {
                AddAttr(element, attribute);
            }
        }

        // Step 3: Build nested elements recursively
        if (element != null && node.HasChildNodes) {
            foreach (XmlNode childNode in node.ChildNodes) {
                VisualElement childElement = CreateElement<VisualElement>(childNode);

                if (childElement != null) {
                    element.Add(childElement);
                }
            }
        }

        // Step 4: Return the finished root element
        if (element is T typedElement) {
            return typedElement;
        }

        throw new InvalidOperationException($"The created element is not of type {typeof(T).Name}");
    }

    private static T CreateElementBase<T>(XmlNode node) where T : VisualElement {
        VisualElement element = node.LocalName switch {
            "VisualElement" => new VisualElement(),
            "ScrollView" => new ScrollView(),
            "ListView" => new ListView(),
            "TreeView" => new TreeView(),
            "GroupBox" => new GroupBox(),
            "Label" => new Label(),
            "Button" => new Button(),
            "Toggle" => new Toggle(),
            "Scroller" => new Scroller(),
            "TextField" => new TextField(),
            "Foldout" => new Foldout(),
            "Slider" => new Slider(),
            "SliderInt" => new SliderInt(),
            "MinMaxSlider" => new MinMaxSlider(),
            "ProgressBar" => new ProgressBar(),
            "DropdownField" => new DropdownField(),
            "EnumField" => new EnumField(),
            "RadioButton" => new RadioButton(),
            "RadioButtonGroup" => new RadioButtonGroup(),
            "IntegerField" => new IntegerField(),
            "FloatField" => new FloatField(),
            "LongField" => new LongField(),
            "DoubleField" => new DoubleField(),
            "Hash128Field" => new Hash128Field(),
            "Vector2Field" => new Vector2Field(),
            "Vector3Field" => new Vector3Field(),
            "Vector4Field" => new Vector4Field(),
            "RectField" => new RectField(),
            "BoundsField" => new BoundsField(),
            "UnsignedIntegerField" => new UnsignedIntegerField(),
            "UnsignedLongField" => new UnsignedLongField(),
            "Vector2IntField" => new Vector2IntField(),
            "Vector3IntField" => new Vector3IntField(),
            "RectIntField" => new RectIntField(),
            "BoundsIntField" => new BoundsIntField(),
            _ => throw new InvalidOperationException($"Cannot create element for type {node.LocalName}.")
        };

        if (element is T typedElement) {
            return typedElement;
        }

        throw new InvalidOperationException(
            $"The created element is not of type {typeof(T).Name}. Element type is {element.GetType().Name}.");
    }

    private static void AddAttr<T>(T el, XmlAttribute attr) where T : VisualElement {
        switch (attr.Name) {
            case "class":
                AddClassAttr(el, attr);
                return;
            case "name":
                el.name = attr.Value;
                return;
            case "tooltip":
                el.tooltip = attr.Value;
                return;
            case "view-data-key":
                el.viewDataKey = attr.Value;
                return;
            case "focusable":
                SetBooleanAttr(el, attr, (e, v) => e.focusable = v);
                return;
            case "visible":
                SetBooleanAttr(el, attr, (e, v) => e.visible = v);
                return;
            case "delegates-focus":
                SetBooleanAttr(el, attr, (e, v) => e.delegatesFocus = v);
                return;
            case "picking-mode":
                SetEnumAttr(el, attr, PickingModeMap, (e, v) => e.pickingMode = v);
                return;
            case "usage-hints":
                SetEnumAttr(el, attr, UsageHintsMap, (e, v) => e.usageHints = v);
                return;
            case "tabindex":
                SetIntAttr(el, attr, (e, v) => e.tabIndex = v);
                return;
            case "binding-path":
                AddBindingPathAttr(el, attr);
                return;
            case "text":
                AddTextAttr(el, attr);
                return;
            case "parse-escape-sequences":
                AddParseEscapeSequencesAttr(el, attr);
                return;
            case "display-tooltip-when-elided":
                AddDisplayTooltipWhenElidedAttr(el, attr);
                return;
            case "enable-rich-text":
                AddEnableRichTextAttr(el, attr);
                return;
            case "mode":
                AddModeAttr(el, attr);
                return;
            case "horizontal-scroller-visibility":
                AddHorizontalScrollerVisibilityAttr(el, attr);
                return;
            case "nested-interaction-kind":
                AddNestedInteractionKindAttr(el, attr);
                return;
            case "vertical-scroller-visibility":
                AddVerticalScrollerVisibilityAttr(el, attr);
                return;
            case "horizontal-page-size":
                AddVerticalPageSizeAttr(el, attr);
                return;
            case "vertical-page-size":
                AddHorizontalPageSizeAttr(el, attr);
                return;
            case "mouse-wheel-scroll-size":
                AddMouseWheelScrollSizeAttr(el, attr);
                return;
            case "touch-scroll-type":
                AddTouchScrollTypeAttr(el, attr);
                return;
            case "scroll-deceleration-rate":
                AddScrollDecelerationRateAttr(el, attr);
                return;
            case "elasticity":
                AddElasticityAttr(el, attr);
                return;
            case "elastic-animation-interval-ms":
                AddElasticityAnimationIntervalMsAttr(el, attr);
                return;
            case "show-border":
                AddShowBorderAttr(el, attr);
                return;
            case "selection-type":
                AddSelectionTypeAttr(el, attr);
                return;
            case "show-alternating-row-backgrounds":
                AddShowAlternatingRowBackgroundsAttr(el, attr);
                return;
            case "reorderable":
                AddReorderableAttr(el, attr);
                return;
            case "horizontal-scrolling":
                AddHorizontalScrollingAttr(el, attr);
                return;
            case "show-foldout-header":
                AddShowFoldoutHeaderAttr(el, attr);
                return;
            case "header-title":
                AddHeaderTitleAttr(el, attr);
                return;
            case "show-add-remove-footer":
                AddShowAddRemoveFooterAttr(el, attr);
                return;
            case "reorder-mode":
                AddReorderModeAttr(el, attr);
                return;
            case "show-bound-collection-size":
                AddShowBoundCollectionSizeAttr(el, attr);
                return;
            case "fixed-item-height":
                AddFixedItemHeightAttr(el, attr);
                return;
            case "virtualization-method":
                AddVirtualizationMethodAttr(el, attr);
                return;
            case "auto-expand":
                AddAutoExpandAttr(el, attr);
                return;
            case "label":
                AddLabelAttr(el, attr);
                return;
        }


        // If we make it all the way here it's an unknown or unsupported attribute
        AttributeNameWarning(el, attr);
    }

    /*
     * Attribute Setters
     */

    private static void SetBooleanAttr<T>(T el, XmlAttribute attr, Action<T, bool> add) where T : VisualElement {
        if (bool.TryParse(attr.Value, out bool boolean)) {
            add(el, boolean);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    private static void SetIntAttr<T>(T el, XmlAttribute attr, Action<T, int> add) where T : VisualElement {
        if (int.TryParse(attr.Value, out int integer)) {
            add(el, integer);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    private static void SetFloatAttr<T>(T el, XmlAttribute attr, Action<T, float> add) where T : VisualElement {
        if (float.TryParse(attr.Value, out float floatNum)) {
            add(el, floatNum);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    private static void SetLongAttr<T>(T el, XmlAttribute attr, Action<T, long> add) where T : VisualElement {
        if (long.TryParse(attr.Value, out long longNum)) {
            add(el, longNum);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    private static void SetEnumAttr<T, T2>(T el, XmlAttribute attr, Dictionary<string, T2> map, Action<T, T2> add)
        where T : VisualElement {
        if (map.TryGetValue(attr.Value, out T2 enumValue)) {
            add(el, enumValue);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    /*
     * Specialized attribute setters
     */

    private static void AddClassAttr<T>(T el, XmlAttribute attr) where T : VisualElement {
        string[] classes = attr.Value.Split(" ");

        foreach (string cls in classes) {
            el.AddToClassList(cls);
        }
    }

    private static void AddParseEscapeSequencesAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                SetBooleanAttr(el, attr, (e, v) => e.parseEscapeSequences = v);
                break;
            case Button el:
                SetBooleanAttr(el, attr, (e, v) => e.parseEscapeSequences = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddEnableRichTextAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                SetBooleanAttr(el, attr, (e, v) => e.enableRichText = v);
                break;
            case Button el:
                SetBooleanAttr(el, attr, (e, v) => e.enableRichText = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddDisplayTooltipWhenElidedAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                SetBooleanAttr(el, attr, (e, v) => e.displayTooltipWhenElided = v);
                break;
            case Button el:
                SetBooleanAttr(el, attr, (e, v) => e.displayTooltipWhenElided = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMouseWheelScrollSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => e.mouseWheelScrollSize = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddTouchScrollTypeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetEnumAttr(el, attr, TouchScrollBehaviorMap, (e, v) => el.touchScrollBehavior = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalPageSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => el.horizontalPageSize = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVerticalPageSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => e.verticalPageSize = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVerticalScrollerVisibilityAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetEnumAttr(el, attr, VerticalScrollerVisibilityMap, (e, v) => e.verticalScrollerVisibility = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalScrollerVisibilityAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetEnumAttr(el, attr, HorizontalScrollerVisibilityMap, (e, v) => el.horizontalScrollerVisibility = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddNestedInteractionKindAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetEnumAttr(el, attr, NestedInteractionKindMap, (e, v) => e.nestedInteractionKind = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddModeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetEnumAttr(el, attr, ScrollViewModeMap, (e, v) => e.mode = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddBindingPathAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                el.bindingPath = attr.Value;
                break;
            case ListView el:
                el.bindingPath = attr.Value;
                break;
            case TreeView el:
                el.bindingPath = attr.Value;
                break;
            case GroupBox el:
                el.bindingPath = attr.Value;
                break;
            case Button el:
                el.bindingPath = attr.Value;
                break;
            case Toggle el:
                el.bindingPath = attr.Value;
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddTextAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                el.text = attr.Value;
                break;
            case GroupBox el:
                el.text = attr.Value;
                break;
            case Button el:
                el.text = attr.Value;
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddScrollDecelerationRateAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => e.scrollDecelerationRate = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddElasticityAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => e.elasticity = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddElasticityAnimationIntervalMsAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetLongAttr(el, attr, (e, v) => e.elasticAnimationIntervalMs = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVirtualizationMethodAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetEnumAttr(el, attr, CollectionVirtualizationMethodMap, (e, v) => e.virtualizationMethod = v);
                break;
            case TreeView el:
                SetEnumAttr(el, attr, CollectionVirtualizationMethodMap, (e, v) => e.virtualizationMethod = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowBorderAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.showBorder = v);
                break;
            case TreeView el:
                SetBooleanAttr(el, attr, (e, v) => e.showBorder = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddSelectionTypeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetEnumAttr(el, attr, SelectionTypeMap, (e, v) => e.selectionType = v);
                break;
            case TreeView el:
                SetEnumAttr(el, attr, SelectionTypeMap, (e, v) => e.selectionType = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowAlternatingRowBackgroundsAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetEnumAttr(el, attr, AlternatingRowBackgroundMap, (e, v) => e.showAlternatingRowBackgrounds = v);
                break;
            case TreeView el:
                SetEnumAttr(el, attr, AlternatingRowBackgroundMap, (e, v) => e.showAlternatingRowBackgrounds = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReorderableAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.reorderable = v);
                break;
            case TreeView el:
                SetBooleanAttr(el, attr, (e, v) => e.reorderable = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalScrollingAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.horizontalScrollingEnabled = v);
                break;
            case TreeView el:
                SetBooleanAttr(el, attr, (e, v) => e.horizontalScrollingEnabled = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowFoldoutHeaderAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.showFoldoutHeader = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHeaderTitleAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                el.headerTitle = attr.Value;
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowAddRemoveFooterAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.showAddRemoveFooter = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReorderModeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetEnumAttr(el, attr, ListViewReorderModeMap, (e, v) => e.reorderMode = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowBoundCollectionSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetBooleanAttr(el, attr, (e, v) => e.showBoundCollectionSize = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddFixedItemHeightAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                SetFloatAttr(el, attr, (e, v) => e.fixedItemHeight = v);
                break;
            case TreeView el:
                SetFloatAttr(el, attr, (e, v) => e.fixedItemHeight = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }


    private static void AddAutoExpandAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TreeView el:
                SetBooleanAttr(el, attr, (e, v) => e.autoExpand = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLabelAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Toggle el:
                el.label = attr.Value;
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }


    /*
     * Warning Logs
     */

    private static string GetElementName(VisualElement element) {
        return string.IsNullOrEmpty(element.name) ? "Unnamed element" : element.name;
    }

    public static void AttributeNameWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute name on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    public static void AttributeValueWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute value on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    public static void AttributeUnsupportedWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Unsupported attribute on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }
}