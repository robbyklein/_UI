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
    public void ClassAttribute() {
        TestClassListAttribute<VisualElement>("hello hello2");
    }

    [Test]
    public void NameAttribute() {
        TestStringAttribute<VisualElement>("name", StringAttributeValue);
    }

    [Test]
    public void TooltipAttribute() {
        TestStringAttribute<VisualElement>("tooltip", StringAttributeValue);
    }

    [Test]
    public void ViewDataKeyAttribute() {
        TestStringAttribute<VisualElement>("view-data-key", StringAttributeValue);
    }

    [Test]
    public void FocusableAttribute() {
        TestBooleanAttribute<VisualElement>("focusable", true);
        TestBooleanAttribute<VisualElement>("focusable", false);
    }

    [Test]
    public void VisibleAttribute() {
        TestBooleanAttribute<VisualElement>("visible", true);
        TestBooleanAttribute<VisualElement>("visible", false);
    }

    [Test]
    public void DelegatesFocusAttribute() {
        TestBooleanAttribute<VisualElement>("delegates-focus", true);
        TestBooleanAttribute<VisualElement>("delegates-focus", false);
    }

    [Test]
    public void PickingModeAttribute() {
        TestStringAttribute<VisualElement>("picking-mode", "Position");
    }

    [Test]
    public void UsageHintsAttribute() {
        TestStringAttribute<VisualElement>("usage-hints", "DynamicTransform");
    }

    [Test]
    public void TabIndexAttribute() {
        TestNumericAttribute<VisualElement>("tabindex", "1");
    }

    [Test]
    public void BindingPathAttribute() {
        TestStringAttribute<Label>("binding-path", StringAttributeValue);
    }

    [Test]
    public void TextAttribute() {
        TestStringAttribute<Label>("text", StringAttributeValue);
    }

    [Test]
    public void ParseEscapeSequencesAttribute() {
        TestBooleanAttribute<Label>("parse-escape-sequences", true);
        TestBooleanAttribute<Label>("parse-escape-sequences", false);
    }

    [Test]
    public void DisplayTooltipWhenElidedAttribute() {
        TestBooleanAttribute<Label>("display-tooltip-when-elided", true);
        TestBooleanAttribute<Label>("display-tooltip-when-elided", false);
    }

    [Test]
    public void EnableRichTextAttribute() {
        TestBooleanAttribute<Label>("enable-rich-text", true);
        TestBooleanAttribute<Label>("enable-rich-text", false);
    }

    [Test]
    public void ModeAttribute() {
        TestStringAttribute<ScrollView>("mode", "VerticalAndHorizontal");
    }

    [Test]
    public void HorizontalScrollerVisibilityAttribute() {
        TestStringAttribute<ScrollView>("horizontal-scroller-visibility", "Auto");
    }

    [Test]
    public void VerticalScrollerVisibilityAttribute() {
        TestStringAttribute<ScrollView>("vertical-scroller-visibility", "Auto");
    }

    [Test]
    public void MouseWheelScrollSizeAttribute() {
        TestNumericAttribute<ScrollView>("mouse-wheel-scroll-size", "10.0");
    }

    [Test]
    public void TouchScrollTypeAttribute() {
        TestStringAttribute<ScrollView>("touch-scroll-type", "Elastic");
    }

    [Test]
    public void ScrollDecelerationRateAttribute() {
        TestNumericAttribute<ScrollView>("scroll-deceleration-rate", "0.135");
    }

    [Test]
    public void ElasticityAttribute() {
        TestNumericAttribute<ScrollView>("elasticity", "0.1");
    }

    [Test]
    public void ElasticAnimationIntervalMsAttribute() {
        TestNumericAttribute<ScrollView>("elastic-animation-interval-ms", "150");
    }

    [Test]
    public void ShowBorderAttribute() {
        TestBooleanAttribute<ListView>("show-border", true);
        TestBooleanAttribute<ListView>("show-border", false);
    }

    [Test]
    public void SelectionTypeAttribute() {
        TestStringAttribute<ListView>("selection-type", "Single");
    }

    [Test]
    public void ShowAlternatingRowBackgroundsAttribute() {
        TestStringAttribute<ListView>("show-alternating-row-backgrounds", "All");
    }

    [Test]
    public void ReorderableAttribute() {
        TestBooleanAttribute<ListView>("reorderable", true);
        TestBooleanAttribute<ListView>("reorderable", false);
    }

    [Test]
    public void HorizontalScrollingAttribute() {
        TestBooleanAttribute<ListView>("horizontal-scrolling", true);
        TestBooleanAttribute<ListView>("horizontal-scrolling", false);
    }

    [Test]
    public void ShowFoldoutHeaderAttribute() {
        TestBooleanAttribute<ListView>("show-foldout-header", true);
        TestBooleanAttribute<ListView>("show-foldout-header", false);
    }

    [Test]
    public void HeaderTitleAttribute() {
        TestStringAttribute<ListView>("header-title", StringAttributeValue);
    }

    [Test]
    public void ShowAddRemoveFooterAttribute() {
        TestBooleanAttribute<ListView>("show-add-remove-footer", true);
        TestBooleanAttribute<ListView>("show-add-remove-footer", false);
    }

    [Test]
    public void ReorderModeAttribute() {
        TestStringAttribute<ListView>("reorder-mode", "Simple");
    }

    [Test]
    public void ShowBoundCollectionSizeAttribute() {
        TestBooleanAttribute<ListView>("show-bound-collection-size", true);
        TestBooleanAttribute<ListView>("show-bound-collection-size", false);
    }

    [Test]
    public void FixedItemHeightAttribute() {
        TestNumericAttribute<ListView>("fixed-item-height", "45.0");
    }

    [Test]
    public void VirtualizationMethodAttribute() {
        TestStringAttribute<ListView>("virtualization-method", "FixedHeight");
    }

    [Test]
    public void AutoExpandAttribute() {
        TestBooleanAttribute<TreeView>("auto-expand", true);
        TestBooleanAttribute<TreeView>("auto-expand", false);
    }

    [Test]
    public void LabelAttribute() {
        TestStringAttribute<TextField>("label", StringAttributeValue);
    }

    [Test]
    public void HighValueAttribute() {
        TestNumericAttribute<Slider>("high-value", "100.0");
    }

    [Test]
    public void ValueAttribute() {
        TestStringAttribute<TextField>("value", StringAttributeValue);
    }

    [Test]
    public void LowValueAttribute() {
        TestNumericAttribute<Slider>("low-value", "0.0");
    }

    [Test]
    public void DirectionAttribute() {
        TestStringAttribute<Slider>("direction", "Horizontal");
    }

    [Test]
    public void MaxLengthAttribute() {
        TestStringAttribute<TextField>("max-length", "100");
    }

    [Test]
    public void PasswordAttribute() {
        TestBooleanAttribute<TextField>("password", true);
        TestBooleanAttribute<TextField>("password", false);
    }

    [Test]
    public void MaskCharacterAttribute() {
        TestStringAttribute<TextField>("mask-character", "*");
    }

    [Test]
    public void ReadOnlyAttribute() {
        TestBooleanAttribute<TextField>("readonly", true);
        TestBooleanAttribute<TextField>("readonly", false);
    }

    [Test]
    public void IsDelayedAttribute() {
        TestBooleanAttribute<TextField>("is-delayed", true);
        TestBooleanAttribute<TextField>("is-delayed", false);
    }

    [Test]
    public void HideMobileInputAttribute() {
        TestBooleanAttribute<TextField>("hide-mobile-input", true);
        TestBooleanAttribute<TextField>("hide-mobile-input", false);
    }

    [Test]
    public void KeyboardTypeAttribute() {
        TestStringAttribute<TextField>("keyboard-type", "NumberPad");
    }

    [Test]
    public void AutoCorrectionAttribute() {
        TestBooleanAttribute<TextField>("auto-correction", true);
        TestBooleanAttribute<TextField>("auto-correction", false);
    }

    [Test]
    public void MultilineAttribute() {
        TestBooleanAttribute<TextField>("multiline", true);
        TestBooleanAttribute<TextField>("multiline", false);
    }

    [Test]
    public void PageSizeAttribute() {
        TestNumericAttribute<Slider>("page-size", "5.0");
    }

    [Test]
    public void ShowInputFieldAttribute() {
        TestBooleanAttribute<Slider>("show-input-field", true);
        TestBooleanAttribute<Slider>("show-input-field", false);
    }

    [Test]
    public void InvertedAttribute() {
        TestBooleanAttribute<Slider>("inverted", true);
        TestBooleanAttribute<Slider>("inverted", false);
    }

    [Test]
    public void MinValueAttribute() {
        TestNumericAttribute<MinMaxSlider>("min-value", "0.0");
    }

    [Test]
    public void MaxValueAttribute() {
        TestNumericAttribute<MinMaxSlider>("max-value", "100.0");
    }

    [Test]
    public void LowLimitAttribute() {
        TestNumericAttribute<MinMaxSlider>("low-limit", "0.0");
    }

    [Test]
    public void HighLimitAttribute() {
        TestNumericAttribute<MinMaxSlider>("high-limit", "100.0");
    }

    [Test]
    public void TitleAttribute() {
        TestStringAttribute<ProgressBar>("title", StringAttributeValue);
    }

    [Test]
    public void IndexAttribute() {
        // Build the element
        DropdownField el = UIBuddy.Build<DropdownField>(
            "<ui:DropdownField label=\"Dropdown\"  choices=\"one,two,three\" index=\"2\" />");
        Assert.NotNull(el);
        Assert.AreEqual(2, el.index);
    }

    [Test]
    public void ChoicesAttribute() {
        TestListAttribute<DropdownField>("choices", new List<string> { "Choice1", "Choice2", "Choice3" });
    }
}