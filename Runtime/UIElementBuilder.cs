using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    private static readonly Dictionary<string, SliderDirection> SliderDirectionMap = new() {
        { "Vertical", SliderDirection.Vertical },
        { "Horizontal", SliderDirection.Horizontal }
    };

    private static readonly Dictionary<string, TouchScreenKeyboardType> TouchScreenKeyboardTypeMap = new() {
        { "Default", TouchScreenKeyboardType.Default },
        { "ASCIICapable", TouchScreenKeyboardType.ASCIICapable },
        { "NumbersAndPunctuation", TouchScreenKeyboardType.NumbersAndPunctuation },
        { "URL", TouchScreenKeyboardType.URL },
        { "NumberPad", TouchScreenKeyboardType.NumberPad },
        { "PhonePad", TouchScreenKeyboardType.PhonePad },
        { "NamePhonePad", TouchScreenKeyboardType.NamePhonePad },
        { "EmailAddress", TouchScreenKeyboardType.EmailAddress },
        { "Social", TouchScreenKeyboardType.Social },
        { "Search", TouchScreenKeyboardType.Search },
        { "DecimalPad", TouchScreenKeyboardType.DecimalPad },
        { "OneTimeCode", TouchScreenKeyboardType.OneTimeCode }
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
        if (doc.DocumentElement != null) {
            XmlNode root = doc.DocumentElement.FirstChild;
            return CreateElement<T>(root);
        }

        throw new InvalidOperationException("The XML document does not have a valid root element.");
    }

    public static Dictionary<string, string> ParseUSS(string ussString) {
        Dictionary<string, string> styles = new();
        // "font-size: 200px; color: #ffffff; background-size: 40px;"

        // Split into properties
        string[] properties = ussString.Split(";");
        
        // Loop them
        foreach (string property in properties) {
            // Trim extra white space
            var trimmedProperty = property.Trim();
            
            // Make sure we actually have a property to work with
            if (string.IsNullOrEmpty(trimmedProperty)) {
                continue;
            }

            // Split by : giving us a key and value
            var keyValue = trimmedProperty.Split(new[] { ':' }, 2);
            
            //  it's not exactly two somethings invalid
            if (keyValue.Length != 2) {
                // TODO: log an error here
                continue;
            }
            
            // Remove any excess whitespace
            var key = keyValue[0].Trim();
            var value = keyValue[1].Trim();

            // Add to the dictionary
            styles[key] = value;
        }

        return styles;
    }
    
    /*
     * Element Building
     */

    private static T CreateElement<T>(XmlNode node) where T : VisualElement {
        // Step 1: Create base element
        T element = CreateElementBase<T>(node);

        // Step 2: Add attributes (ex. style)
        if (element != null && node.Attributes != null) {
            foreach (XmlAttribute attribute in node.Attributes) {
                AddAttr(element, attribute, node);
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
        return element ?? throw new InvalidOperationException($"The created element is not of type {typeof(T).Name}");
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

    private static void AddAttr<T>(T el, XmlAttribute attr, XmlNode node) where T : VisualElement {
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
            case "high-value":
                AddHighValueAttr(el, attr);
                return;
            case "value":
                AddValueAttr(el, attr, node);
                return;
            case "low-value":
                AddLowValueAttr(el, attr);
                return;
            case "direction":
                AddDirectionAttr(el, attr);
                return;
            case "max-length":
                AddMaxLengthAttr(el, attr);
                return;
            case "password":
                AddPasswordAttr(el, attr);
                return;
            case "mask-character":
                AddMaskCharAttr(el, attr);
                return;
            case "readonly":
                AddReadOnlyAttr(el, attr);
                return;
            case "is-delayed":
                AddIsDelayedAttr(el, attr);
                return;
            case "hide-mobile-input":
                AddHideMobileInputAttr(el, attr);
                return;
            case "keyboard-type":
                AddKeyboardTypeAttr(el, attr);
                return;
            case "auto-correction":
                AddAutoCorrectionAttr(el, attr);
                return;
            case "multiline":
                AddMultilineAttr(el, attr);
                return;
            case "page-size":
                AddPageSizeAttr(el, attr);
                return;
            case "show-input-field":
                AddShowInputFieldAttr(el, attr);
                return;
            case "inverted":
                AddInvertedAttr(el, attr);
                return;
            case "min-value":
                AddMinValueAttr(el, attr);
                return;
            case "max-value":
                AddMaxValueAttr(el, attr);
                return;
            case "low-limit":
                AddLowLimitAttr(el, attr);
                return;
            case "high-limit":
                AddHighLimitAttr(el, attr);
                return;
            case "title":
                AddTitleAttr(el, attr);
                return;
            case "index":
                AddIndexAttr(el, attr);
                return;
            case "choices":
                AddChoicesAttr(el, attr);
                return;
            case "type":
                // Handled in value
                return;
            case "include-obsolete-values":
                // Handled in value
                return;
            case "x":
            case "y":
            case "z":
            case "w":
            case "h":
                AddVectorAttrs(el, attr, node);
                break;
            case "cx":
            case "cy":
            case "cz":
            case "ex":
            case "ey":
            case "ez":
                AddBoundsAttrs(el, attr, node);
                break;
            case "px":
            case "py":
            case "pz":
            case "sx":
            case "sy":
            case "sz":
                AddBoundsIntAttrs(el, attr, node);
                break;
            case "style":
                ParseAndApplyUSS(el, attr);
                break;
            default:
                AttributeNameWarning(el, attr);
                return;
        }
    }
    
    /*
     * USS
     */

    private static void ParseAndApplyUSS(VisualElement el, XmlNode attr) {
        string ussString = attr.Value;
        if (ussString == null) return;

        Dictionary<string, string> styles = ParseUSS(ussString);
        
        foreach (var kvp in styles)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value}");
        }
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

    private static void SetHash128Attr<T>(T el, XmlAttribute attr, Action<T, Hash128> add) where T : VisualElement {
        try {
            Hash128 hashValue = Hash128.Parse(attr.Value);
            add(el, hashValue);
        }
        catch (Exception) {
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

    private static void SetUlongAttr<T>(T el, XmlAttribute attr, Action<T, ulong> add) where T : VisualElement {
        if (ulong.TryParse(attr.Value, out ulong ulongNum)) {
            add(el, ulongNum);
        }
        else {
            AttributeValueWarning(el, attr);
        }
    }

    private static void SetUintAttr<T>(T el, XmlAttribute attr, Action<T, uint> add) where T : VisualElement {
        if (uint.TryParse(attr.Value, out uint uintNum)) {
            add(el, uintNum);
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

    private static void SetCharAttr<T>(T el, XmlAttribute attr, Action<T, char> add) where T : VisualElement {
        if (char.TryParse(attr.Value, out char character)) {
            add(el, character);
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

    private static void SetCustomEnumValue(EnumField el, string value, Type enumType) {
        if (Enum.TryParse(enumType, value, true, out object enumValue)) {
            el.Init((Enum)enumValue);
            el.value = (Enum)enumValue;
        }
        else {
            XmlDocument doc = new();
            XmlAttribute attr = doc.CreateAttribute("value");
            attr.Value = value;
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
                SetEnumAttr(el, attr, TouchScrollBehaviorMap, (e, v) => e.touchScrollBehavior = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddPageSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                SetFloatAttr(el, attr, (e, v) => e.pageSize = v);
                break;
            case SliderInt el:
                SetFloatAttr(el, attr, (e, v) => e.pageSize = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalPageSizeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                SetFloatAttr(el, attr, (e, v) => e.horizontalPageSize = v);
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
                SetEnumAttr(el, attr, HorizontalScrollerVisibilityMap, (e, v) => e.horizontalScrollerVisibility = v);
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
            case TextField el:
                el.bindingPath = attr.Value;
                break;
            case Foldout el:
                el.bindingPath = attr.Value;
                break;
            case Slider el:
                el.bindingPath = attr.Value;
                break;
            case SliderInt el:
                el.bindingPath = attr.Value;
                break;
            case MinMaxSlider el:
                el.bindingPath = attr.Value;
                break;
            case ProgressBar el:
                el.bindingPath = attr.Value;
                break;
            case DropdownField el:
                el.bindingPath = attr.Value;
                break;
            case EnumField el:
                el.bindingPath = attr.Value;
                break;
            case RadioButton el:
                el.bindingPath = attr.Value;
                break;
            case RadioButtonGroup el:
                el.bindingPath = attr.Value;
                break;
            case IntegerField el:
                el.bindingPath = attr.Value;
                break;
            case FloatField el:
                el.bindingPath = attr.Value;
                break;
            case LongField el:
                el.bindingPath = attr.Value;
                break;
            case DoubleField el:
                el.bindingPath = attr.Value;
                break;
            case Hash128Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector2Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector3Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector4Field el:
                el.bindingPath = attr.Value;
                break;
            case RectField el:
                el.bindingPath = attr.Value;
                break;
            case BoundsField el:
                el.bindingPath = attr.Value;
                break;
            case UnsignedIntegerField el:
                el.bindingPath = attr.Value;
                break;
            case UnsignedLongField el:
                el.bindingPath = attr.Value;
                break;
            case Vector2IntField el:
                el.bindingPath = attr.Value;
                break;
            case Vector3IntField el:
                el.bindingPath = attr.Value;
                break;
            case RectIntField el:
                el.bindingPath = attr.Value;
                break;
            case BoundsIntField el:
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
            case Foldout el:
                el.text = attr.Value;
                break;
            case RadioButton el:
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

    private static void AddTitleAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ProgressBar el:
                el.title = attr.Value;
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
            case TextField el:
                el.label = attr.Value;
                break;
            case Slider el:
                el.label = attr.Value;
                break;
            case SliderInt el:
                el.label = attr.Value;
                break;
            case MinMaxSlider el:
                el.label = attr.Value;
                break;
            case DropdownField el:
                el.label = attr.Value;
                break;
            case EnumField el:
                el.label = attr.Value;
                break;
            case RadioButton el:
                el.label = attr.Value;
                break;
            case RadioButtonGroup el:
                el.label = attr.Value;
                break;
            case IntegerField el:
                el.bindingPath = attr.Value;
                break;
            case FloatField el:
                el.bindingPath = attr.Value;
                break;
            case LongField el:
                el.bindingPath = attr.Value;
                break;
            case DoubleField el:
                el.bindingPath = attr.Value;
                break;
            case Hash128Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector2Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector3Field el:
                el.bindingPath = attr.Value;
                break;
            case Vector4Field el:
                el.bindingPath = attr.Value;
                break;
            case RectField el:
                el.bindingPath = attr.Value;
                break;
            case BoundsField el:
                el.bindingPath = attr.Value;
                break;
            case UnsignedIntegerField el:
                el.bindingPath = attr.Value;
                break;
            case UnsignedLongField el:
                el.bindingPath = attr.Value;
                break;
            case Vector2IntField el:
                el.bindingPath = attr.Value;
                break;
            case Vector3IntField el:
                el.bindingPath = attr.Value;
                break;
            case RectIntField el:
                el.bindingPath = attr.Value;
                break;
            case BoundsIntField el:
                el.bindingPath = attr.Value;
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHighValueAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                SetFloatAttr(el, attr, (e, v) => e.highValue = v);
                break;
            case Slider el:
                SetFloatAttr(el, attr, (e, v) => e.highValue = v);
                break;
            case SliderInt el:
                SetIntAttr(el, attr, (e, v) => e.highValue = v);
                break;
            case ProgressBar el:
                SetFloatAttr(el, attr, (e, v) => e.highValue = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLowValueAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                SetFloatAttr(el, attr, (e, v) => e.lowValue = v);
                break;
            case Slider el:
                SetFloatAttr(el, attr, (e, v) => e.lowValue = v);
                break;
            case SliderInt el:
                SetIntAttr(el, attr, (e, v) => e.lowValue = v);
                break;
            case ProgressBar el:
                SetFloatAttr(el, attr, (e, v) => e.lowValue = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddValueAttr<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                SetFloatAttr(el, attr, (e, v) => e.value = v);
                break;
            case TextField el:
                el.value = attr.Value;
                break;
            case Foldout el:
                SetBooleanAttr(el, attr, (e, v) => e.value = v);
                break;
            case ProgressBar el:
                SetFloatAttr(el, attr, (e, v) => e.value = v);
                break;
            case EnumField el:
                AddEnumFieldValueAttr(el, attr, node);
                break;
            case RadioButton el:
                SetBooleanAttr(el, attr, (e, v) => e.value = v);
                break;
            case RadioButtonGroup el:
                SetIntAttr(el, attr, (e, v) => e.value = v);
                break;
            case IntegerField el:
                SetIntAttr(el, attr, (e, v) => e.value = v);
                break;
            case FloatField el:
                SetFloatAttr(el, attr, (e, v) => e.value = v);
                break;
            case LongField el:
                SetLongAttr(el, attr, (e, v) => e.value = v);
                break;
            case Hash128Field el:
                SetHash128Attr(el, attr, (e, v) => e.value = v);
                break;
            case UnsignedIntegerField el:
                SetUintAttr(el, attr, (e, v) => e.value = v);
                break;
            case UnsignedLongField el:
                SetUlongAttr(el, attr, (e, v) => e.value = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddEnumFieldValueAttr(EnumField el, XmlAttribute attr, XmlNode node) {
        // EnumField has properties that need to be set during initialization
        XmlAttribute typeAttr = node.Attributes?["type"];
        XmlAttribute obsoleteAttr = node.Attributes?["include-obsolete-values"];

        // Type attribute is required
        if (typeAttr != null) {
            // Find the enum type
            Type enumType = Type.GetType(typeAttr.Value);

            // If it exists and is an enum
            if (enumType != null && enumType.IsEnum) {
                // Find the obsolete values attribute if it exists
                bool includeObsoleteValues =
                    obsoleteAttr != null && bool.TryParse(obsoleteAttr.Value, out bool result) && result;

                // Get the enum values
                Array enumValues = Enum.GetValues(enumType);

                if (enumValues.Length > 0) {
                    Enum defaultEnumValue;

                    if (includeObsoleteValues) {
                        // Include obsolete enum values
                        defaultEnumValue = enumValues.Cast<Enum>()
                            .FirstOrDefault(v => v.GetType().GetField(v.ToString())
                                .GetCustomAttribute<ObsoleteAttribute>() != null);
                    }
                    else {
                        // Default to the first non-obsolete value
                        defaultEnumValue = enumValues.Cast<Enum>()
                            .FirstOrDefault(v => v.GetType().GetField(v.ToString())
                                .GetCustomAttribute<ObsoleteAttribute>() == null);
                    }

                    // Initialize the EnumField
                    el.Init(defaultEnumValue ?? (Enum)enumValues.GetValue(0));

                    // Set the value
                    SetCustomEnumValue(el, attr.Value, enumType);
                }
                else {
                    // Handle the case where there are no enum values
                    AttributeValueWarning(el, attr);
                }
            }
            else {
                AttributeValueWarning(el, attr);
            }
        }
    }

    private static void AddDirectionAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                SetEnumAttr(el, attr, SliderDirectionMap, (e, v) => e.direction = v);
                break;
            case Slider el:
                SetEnumAttr(el, attr, SliderDirectionMap, (e, v) => e.direction = v);
                break;
            case SliderInt el:
                SetEnumAttr(el, attr, SliderDirectionMap, (e, v) => e.direction = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMultilineAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.multiline = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddAutoCorrectionAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.autoCorrection = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddKeyboardTypeAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetEnumAttr(el, attr, TouchScreenKeyboardTypeMap, (e, v) => e.keyboardType = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHideMobileInputAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.hideMobileInput = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddIsDelayedAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case IntegerField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case FloatField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case LongField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case Hash128Field el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case UnsignedIntegerField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            case UnsignedLongField el:
                SetBooleanAttr(el, attr, (e, v) => e.isDelayed = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReadOnlyAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case IntegerField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case FloatField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case LongField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case Hash128Field el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case UnsignedIntegerField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case UnsignedLongField el:
                SetBooleanAttr(el, attr, (e, v) => e.isReadOnly = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaskCharAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetCharAttr(el, attr, (e, v) => e.maskChar = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaxLengthAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetIntAttr(el, attr, (e, v) => e.maxLength = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddPasswordAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                SetBooleanAttr(el, attr, (e, v) => e.isPasswordField = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowInputFieldAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                SetBooleanAttr(el, attr, (e, v) => e.showInputField = v);
                break;
            case SliderInt el:
                SetBooleanAttr(el, attr, (e, v) => e.showInputField = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddInvertedAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                SetBooleanAttr(el, attr, (e, v) => e.inverted = v);
                break;
            case SliderInt el:
                SetBooleanAttr(el, attr, (e, v) => e.inverted = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMinValueAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                SetFloatAttr(el, attr, (e, v) => e.minValue = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaxValueAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                SetFloatAttr(el, attr, (e, v) => e.maxValue = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLowLimitAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                SetFloatAttr(el, attr, (e, v) => e.lowLimit = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHighLimitAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                SetFloatAttr(el, attr, (e, v) => e.highLimit = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddIndexAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case DropdownField el:
                SetIntAttr(el, attr, (e, v) => e.index = v);
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddChoicesAttr<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case DropdownField el:
                el.choices = attr.Value.Split(',').ToList();
                break;
            case RadioButtonGroup el:
                el.choices = attr.Value.Split(',').ToList();
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVectorAttrs<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        XmlAttribute xAttr = node.Attributes?["x"];
        XmlAttribute yAttr = node.Attributes?["y"];
        XmlAttribute zAttr = node.Attributes?["z"];
        XmlAttribute wAttr = node.Attributes?["w"];
        XmlAttribute hAttr = node.Attributes?["h"];

        if (node.Name == "ui:Vector2IntField" || node.Name == "ui:Vector3IntField" || node.Name == "ui:RectIntField") {
            int x = xAttr != null && int.TryParse(xAttr.Value, out int xValue) ? xValue : 0;
            int y = yAttr != null && int.TryParse(yAttr.Value, out int yValue) ? yValue : 0;
            int z = zAttr != null && int.TryParse(zAttr.Value, out int zValue) ? zValue : 0;
            int w = wAttr != null && int.TryParse(wAttr.Value, out int wValue) ? wValue : 0;
            int h = hAttr != null && int.TryParse(hAttr.Value, out int hValue) ? hValue : 0;

            switch (gEl) {
                case Vector2IntField el:
                    el.value = new Vector2Int(x, y);
                    break;
                case Vector3IntField el:
                    el.value = new Vector3Int(x, y, z);
                    break;
                case RectIntField el:
                    el.value = new RectInt(x, y, w, h);
                    break;
                default:
                    AttributeUnsupportedWarning(gEl, attr);
                    break;
            }
        }
        else {
            float x = xAttr != null && float.TryParse(xAttr.Value, out float xValue) ? xValue : 0f;
            float y = yAttr != null && float.TryParse(yAttr.Value, out float yValue) ? yValue : 0f;
            float z = zAttr != null && float.TryParse(zAttr.Value, out float zValue) ? zValue : 0f;
            float w = wAttr != null && float.TryParse(wAttr.Value, out float wValue) ? wValue : 0f;
            float h = hAttr != null && float.TryParse(hAttr.Value, out float hValue) ? hValue : 0f;

            switch (gEl) {
                case Vector2Field el:
                    el.value = new Vector2(x, y);
                    break;
                case Vector3Field el:
                    el.value = new Vector3(x, y, z);
                    break;
                case Vector4Field el:
                    el.value = new Vector4(x, y, z, w);
                    break;
                case RectField el:
                    el.value = new Rect(x, y, w, h);
                    break;
                default:
                    AttributeUnsupportedWarning(gEl, attr);
                    break;
            }
        }
    }

    private static void AddBoundsAttrs<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        XmlAttribute cxAttr = node.Attributes?["cx"];
        XmlAttribute cyAttr = node.Attributes?["cy"];
        XmlAttribute czAttr = node.Attributes?["cz"];
        XmlAttribute exAttr = node.Attributes?["ex"];
        XmlAttribute eyAttr = node.Attributes?["ey"];
        XmlAttribute ezAttr = node.Attributes?["ez"];

        float cx = cxAttr != null && float.TryParse(cxAttr.Value, out float cxValue) ? cxValue : 0f;
        float cy = cyAttr != null && float.TryParse(cyAttr.Value, out float cyValue) ? cyValue : 0f;
        float cz = czAttr != null && float.TryParse(czAttr.Value, out float czValue) ? czValue : 0f;
        float ex = exAttr != null && float.TryParse(exAttr.Value, out float exValue) ? exValue : 0f;
        float ey = eyAttr != null && float.TryParse(eyAttr.Value, out float eyValue) ? eyValue : 0f;
        float ez = ezAttr != null && float.TryParse(ezAttr.Value, out float ezValue) ? ezValue : 0f;

        switch (gEl) {
            case BoundsField el:
                el.value = new Bounds(new Vector3(cx, cy, cz), new Vector3(ex, ey, ez));
                break;
            default:
                AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddBoundsIntAttrs<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        XmlAttribute pxAttr = node.Attributes?["px"];
        XmlAttribute pyAttr = node.Attributes?["py"];
        XmlAttribute pzAttr = node.Attributes?["pz"];
        XmlAttribute sxAttr = node.Attributes?["sx"];
        XmlAttribute syAttr = node.Attributes?["sy"];
        XmlAttribute szAttr = node.Attributes?["sz"];

        int px = pxAttr != null && int.TryParse(pxAttr.Value, out int pxValue) ? pxValue : 0;
        int py = pyAttr != null && int.TryParse(pyAttr.Value, out int pyValue) ? pyValue : 0;
        int pz = pzAttr != null && int.TryParse(pzAttr.Value, out int pzValue) ? pzValue : 0;
        int sx = sxAttr != null && int.TryParse(sxAttr.Value, out int sxValue) ? sxValue : 0;
        int sy = syAttr != null && int.TryParse(syAttr.Value, out int syValue) ? syValue : 0;
        int sz = szAttr != null && int.TryParse(szAttr.Value, out int szValue) ? szValue : 0;

        switch (gEl) {
            case BoundsIntField el:
                el.value = new BoundsInt(new Vector3Int(px, py, pz), new Vector3Int(sx, sy, sz));
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

    private static void AttributeNameWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute name on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    private static void AttributeValueWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Invalid attribute value on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }

    private static void AttributeUnsupportedWarning(VisualElement element, XmlAttribute attribute) {
        Debug.LogWarning(
            $"Unsupported attribute on {GetElementName(element)}: {attribute.Name}=\"{attribute.Value}\"");
    }
}