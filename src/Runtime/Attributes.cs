using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;


internal static class Attributes {
    internal static void Add<T>(T el, XmlAttribute attr, XmlNode node) where T : VisualElement {
        switch (attr.Name) {
            case "class":
                AddClass(el, attr);
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
                AddBoolean(el, attr, (e, v) => e.focusable = v);
                return;
            case "visible":
                AddBoolean(el, attr, (e, v) => e.visible = v);
                return;
            case "delegates-focus":
                AddBoolean(el, attr, (e, v) => e.delegatesFocus = v);
                return;
            case "picking-mode":
                AddEnum(el, attr, Maps.PickingMode, (e, v) => e.pickingMode = v);
                return;
            case "usage-hints":
                AddEnum(el, attr, Maps.UsageHints, (e, v) => e.usageHints = v);
                return;
            case "tabindex":
                AddInt(el, attr, (e, v) => e.tabIndex = v);
                return;
            case "binding-path":
                AddBindingPath(el, attr);
                return;
            case "text":
                AddText(el, attr);
                return;
            case "parse-escape-sequences":
                AddParseEscapeSequences(el, attr);
                return;
            case "display-tooltip-when-elided":
                AddDisplayTooltipWhenElided(el, attr);
                return;
            case "enable-rich-text":
                AddEnableRichText(el, attr);
                return;
            case "mode":
                AddMode(el, attr);
                return;
            case "horizontal-scroller-visibility":
                AddHorizontalScrollerVisibility(el, attr);
                return;
            case "nested-interaction-kind":
                AddNestedInteractionKind(el, attr);
                return;
            case "vertical-scroller-visibility":
                AddVerticalScrollerVisibility(el, attr);
                return;
            case "horizontal-page-size":
                AddVerticalPageSize(el, attr);
                return;
            case "vertical-page-size":
                AddHorizontalPageSize(el, attr);
                return;
            case "mouse-wheel-scroll-size":
                AddMouseWheelScrollSize(el, attr);
                return;
            case "touch-scroll-type":
                AddTouchScrollType(el, attr);
                return;
            case "scroll-deceleration-rate":
                AddScrollDecelerationRate(el, attr);
                return;
            case "elasticity":
                AddElasticity(el, attr);
                return;
            case "elastic-animation-interval-ms":
                AddElasticityAnimationIntervalMs(el, attr);
                return;
            case "show-border":
                AddShowBorder(el, attr);
                return;
            case "selection-type":
                AddSelectionType(el, attr);
                return;
            case "show-alternating-row-backgrounds":
                AddShowAlternatingRowBackgrounds(el, attr);
                return;
            case "reorderable":
                AddReorderable(el, attr);
                return;
            case "horizontal-scrolling":
                AddHorizontalScrolling(el, attr);
                return;
            case "show-foldout-header":
                AddShowFoldoutHeader(el, attr);
                return;
            case "header-title":
                AddHeaderTitle(el, attr);
                return;
            case "show-add-remove-footer":
                AddShowAddRemoveFooter(el, attr);
                return;
            case "reorder-mode":
                AddReorderMode(el, attr);
                return;
            case "show-bound-collection-size":
                AddShowBoundCollectionSize(el, attr);
                return;
            case "fixed-item-height":
                AddFixedItemHeight(el, attr);
                return;
            case "virtualization-method":
                AddVirtualizationMethod(el, attr);
                return;
            case "auto-expand":
                AddAutoExpand(el, attr);
                return;
            case "label":
                AddLabel(el, attr);
                return;
            case "high-value":
                AddHighValue(el, attr);
                return;
            case "value":
                AddValue(el, attr, node);
                return;
            case "low-value":
                AddLowValue(el, attr);
                return;
            case "direction":
                AddDirection(el, attr);
                return;
            case "max-length":
                AddMaxLength(el, attr);
                return;
            case "password":
                AddPassword(el, attr);
                return;
            case "mask-character":
                AddMaskChar(el, attr);
                return;
            case "readonly":
                AddReadOnly(el, attr);
                return;
            case "is-delayed":
                AddIsDelayed(el, attr);
                return;
            case "hide-mobile-input":
                AddHideMobileInput(el, attr);
                return;
            case "keyboard-type":
                AddKeyboardType(el, attr);
                return;
            case "auto-correction":
                AddAutoCorrection(el, attr);
                return;
            case "multiline":
                AddMultiline(el, attr);
                return;
            case "page-size":
                AddPageSize(el, attr);
                return;
            case "show-input-field":
                AddShowInputField(el, attr);
                return;
            case "inverted":
                AddInverted(el, attr);
                return;
            case "min-value":
                AddMinValue(el, attr);
                return;
            case "max-value":
                AddMaxValue(el, attr);
                return;
            case "low-limit":
                AddLowLimit(el, attr);
                return;
            case "high-limit":
                AddHighLimit(el, attr);
                return;
            case "title":
                AddTitle(el, attr);
                return;
            case "index":
                AddIndex(el, attr);
                return;
            case "choices":
                AddChoices(el, attr);
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
                USS.ParseAndApplyUSS(el, attr);
                break;
            default:
                Logging.AttributeNameWarning(el, attr);
                return;
        }
    }

    /*
     * Attribute Setters
     */

    private static void AddBoolean<T>(T el, XmlAttribute attr, Action<T, bool> add) where T : VisualElement {
        if (bool.TryParse(attr.Value, out bool boolean)) {
            add(el, boolean);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddInt<T>(T el, XmlAttribute attr, Action<T, int> add) where T : VisualElement {
        if (int.TryParse(attr.Value, out int integer)) {
            add(el, integer);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddHash128<T>(T el, XmlAttribute attr, Action<T, Hash128> add) where T : VisualElement {
        try {
            Hash128 hashValue = Hash128.Parse(attr.Value);
            add(el, hashValue);
        } catch (Exception) {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddFloat<T>(T el, XmlAttribute attr, Action<T, float> add) where T : VisualElement {
        if (float.TryParse(attr.Value, out float floatNum)) {
            add(el, floatNum);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddUlong<T>(T el, XmlAttribute attr, Action<T, ulong> add) where T : VisualElement {
        if (ulong.TryParse(attr.Value, out ulong ulongNum)) {
            add(el, ulongNum);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddUint<T>(T el, XmlAttribute attr, Action<T, uint> add) where T : VisualElement {
        if (uint.TryParse(attr.Value, out uint uintNum)) {
            add(el, uintNum);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddLong<T>(T el, XmlAttribute attr, Action<T, long> add) where T : VisualElement {
        if (long.TryParse(attr.Value, out long longNum)) {
            add(el, longNum);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddChar<T>(T el, XmlAttribute attr, Action<T, char> add) where T : VisualElement {
        if (char.TryParse(attr.Value, out char character)) {
            add(el, character);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddEnum<T, T2>(T el, XmlAttribute attr, Dictionary<string, T2> map, Action<T, T2> add)
        where T : VisualElement {
        if (map.TryGetValue(attr.Value, out T2 enumValue)) {
            add(el, enumValue);
        } else {
            Logging.AttributeValueWarning(el, attr);
        }
    }

    private static void AddCustomEnumValue(EnumField el, string value, Type enumType) {
        if (Enum.TryParse(enumType, value, true, out object enumValue)) {
            el.Init((Enum)enumValue);
            el.value = (Enum)enumValue;
        } else {
            XmlDocument doc = new();
            XmlAttribute attr = doc.CreateAttribute("value");
            attr.Value = value;
            Logging.AttributeValueWarning(el, attr);
        }
    }

    /*
     * Specialized attribute setters
     */

    private static void AddClass<T>(T el, XmlAttribute attr) where T : VisualElement {
        string[] classes = attr.Value.Split(" ");

        foreach (string cls in classes) {
            el.AddToClassList(cls);
        }
    }

    private static void AddParseEscapeSequences<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                AddBoolean(el, attr, (e, v) => e.parseEscapeSequences = v);
                break;
            case Button el:
                AddBoolean(el, attr, (e, v) => e.parseEscapeSequences = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddEnableRichText<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                AddBoolean(el, attr, (e, v) => e.enableRichText = v);
                break;
            case Button el:
                AddBoolean(el, attr, (e, v) => e.enableRichText = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddDisplayTooltipWhenElided<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Label el:
                AddBoolean(el, attr, (e, v) => e.displayTooltipWhenElided = v);
                break;
            case Button el:
                AddBoolean(el, attr, (e, v) => e.displayTooltipWhenElided = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMouseWheelScrollSize<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddFloat(el, attr, (e, v) => e.mouseWheelScrollSize = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddTouchScrollType<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddEnum(el, attr, Maps.TouchScrollBehavior, (e, v) => e.touchScrollBehavior = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddPageSize<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                AddFloat(el, attr, (e, v) => e.pageSize = v);
                break;
            case SliderInt el:
                AddFloat(el, attr, (e, v) => e.pageSize = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalPageSize<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddFloat(el, attr, (e, v) => e.horizontalPageSize = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVerticalPageSize<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddFloat(el, attr, (e, v) => e.verticalPageSize = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVerticalScrollerVisibility<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddEnum(el, attr, Maps.VerticalScrollerVisibility, (e, v) => e.verticalScrollerVisibility = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalScrollerVisibility<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddEnum(el, attr, Maps.HorizontalScrollerVisibility,
                    (e, v) => e.horizontalScrollerVisibility = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddNestedInteractionKind<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddEnum(el, attr, Maps.NestedInteractionKind, (e, v) => e.nestedInteractionKind = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMode<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddEnum(el, attr, Maps.ScrollViewMode, (e, v) => e.mode = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddBindingPath<T>(T gEl, XmlAttribute attr) where T : VisualElement {
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
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddText<T>(T gEl, XmlAttribute attr) where T : VisualElement {
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
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddScrollDecelerationRate<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddFloat(el, attr, (e, v) => e.scrollDecelerationRate = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddElasticity<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddFloat(el, attr, (e, v) => e.elasticity = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddElasticityAnimationIntervalMs<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ScrollView el:
                AddLong(el, attr, (e, v) => e.elasticAnimationIntervalMs = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVirtualizationMethod<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddEnum(el, attr, Maps.CollectionVirtualizationMethod, (e, v) => e.virtualizationMethod = v);
                break;
            case TreeView el:
                AddEnum(el, attr, Maps.CollectionVirtualizationMethod, (e, v) => e.virtualizationMethod = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowBorder<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.showBorder = v);
                break;
            case TreeView el:
                AddBoolean(el, attr, (e, v) => e.showBorder = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddSelectionType<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddEnum(el, attr, Maps.SelectionType, (e, v) => e.selectionType = v);
                break;
            case TreeView el:
                AddEnum(el, attr, Maps.SelectionType, (e, v) => e.selectionType = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowAlternatingRowBackgrounds<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddEnum(el, attr, Maps.AlternatingRowBackground, (e, v) => e.showAlternatingRowBackgrounds = v);
                break;
            case TreeView el:
                AddEnum(el, attr, Maps.AlternatingRowBackground, (e, v) => e.showAlternatingRowBackgrounds = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReorderable<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.reorderable = v);
                break;
            case TreeView el:
                AddBoolean(el, attr, (e, v) => e.reorderable = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHorizontalScrolling<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.horizontalScrollingEnabled = v);
                break;
            case TreeView el:
                AddBoolean(el, attr, (e, v) => e.horizontalScrollingEnabled = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowFoldoutHeader<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.showFoldoutHeader = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHeaderTitle<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                el.headerTitle = attr.Value;
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddTitle<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ProgressBar el:
                el.title = attr.Value;
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowAddRemoveFooter<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.showAddRemoveFooter = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReorderMode<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddEnum(el, attr, Maps.ListViewReorderMode, (e, v) => e.reorderMode = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowBoundCollectionSize<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddBoolean(el, attr, (e, v) => e.showBoundCollectionSize = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddFixedItemHeight<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case ListView el:
                AddFloat(el, attr, (e, v) => e.fixedItemHeight = v);
                break;
            case TreeView el:
                AddFloat(el, attr, (e, v) => e.fixedItemHeight = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddAutoExpand<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TreeView el:
                AddBoolean(el, attr, (e, v) => e.autoExpand = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLabel<T>(T gEl, XmlAttribute attr) where T : VisualElement {
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
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHighValue<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                AddFloat(el, attr, (e, v) => e.highValue = v);
                break;
            case Slider el:
                AddFloat(el, attr, (e, v) => e.highValue = v);
                break;
            case SliderInt el:
                AddInt(el, attr, (e, v) => e.highValue = v);
                break;
            case ProgressBar el:
                AddFloat(el, attr, (e, v) => e.highValue = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLowValue<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                AddFloat(el, attr, (e, v) => e.lowValue = v);
                break;
            case Slider el:
                AddFloat(el, attr, (e, v) => e.lowValue = v);
                break;
            case SliderInt el:
                AddInt(el, attr, (e, v) => e.lowValue = v);
                break;
            case ProgressBar el:
                AddFloat(el, attr, (e, v) => e.lowValue = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddValue<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                AddFloat(el, attr, (e, v) => e.value = v);
                break;
            case TextField el:
                el.value = attr.Value;
                break;
            case Foldout el:
                AddBoolean(el, attr, (e, v) => e.value = v);
                break;
            case ProgressBar el:
                AddFloat(el, attr, (e, v) => e.value = v);
                break;
            case EnumField el:
                AddEnumFieldValue(el, attr, node);
                break;
            case RadioButton el:
                AddBoolean(el, attr, (e, v) => e.value = v);
                break;
            case RadioButtonGroup el:
                AddInt(el, attr, (e, v) => e.value = v);
                break;
            case IntegerField el:
                AddInt(el, attr, (e, v) => e.value = v);
                break;
            case FloatField el:
                AddFloat(el, attr, (e, v) => e.value = v);
                break;
            case LongField el:
                AddLong(el, attr, (e, v) => e.value = v);
                break;
            case Hash128Field el:
                AddHash128(el, attr, (e, v) => e.value = v);
                break;
            case UnsignedIntegerField el:
                AddUint(el, attr, (e, v) => e.value = v);
                break;
            case UnsignedLongField el:
                AddUlong(el, attr, (e, v) => e.value = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddEnumFieldValue(EnumField el, XmlAttribute attr, XmlNode node) {
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
                    } else {
                        // Default to the first non-obsolete value
                        defaultEnumValue = enumValues.Cast<Enum>()
                            .FirstOrDefault(v => v.GetType().GetField(v.ToString())
                                .GetCustomAttribute<ObsoleteAttribute>() == null);
                    }

                    // Initialize the EnumField
                    el.Init(defaultEnumValue ?? (Enum)enumValues.GetValue(0));

                    // Set the value
                    AddCustomEnumValue(el, attr.Value, enumType);
                } else {
                    // Handle the case where there are no enum values
                    Logging.AttributeValueWarning(el, attr);
                }
            } else {
                Logging.AttributeValueWarning(el, attr);
            }
        }
    }

    private static void AddDirection<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Scroller el:
                AddEnum(el, attr, Maps.SliderDirection, (e, v) => e.direction = v);
                break;
            case Slider el:
                AddEnum(el, attr, Maps.SliderDirection, (e, v) => e.direction = v);
                break;
            case SliderInt el:
                AddEnum(el, attr, Maps.SliderDirection, (e, v) => e.direction = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMultiline<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.multiline = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddAutoCorrection<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.autoCorrection = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddKeyboardType<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddEnum(el, attr, Maps.TouchScreenKeyboardType, (e, v) => e.keyboardType = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHideMobileInput<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.hideMobileInput = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddIsDelayed<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case IntegerField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case FloatField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case LongField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case Hash128Field el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case UnsignedIntegerField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            case UnsignedLongField el:
                AddBoolean(el, attr, (e, v) => e.isDelayed = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddReadOnly<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case IntegerField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case FloatField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case LongField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case Hash128Field el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case UnsignedIntegerField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            case UnsignedLongField el:
                AddBoolean(el, attr, (e, v) => e.isReadOnly = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaskChar<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddChar(el, attr, (e, v) => e.maskChar = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaxLength<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddInt(el, attr, (e, v) => e.maxLength = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddPassword<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case TextField el:
                AddBoolean(el, attr, (e, v) => e.isPasswordField = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddShowInputField<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                AddBoolean(el, attr, (e, v) => e.showInputField = v);
                break;
            case SliderInt el:
                AddBoolean(el, attr, (e, v) => e.showInputField = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddInverted<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case Slider el:
                AddBoolean(el, attr, (e, v) => e.inverted = v);
                break;
            case SliderInt el:
                AddBoolean(el, attr, (e, v) => e.inverted = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMinValue<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                AddFloat(el, attr, (e, v) => e.minValue = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddMaxValue<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                AddFloat(el, attr, (e, v) => e.maxValue = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddLowLimit<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                AddFloat(el, attr, (e, v) => e.lowLimit = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddHighLimit<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case MinMaxSlider el:
                AddFloat(el, attr, (e, v) => e.highLimit = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddIndex<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case DropdownField el:
                AddInt(el, attr, (e, v) => e.index = v);
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddChoices<T>(T gEl, XmlAttribute attr) where T : VisualElement {
        switch (gEl) {
            case DropdownField el:
                el.choices = attr.Value.Split(',').ToList();
                break;
            case RadioButtonGroup el:
                el.choices = attr.Value.Split(',').ToList();
                break;
            default:
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }

    private static void AddVectorAttrs<T>(T gEl, XmlAttribute attr, XmlNode node) where T : VisualElement {
        XmlAttribute xAttr = node.Attributes?["x"];
        XmlAttribute yAttr = node.Attributes?["y"];
        XmlAttribute zAttr = node.Attributes?["z"];
        XmlAttribute wAttr = node.Attributes?["w"];
        XmlAttribute hAttr = node.Attributes?["h"];

        if (node.Name == "ui:Vector2IntField" || node.Name == "ui:Vector3IntField" ||
            node.Name == "ui:RectIntField") {
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
                    Logging.AttributeUnsupportedWarning(gEl, attr);
                    break;
            }
        } else {
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
                    Logging.AttributeUnsupportedWarning(gEl, attr);
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
                Logging.AttributeUnsupportedWarning(gEl, attr);
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
                Logging.AttributeUnsupportedWarning(gEl, attr);
                break;
        }
    }
}