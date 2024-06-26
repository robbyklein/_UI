using System;
using UnityEngine.UIElements;


internal static class Elements {
    internal static T Create<T>(string name) where T : VisualElement {
        VisualElement element = name switch {
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
            _ => throw new InvalidOperationException($"Cannot create element for type {name}.")
        };

        if (element is T typedElement) {
            return typedElement;
        }

        throw new InvalidOperationException(
            $"The created element is not of type {typeof(T).Name}. Element type is {element.GetType().Name}.");
    }
}