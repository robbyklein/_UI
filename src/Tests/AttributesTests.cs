using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;

public class AttributesTests {
    public const string StringAttributeValue = "hello";

    public T BuildElementWithAttribute<T>(string attributeName, string attributeValue) where T : VisualElement, new() {
        return UIBuddy.Build<T>($"<ui:{typeof(T).Name} {attributeName}=\"{attributeValue}\"/>");
    }

    private Dictionary<string, string> attributeToPropertyNameMap = new() {
        { "horizontal-scrolling", "horizontalScrollingEnabled" },
        { "touch-scroll-type", "touchScrollBehavior" },
        { "mask-character", "maskChar" },
        { "password", "isPasswordField" },
        { "readonly", "isReadOnly" },
        { "tabindex", "tabIndex" }
    };

    private string ConvertAttributeNameToPropertyName(string attributeName) {
        if (attributeToPropertyNameMap.TryGetValue(attributeName, out string propertyName)) {
            return propertyName;
        }

        string[] parts = attributeName.Split('-');
        for (int i = 1; i < parts.Length; i++) {
            parts[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[i]);
        }

        return string.Join(string.Empty, parts);
    }

    private void TestStringAttribute<T>(string attributeName, string expectedValue) where T : VisualElement, new() {
        T element = BuildElementWithAttribute<T>(attributeName, expectedValue);
        Assert.NotNull(element);

        string propertyName = ConvertAttributeNameToPropertyName(attributeName);
        PropertyInfo property = element.GetType().GetProperty(propertyName);
        Assert.NotNull(property, $"Property '{propertyName}' not found on {typeof(T).Name}.");

        string actualValue = property.GetValue(element).ToString();
        Assert.AreEqual(expectedValue, actualValue);
    }

    private void TestBooleanAttribute<T>(string attributeName, bool expectedValue) where T : VisualElement, new() {
        T element = BuildElementWithAttribute<T>(attributeName, expectedValue.ToString().ToLower());
        Assert.NotNull(element);

        string propertyName = ConvertAttributeNameToPropertyName(attributeName);
        PropertyInfo property = element.GetType().GetProperty(propertyName);
        Assert.NotNull(property, $"Property '{propertyName}' not found on {typeof(T).Name}.");

        bool actualValue = (bool)property.GetValue(element);
        Assert.AreEqual(expectedValue, actualValue);
    }

    private void TestClassListAttribute<T>(string classList) where T : VisualElement, new() {
        T element = BuildElementWithAttribute<T>("class", classList);
        Assert.NotNull(element);

        foreach (string className in classList.Split(' ')) {
            bool hasClass = element.ClassListContains(className);
            Assert.AreEqual(true, hasClass);
        }
    }

    private void TestNumericAttribute<T>(string attributeName, string expectedValue) where T : VisualElement, new() {
        T element = BuildElementWithAttribute<T>(attributeName, expectedValue);
        Assert.NotNull(element);

        string propertyName = ConvertAttributeNameToPropertyName(attributeName);
        PropertyInfo property = element.GetType().GetProperty(propertyName);
        Assert.NotNull(property, $"Property '{propertyName}' not found on {typeof(T).Name}.");

        string actualValue = property.GetValue(element).ToString();
        Assert.AreEqual(float.Parse(expectedValue), float.Parse(actualValue));
    }

    private void TestListAttribute<T>(string attributeName, List<string> expectedValues)
        where T : VisualElement, new() {
        T element = BuildElementWithAttribute<T>(attributeName, string.Join(",", expectedValues));
        Assert.NotNull(element);

        string propertyName = ConvertAttributeNameToPropertyName(attributeName);
        PropertyInfo property = element.GetType().GetProperty(propertyName);
        Assert.NotNull(property, $"Property '{propertyName}' not found on {typeof(T).Name}.");

        List<string> actualValue = (List<string>)property.GetValue(element);
        CollectionAssert.AreEqual(expectedValues, actualValue);
    }

    [Test]
    public void TestClassAttribute() {
        TestClassListAttribute<VisualElement>("hello hello2");
    }

    [Test]
    public void TestNameAttribute() {
        TestStringAttribute<VisualElement>("name", StringAttributeValue);
    }

    [Test]
    public void TestTooltipAttribute() {
        TestStringAttribute<VisualElement>("tooltip", StringAttributeValue);
    }

    [Test]
    public void TestViewDataKeyAttribute() {
        TestStringAttribute<VisualElement>("view-data-key", StringAttributeValue);
    }

    [Test]
    public void TestFocusableAttribute() {
        TestBooleanAttribute<VisualElement>("focusable", true);
        TestBooleanAttribute<VisualElement>("focusable", false);
    }

    [Test]
    public void TestVisibleAttribute() {
        TestBooleanAttribute<VisualElement>("visible", true);
        TestBooleanAttribute<VisualElement>("visible", false);
    }

    [Test]
    public void TestDelegatesFocusAttribute() {
        TestBooleanAttribute<VisualElement>("delegates-focus", true);
        TestBooleanAttribute<VisualElement>("delegates-focus", false);
    }

    [Test]
    public void TestPickingModeAttribute() {
        TestStringAttribute<VisualElement>("picking-mode", "Position");
    }

    [Test]
    public void TestUsageHintsAttribute() {
        TestStringAttribute<VisualElement>("usage-hints", "DynamicTransform");
    }

    [Test]
    public void TestTabIndexAttribute() {
        TestNumericAttribute<VisualElement>("tabindex", "1");
    }

    [Test]
    public void TestBindingPathAttribute() {
        TestStringAttribute<Label>("binding-path", StringAttributeValue);
    }

    [Test]
    public void TestTextAttribute() {
        TestStringAttribute<Label>("text", StringAttributeValue);
    }

    [Test]
    public void TestParseEscapeSequencesAttribute() {
        TestBooleanAttribute<Label>("parse-escape-sequences", true);
        TestBooleanAttribute<Label>("parse-escape-sequences", false);
    }

    [Test]
    public void TestDisplayTooltipWhenElidedAttribute() {
        TestBooleanAttribute<Label>("display-tooltip-when-elided", true);
        TestBooleanAttribute<Label>("display-tooltip-when-elided", false);
    }

    [Test]
    public void TestEnableRichTextAttribute() {
        TestBooleanAttribute<Label>("enable-rich-text", true);
        TestBooleanAttribute<Label>("enable-rich-text", false);
    }

    [Test]
    public void TestModeAttribute() {
        TestStringAttribute<ScrollView>("mode", "VerticalAndHorizontal");
    }

    [Test]
    public void TestHorizontalScrollerVisibilityAttribute() {
        TestStringAttribute<ScrollView>("horizontal-scroller-visibility", "Auto");
    }

    [Test]
    public void TestVerticalScrollerVisibilityAttribute() {
        TestStringAttribute<ScrollView>("vertical-scroller-visibility", "Auto");
    }

    [Test]
    public void TestMouseWheelScrollSizeAttribute() {
        TestNumericAttribute<ScrollView>("mouse-wheel-scroll-size", "10.0");
    }

    [Test]
    public void TestTouchScrollTypeAttribute() {
        TestStringAttribute<ScrollView>("touch-scroll-type", "Elastic");
    }

    [Test]
    public void TestScrollDecelerationRateAttribute() {
        TestNumericAttribute<ScrollView>("scroll-deceleration-rate", "0.135");
    }

    [Test]
    public void TestElasticityAttribute() {
        TestNumericAttribute<ScrollView>("elasticity", "0.1");
    }

    [Test]
    public void TestElasticAnimationIntervalMsAttribute() {
        TestNumericAttribute<ScrollView>("elastic-animation-interval-ms", "150");
    }

    [Test]
    public void TestShowBorderAttribute() {
        TestBooleanAttribute<ListView>("show-border", true);
        TestBooleanAttribute<ListView>("show-border", false);
    }

    [Test]
    public void TestSelectionTypeAttribute() {
        TestStringAttribute<ListView>("selection-type", "Single");
    }

    [Test]
    public void TestShowAlternatingRowBackgroundsAttribute() {
        TestStringAttribute<ListView>("show-alternating-row-backgrounds", "All");
    }

    [Test]
    public void TestReorderableAttribute() {
        TestBooleanAttribute<ListView>("reorderable", true);
        TestBooleanAttribute<ListView>("reorderable", false);
    }

    [Test]
    public void TestHorizontalScrollingAttribute() {
        TestBooleanAttribute<ListView>("horizontal-scrolling", true);
        TestBooleanAttribute<ListView>("horizontal-scrolling", false);
    }

    [Test]
    public void TestShowFoldoutHeaderAttribute() {
        TestBooleanAttribute<ListView>("show-foldout-header", true);
        TestBooleanAttribute<ListView>("show-foldout-header", false);
    }

    [Test]
    public void TestHeaderTitleAttribute() {
        TestStringAttribute<ListView>("header-title", StringAttributeValue);
    }

    [Test]
    public void TestShowAddRemoveFooterAttribute() {
        TestBooleanAttribute<ListView>("show-add-remove-footer", true);
        TestBooleanAttribute<ListView>("show-add-remove-footer", false);
    }

    [Test]
    public void TestReorderModeAttribute() {
        TestStringAttribute<ListView>("reorder-mode", "Simple");
    }

    [Test]
    public void TestShowBoundCollectionSizeAttribute() {
        TestBooleanAttribute<ListView>("show-bound-collection-size", true);
        TestBooleanAttribute<ListView>("show-bound-collection-size", false);
    }

    [Test]
    public void TestFixedItemHeightAttribute() {
        TestNumericAttribute<ListView>("fixed-item-height", "45.0");
    }

    [Test]
    public void TestVirtualizationMethodAttribute() {
        TestStringAttribute<ListView>("virtualization-method", "FixedHeight");
    }

    [Test]
    public void TestAutoExpandAttribute() {
        TestBooleanAttribute<TreeView>("auto-expand", true);
        TestBooleanAttribute<TreeView>("auto-expand", false);
    }

    [Test]
    public void TestLabelAttribute() {
        TestStringAttribute<TextField>("label", StringAttributeValue);
    }

    [Test]
    public void TestHighValueAttribute() {
        TestNumericAttribute<Slider>("high-value", "100.0");
    }

    [Test]
    public void TestValueAttribute() {
        TestStringAttribute<TextField>("value", StringAttributeValue);
    }

    [Test]
    public void TestLowValueAttribute() {
        TestNumericAttribute<Slider>("low-value", "0.0");
    }

    [Test]
    public void TestDirectionAttribute() {
        TestStringAttribute<Slider>("direction", "Horizontal");
    }

    [Test]
    public void TestMaxLengthAttribute() {
        TestStringAttribute<TextField>("max-length", "100");
    }

    [Test]
    public void TestPasswordAttribute() {
        TestBooleanAttribute<TextField>("password", true);
        TestBooleanAttribute<TextField>("password", false);
    }

    [Test]
    public void TestMaskCharacterAttribute() {
        TestStringAttribute<TextField>("mask-character", "*");
    }

    [Test]
    public void TestReadOnlyAttribute() {
        TestBooleanAttribute<TextField>("readonly", true);
        TestBooleanAttribute<TextField>("readonly", false);
    }

    [Test]
    public void TestIsDelayedAttribute() {
        TestBooleanAttribute<TextField>("is-delayed", true);
        TestBooleanAttribute<TextField>("is-delayed", false);
    }

    [Test]
    public void TestHideMobileInputAttribute() {
        TestBooleanAttribute<TextField>("hide-mobile-input", true);
        TestBooleanAttribute<TextField>("hide-mobile-input", false);
    }

    [Test]
    public void TestKeyboardTypeAttribute() {
        TestStringAttribute<TextField>("keyboard-type", "NumberPad");
    }

    [Test]
    public void TestAutoCorrectionAttribute() {
        TestBooleanAttribute<TextField>("auto-correction", true);
        TestBooleanAttribute<TextField>("auto-correction", false);
    }

    [Test]
    public void TestMultilineAttribute() {
        TestBooleanAttribute<TextField>("multiline", true);
        TestBooleanAttribute<TextField>("multiline", false);
    }

    [Test]
    public void TestPageSizeAttribute() {
        TestNumericAttribute<Slider>("page-size", "5.0");
    }

    [Test]
    public void TestShowInputFieldAttribute() {
        TestBooleanAttribute<Slider>("show-input-field", true);
        TestBooleanAttribute<Slider>("show-input-field", false);
    }

    [Test]
    public void TestInvertedAttribute() {
        TestBooleanAttribute<Slider>("inverted", true);
        TestBooleanAttribute<Slider>("inverted", false);
    }

    [Test]
    public void TestMinValueAttribute() {
        TestNumericAttribute<MinMaxSlider>("min-value", "0.0");
    }

    [Test]
    public void TestMaxValueAttribute() {
        TestNumericAttribute<MinMaxSlider>("max-value", "100.0");
    }

    [Test]
    public void TestLowLimitAttribute() {
        TestNumericAttribute<MinMaxSlider>("low-limit", "0.0");
    }

    [Test]
    public void TestHighLimitAttribute() {
        TestNumericAttribute<MinMaxSlider>("high-limit", "100.0");
    }

    [Test]
    public void TestTitleAttribute() {
        TestStringAttribute<ProgressBar>("title", StringAttributeValue);
    }

    [Test]
    public void TestIndexAttribute() {
        // Build the element
        DropdownField el = UIBuddy.Build<DropdownField>(
            "<ui:DropdownField label=\"Dropdown\"  choices=\"one,two,three\" index=\"2\" />");
        Assert.NotNull(el);
        Assert.AreEqual(2, el.index);
    }

    [Test]
    public void TestChoicesAttribute() {
        TestListAttribute<DropdownField>("choices", new List<string> { "Choice1", "Choice2", "Choice3" });
    }
}